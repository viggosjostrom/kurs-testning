namespace WebApp;
public static class Utils
{
    // Read all mock users from file
    private static readonly Arr mockUsers = JSON.Parse(
        File.ReadAllText(FilePath("json", "mock-users.json"))
    );

    // Read all bad words from file and sort from longest to shortest
    // if we didn't sort we would often get "---ing" instead of "---" etc.
    // (Comment out the sort - run the unit tests and see for yourself...)
    private static readonly Arr badWords = ((Arr)JSON.Parse(
        File.ReadAllText(FilePath("json", "bad-words.json"))
    )).Sort((a, b) => ((string)b).Length - ((string)a).Length);

    public static bool IsPasswordGoodEnough(string password)
    {
        return password.Length >= 8
            && password.Any(Char.IsDigit)
            && password.Any(Char.IsLower)
            && password.Any(Char.IsUpper)
            && password.Any(x => !Char.IsLetterOrDigit(x));
    }

    public static bool IsPasswordGoodEnoughRegexVersion(string password)
    {
        // See: https://dev.to/rasaf_ibrahim/write-regex-password-validation-like-a-pro-5175
        var pattern = @"^(?=.*[0-9])(?=.*[a-zåäö])(?=.*[A-ZÅÄÖ])(?=.*\W).{8,}$";
        return Regex.IsMatch(password, pattern);
    }

    public static string RemoveBadWords(string comment, string replaceWith = "---")
    {
        comment = " " + comment;
        replaceWith = " " + replaceWith + "$1";
        badWords.ForEach(bad =>
        {
            var pattern = @$" {bad}([\,\.\!\?\:\; ])";
            comment = Regex.Replace(
                comment, pattern, replaceWith, RegexOptions.IgnoreCase);
        });
        return comment[1..];
    }

    public static Arr CreateMockUsers()
    {
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

    // Now write the two last ones yourself!
    // See: https://sys23m-jensen.lms.nodehill.se/uploads/videos/2021-05-18T15-38-54/sysa-23-presentation-2024-05-02-updated.html#8

    public static Arr RemoveMockUsers()
    {
        Arr removedUsers = Arr();
        foreach (var user in mockUsers)
        {
            var result = SQLQueryOne(
                @"DELETE FROM users 
              WHERE firstName = $firstName AND lastName = $lastName AND email = $email
              RETURNING firstName, lastName, email", // Returning the deleted user info except password
                user
            );

            // If the result has returned data, it means the user was successfully deleted
            if (result != null && result.Count > 0)
            {
                removedUsers.Push(result);
            }
        }
        return removedUsers;
    }

    public static Obj CountDomainsFromUserEmails()
    {
        // Fetch all emails from the users table
        var users = SQLQuery(@"SELECT email FROM users");

        // Dictionary to store domain counts
        Dictionary<string, int> domainCounts = new Dictionary<string, int>();

        // Regular expression to extract domain from email
        Regex domainRegex = new Regex(@"@(?<domain>[\w.-]+)$");

        foreach (var user in users)
        {
            string email = user["email"];
            Match match = domainRegex.Match(email);

            if (match.Success)
            {
                string domain = match.Groups["domain"].Value;

                if (domainCounts.ContainsKey(domain))
                {
                    domainCounts[domain]++;
                }
                else
                {
                    domainCounts[domain] = 1;
                }
            }
        }

        // Convert dictionary to Obj
        Obj result = Obj();
        foreach (var domainCount in domainCounts)
        {
            result[domainCount.Key] = domainCount.Value;
        }

        return result;
    }
}