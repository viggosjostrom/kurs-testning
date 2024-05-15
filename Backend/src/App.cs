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

Server.Start();
WebApp.Utils.CreateMockUsers();
//WebApp.Utils.RemoveMockUsers();
WebApp.Utils.CountDomainsFromUserEmails();