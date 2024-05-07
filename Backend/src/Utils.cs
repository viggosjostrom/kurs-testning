namespace WebApp;
public static class Utils
{
    public static int SumInts(int a, int b)
    {
        return a + b;
    }

    public static Arr CreateMockUsers()
    {
        // Read all mock users from the JSON file
        var read = File.ReadAllText(FilePath("json", "mock-users.json"));
        Arr mockUsers = JSON.Parse(read);
        Arr successFullyWrittenUsers = Arr();
        foreach (var user in mockUsers)
        {
            user.password = "12345678";
            var result = SQLQueryOne(
                @"INSERT INTO users(firstName,lastName,email,password)
                VALUES($firstName, $lastName, $email, $password)
            ", user);
            // If we get an error from the DB then we haven't added
            // the mock users, if not we have so add to the successful list
            if (!result.HasKey("error"))
            {
                // The specification says return the user list without password
                user.Delete("password");
                successFullyWrittenUsers.Push(user);
            }
        }
        return successFullyWrittenUsers;
    }
    

    public static bool IsPasswordGoodEnough(string passowrd)
    {
        string pattern = "^(?=.*[a-zåäö])(?=.*[A-ZÅÄÖ])(?=.*\\d)(?=.*[@$!%*?&\\s])[A-Za-zåäöÅÄÖ\\d@$!%*?&\\s]{8,}$";

        Regex regex = new Regex(pattern);

        return regex.IsMatch(passowrd);
    }

    
    public static string RemoveBadWords(string text, string replacement)
    {

        var badWordsJson = File.ReadAllText(Path.Combine("json", "bad-words.json"));
        var badWords = JSON.Parse(badWordsJson) as Arr;

        foreach (var word in badWords)
        {
            var regex = new Regex($@"\b{Regex.Escape(word)}\b", RegexOptions.IgnoreCase);
            text = regex.Replace(text, replacement);
        }
        return text;
    }
}