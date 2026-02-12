# jjazure-web-aspire
JJ Website using dotnet Aspire to run distributed applications

About Aspire: https://aspire.dev/

## Development

### Using CosmosDB

https://aspire.dev/integrations/cloud/azure/azure-cosmos-db/

It creates CosmosDB resource in Azure. You need setup permissions for you as developer.

## Deployment

https://aspire.dev/deployment/overview/

### Deploying to Azure ContainerApps

Create Publish profile in Visual Studio. It will create new Azure ContainerApps resource. It uses azd cli to deploy the application.
Creates definition in .azure folder and azure.yaml file.

### Deploy with Azd

It stores data into .azure folder. Azure resources are tagged with azd-env-name=your_environment_name.

This will deploy new Azure environment and fill .env file
```
azd env new jjwebaspire-dev
azd env set AZURE_SUBSCRIPTION_ID <your-subscription-id>
azd env set AZURE_LOCATION swedencentral
azd env set AZURE_CONTAINER_APPS_ENVIRONMENT_NAME <your-existing-env-name>
azd env set AZURE_CONTAINER_APPS_ENVIRONMENT_RG <your-existing-env-rg>

azd provision
azd deploy
```

If you want to get information about Azure environment (fill .env file) and run deployment
```
azd env new jjwebaspire-dev
azd env set AZURE_SUBSCRIPTION_ID <your-subscription-id>
azd env set AZURE_LOCATION swedencentral

azd env refresh
azd deploy
```

### Configure GitHub Actions

https://github.com/dotnet/docs-aspire/blob/main/docs/deployment/azd/aca-deployment-github-actions.md
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