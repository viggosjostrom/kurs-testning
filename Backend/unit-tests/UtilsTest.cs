namespace WebApp;

public class UtilsTest(Xlog Console)
{
    // Read all mock users from file
    private static readonly Arr mockUsers = JSON.Parse(
        File.ReadAllText(FilePath("json", "mock-users.json"))
    );

    [Theory]
    [InlineData("abC9#fgh", true)]  // ok
    [InlineData("stU5/xyz", true)]  // ok too
    [InlineData("abC9#fg", false)]  // too short
    [InlineData("abCd#fgh", false)] // no digit
    [InlineData("abc9#fgh", false)] // no capital letter
    [InlineData("abC9efgh", false)] // no special character
    public void TestIsPasswordGoodEnough(string password, bool expected)
    {
        Assert.Equal(expected, Utils.IsPasswordGoodEnough(password));
    }

    [Theory]
    [InlineData("abC9#fgh", true)]  // ok
    [InlineData("stU5/xyz", true)]  // ok too
    [InlineData("abC9#fg", false)]  // too short
    [InlineData("abCd#fgh", false)] // no digit
    [InlineData("abc9#fgh", false)] // no capital letter
    [InlineData("abC9efgh", false)] // no special character
    public void TestIsPasswordGoodEnoughRegexVersion(string password, bool expected)
    {
        Assert.Equal(expected, Utils.IsPasswordGoodEnoughRegexVersion(password));
    }

    [Theory]
    [InlineData(
        "---",
        "Hello, I am going through hell. Hell is a real fucking place " +
            "outside your goddamn comfy tortoiseshell!",
        "Hello, I am going through ---. --- is a real --- place " +
            "outside your --- comfy tortoiseshell!"
    )]
    [InlineData(
        "---",
        "Rhinos have a horny knob? (or what should I call it) on " +
            "their heads. And doorknobs are damn round.",
        "Rhinos have a --- ---? (or what should I call it) on " +
            "their heads. And doorknobs are --- round."
    )]
    public void TestRemoveBadWords(string replaceWith, string original, string expected)
    {
        Assert.Equal(expected, Utils.RemoveBadWords(original, replaceWith));
    }

    [Fact]
    public void TestCreateMockUsers()
    {
        // Get all users from the database
        Arr usersInDb = SQLQuery("SELECT email FROM users");
        Arr emailsInDb = usersInDb.Map(user => user.email);
        // Only keep the mock users not already in db
        Arr mockUsersNotInDb = mockUsers.Filter(
            mockUser => !emailsInDb.Contains(mockUser.email)
        );
        // Get the result of running the method in our code
        var result = Utils.CreateMockUsers();
        // Assert that the CreateMockUsers only return
        // newly created users in the db
        Console.WriteLine($"The test expected that {mockUsersNotInDb.Length} users should be added.");
        Console.WriteLine($"And {result.Length} users were added.");
        Console.WriteLine("The test also asserts that the users added " +
            "are equivalent (the same) as the expected users!");
        Assert.Equivalent(mockUsersNotInDb, result);
        Console.WriteLine("The test passed!");
    }

    // Now write the two last ones yourself!
    // See: https://sys23m-jensen.lms.nodehill.se/uploads/videos/2021-05-18T15-38-54/sysa-23-presentation-2024-05-02-updated.html#8

    [Fact]
    public void TestCountDomainsFromUserEmails()
    {
        // Adding testdata
        SQLQueryOne(@"INSERT INTO users (firstName, lastName, email, password) VALUES ('User1', 'Example', 'user1@example.com', '12345678')");
        SQLQueryOne(@"INSERT INTO users (firstName, lastName, email, password) VALUES ('User2', 'Example', 'user2@example.com', '12345678')");
        SQLQueryOne(@"INSERT INTO users (firstName, lastName, email, password) VALUES ('User3', 'Test', 'user3@test.com', '12345678')");
        SQLQueryOne(@"INSERT INTO users (firstName, lastName, email, password) VALUES ('User4', 'Test', 'user4@test.com', '12345678')");

        var result = Utils.CountDomainsFromUserEmails();

        var entries = result.GetEntries();
        foreach (var entry in entries)
        {
            var key = entry[0]; // First element is the key
            var value = entry[1]; // Second element is the value
            Console.WriteLine($"Domain: {key}, Count: {value}");
        }

        Assert.True(result.HasKey("example.com"), "example.com domain should be present in the result.");
        Assert.True(result.HasKey("test.com"), "test.com domain should be present in the result.");

        Assert.IsType<int>(result["example.com"]);
        Assert.IsType<int>(result["test.com"]);

        Assert.Equal(2, (int)result["example.com"]);
        Assert.Equal(2, (int)result["test.com"]);
    }

    [Fact]
    public void TestRemoveMockUsers()
    {
        // Call the method to remove mock users
        var removedUsers = Utils.RemoveMockUsers();

        // Verify the users have been removed and passwords are not returned
        foreach (var user in removedUsers)
        {
            Assert.False(user.HasKey("password"), "Password should not be returned.");

            // Verify the user no longer exists in the database
            var result = SQLQueryOne(
                @"SELECT * FROM users 
                  WHERE firstName = $firstName AND lastName = $lastName AND email = $email",
                user
            );

            Assert.True(result == null || result.Count == 0, "User should be removed from the database.");

        }
    }

}