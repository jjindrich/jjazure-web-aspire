using MessagePack.Formatters;
using Microsoft.Extensions.Hosting;
using Azure.Provisioning;

var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.jjwebaspire_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

var cosmos = builder.AddAzureCosmosDB("cosmos-db");
if (builder.Environment.IsDevelopment())
{
    cosmos.RunAsEmulator();
}
var db = cosmos.AddCosmosDatabase("mydb");
var container = db.AddContainer("MyDbContext", "/__partitionKey");

builder.AddProject<Projects.jjwebaspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService)
    .WaitFor(cosmos)
    .WithReference(cosmos);

builder.Build().Run();
