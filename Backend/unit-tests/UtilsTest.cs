namespace WebApp;

using Microsoft.AspNetCore.Components.Web;
using Xunit;
using Xunit.Sdk;
using System.Text.RegularExpressions;

public class UtilsTest
{
    [Fact]
    public void TestSumInt()
    {
        Assert.Equal(12, Utils.SumInts(7, 5));
        Assert.Equal(-3, Utils.SumInts(6, -9));
    }

    // NOTE: To see console.writelines / Log and to get the path correct
    // Start Git Bash terminal (standalone). Navigate to backend folder, do "dotnet test".
  /*  [Fact]
    public void TestCreateMockUsers()
    {
    //Read all mockusers from the JSON file
       var read = File.ReadAllText(Path.Combine("json","mock-users.json"));
       Arr mockUsers = JSON.Parse(read);
         
        // Get all users from the database
        Arr usersInDb = SQLQuery("SELECT email FROM users");
        Arr emailsInDb = usersInDb.Map(user => user.email);
        // Only keep the mock users not already in DB
        Arr mockUsersNotInDb = mockUsers.Filter(mockUser => !emailsInDb.Contains(mockUser.email));
        //Assert that the CreateMockUsers only return newly created users in the DB
        
        var result = Utils.CreateMockUsers();

        // Assert the same length
       Assert.Equal(mockUsersNotInDb.Length, result.Length);
        //Check equivalency for each user
        for(var i = 0; i < result.Length; i++)
        {
           // Assert.Equivalent(mockUsersNotInDb[i], result[i]);
           Assert.Equal(JSON.Stringify(result[i]), JSON.Stringify(mockUsersNotInDb[i])); 
        }
    }
*/
    [Fact]
    public void IsPasswordGoodEnoughTest()
    {
    string validPassword = "&&&&&&&&&&&&uS1äÖ";
    string invalidPassword = "solstråle1994";

    bool validPasswordPassed = Utils.IsPasswordGoodEnough(validPassword);
    bool invalidPasswordPassed = Utils.IsPasswordGoodEnough(invalidPassword);

    Assert.True(validPasswordPassed);
    Assert.False(invalidPasswordPassed);
    }
}