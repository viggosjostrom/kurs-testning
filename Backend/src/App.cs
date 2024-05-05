// Global settings
Globals = Obj(new
{
    debugOn = true,
    detailedAclDebug = false,
    aclOn = true,
    isSpa = true,
    port = 3001,
    serverName = "Ironboy's Minimal API Server",
    frontendPath = FilePath("..", "Frontend"),
    sessionLifeTimeHours = 2
});

//Server.Start();

// För att printa users i terminalen
//new UtilsTest().TestCreateMockUsers();

/*
var addedUsers = WebApp.Utils.CreateMockUsers();
foreach(var user in addedUsers)
{
    Log("addedUsers", user);
}
*/

new UtilsTest().IsPasswordGoodEnoughTest(); 