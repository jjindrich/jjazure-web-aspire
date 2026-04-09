# Copilot instructions for `jjazure-web-aspire`

## Build, test, and lint commands

Run commands from the repository root unless noted otherwise.

- Build the whole solution: `dotnet build .\src\jjwebaspire.slnx`
- Build a single project when you only changed one area:
  - Web frontend: `dotnet build .\src\jjwebaspire.Web\jjwebaspire.Web.csproj`
  - API service: `dotnet build .\src\jjwebaspire.ApiService\jjwebaspire.ApiService.csproj`
  - AppHost: `dotnet build .\src\jjwebaspire.AppHost\jjwebaspire.AppHost.csproj`
- Run the distributed app locally through Aspire: `dotnet run --project .\src\jjwebaspire.AppHost\jjwebaspire.AppHost.csproj`

There are currently **no test projects** in `src\jjwebaspire.slnx` and no dedicated lint script or formatter config checked into the repo. Do not invent `dotnet test` or lint steps in automation unless those are added to the repository first.

## High-level architecture

This repository is an Aspire-composed .NET application with four projects:

- `jjwebaspire.AppHost` is the orchestration entry point. It starts the API service and web frontend, creates the `cosmos-db` resource, adds database `mydb` and container `MyDbContext`, and wires dependency ordering with `WithReference(...)` and `WaitFor(...)`.
- `jjwebaspire.Web` is the user-facing Blazor Web App. It uses interactive server components, connects EF Core to Cosmos with `AddCosmosDbContext<MyDbContext>("cosmos-db", "mydb")`, and calls the API through a typed `HttpClient`.
- `jjwebaspire.ApiService` is a minimal backend that serves `/weatherforecast`.
- `jjwebaspire.ServiceDefaults` centralizes cross-cutting runtime behavior for both services: OpenTelemetry, service discovery, standard `HttpClient` resilience, and health checks.

The important cross-project flow is:

1. AppHost creates the topology and injects service/resource references.
2. The web app resolves the API by the Aspire service name `apiservice` using the `https+http://apiservice` base address pattern rather than a hard-coded URL.
3. The web app also uses the Cosmos resource name from AppHost rather than a local connection string.
4. Deployment is centered on the AppHost: `src\azure.yaml` points azd at `jjwebaspire.AppHost`, and `.github\workflows\azure-dev.yml` provisions and deploys from `src\` with `azd`.

## Key conventions

- **Keep shared runtime setup in `jjwebaspire.ServiceDefaults`.** Both the web app and API call `builder.AddServiceDefaults()`. If you need to change service discovery, default resilience, telemetry, or health endpoint behavior, update the shared extensions rather than duplicating setup in each service.
- **Model service-to-service calls with Aspire names, not environment-specific URLs.** The current pattern is the typed `WeatherApiClient` in the web app using `https+http://apiservice`. Reuse that pattern for new internal HTTP dependencies.
- **Treat Cosmos as an AppHost-managed dependency.** The AppHost defines the resource name, database name, and container name, and the web app binds to those names. Keep new Cosmos databases/containers declared in AppHost so local Aspire runs and Azure deployment stay aligned.
- **Preserve the environment split for Cosmos.** Development uses `cosmos.RunAsEmulator()`. Non-development publishes against the existing Azure Cosmos account `jjcosmos` in resource group `rg-jjwebaspire-data`. Changes to Cosmos provisioning should account for both paths.
- **The web app eagerly creates the EF Core/Cosmos schema at startup.** `jjwebaspire.Web\Program.cs` calls `dbContext.Database.EnsureCreatedAsync()` before serving requests. If you change the data model, remember that startup behavior is part of the current contract.
- **Use component-level streaming/caching patterns where they already exist.** `Weather.razor` and `Cosmos.razor` use `[StreamRendering(true)]`, and `Weather.razor` also uses `[OutputCache(Duration = 5)]`. Follow those existing patterns for similar pages instead of introducing a different data-loading style without a reason.
- **Keep azd-owned deployment assets consistent.** The README and workflow assume `src\azure.yaml` plus the `src\.azure\` environment state drive provisioning/deployment. If you change AppHost resources, check whether the azd/Bicep assets also need to change.

## Deployment context from the repository docs

- The intended Azure deployment flow is `azd provision` and `azd deploy` from `src\`.
- The repository already includes a GitHub Actions workflow that runs those commands on `main`.
- The README also documents `azd pipeline config` for GitHub Actions setup and `aspirate` as an alternative Kubernetes manifest workflow.
