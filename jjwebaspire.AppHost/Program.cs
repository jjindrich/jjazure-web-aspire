var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.jjwebaspire_api>("jjapi");

builder.AddProject<Projects.jjwebaspire_web>("jjweb")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
