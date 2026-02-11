# jjazure-web-aspire
JJ Website using dotnet Aspire to run distributed applications

About Aspire: https://aspire.dev/


## Development

### Using CosmosDB

TODO: https://github.com/jjindrich/jjweb-aspire-tracing

## Deployment

https://aspire.dev/deployment/overview/

### Deploying to Azure ContainerApps

Create Publish profile in Visual Studio. It will create new Azure ContainerApps resource. It uses azd cli to deploy the application.

### Configure GitHub Actions

TODO: configure github

### Deploying to Kubernetes

Aspirate generates manifests with Kustomize. To deploy to Kubernetes, run the following command:

```powershell
dotnet tool install --global aspirate

aspirate init
aspirate generate
aspirate run
```

## Monitoring

### Aspire Dashboard

Data stored in-memory - https://aspire.dev/fundamentals/telemetry/

### Application Insights

Use OpenTelemetry with Azure Monitor https://learn.microsoft.com/en-us/dotnet/aspire/deployment/aspire-deploy/application-insights#use-the-azure-monitor-distro

TODO: set OTEL exporter to Application Insights