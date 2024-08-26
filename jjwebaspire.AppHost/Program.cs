var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.jjwebaspire_Api>("jjapi");

builder.AddProject<Projects.jjwebaspire_Web>("jjweb")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
