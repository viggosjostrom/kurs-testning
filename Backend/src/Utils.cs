namespace WebApp;
public static class Utils
{
    public static int SumInts(int a, int b)
    {
        return a + b;
    }

    public static Arr CreateMockUsers()
    {
        var read = File.ReadAllText(Path.Combine("json","mock-users.json"));
        Arr mockUsers = JSON.Parse(read);
        Arr sucessfullyWrittenUsers = Arr();
        foreach(var user in mockUsers)
        {
            user.password = "12345678";
            var result = SQLQueryOne(
                @"INSERT INTO users(firstName, lastName, email, password)
                VALUES($firstName, $lastName, $email, $password)
            ", user);
            // If we get an error from the DB then we havent added the mock users, if not we have to add to the sucessfull list
            if(!result.HasKey("error"))
            {
                user.Delete("password"); //The specification says reutrn the user list without password
                sucessfullyWrittenUsers.Push(user);
            }
        }
        return sucessfullyWrittenUsers;
    }

    public static bool IsPasswordGoodEnough(string passowrd)
    {
        string pattern = "^(?=.*[a-zåäö])(?=.*[A-ZÅÄÖ])(?=.*\\d)(?=.*[@$!%*?&\\s])[A-Za-zåäöÅÄÖ\\d@$!%*?&\\s]{8,}$";

        Regex regex = new Regex(pattern);

        return regex.IsMatch(passowrd);
    }
}