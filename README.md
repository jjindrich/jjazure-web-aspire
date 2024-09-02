# jjazure-web-aspire
JJ Website using dotnet Aspire to run distributed applications

## Deployment

https://learn.microsoft.com/en-us/dotnet/aspire/deployment/overview

### Deploying to Kubernetes

Aspirate generates manifests with Kustomize. To deploy to Kubernetes, run the following command:

```powershell
dotnet tool install --global aspirate

aspirate init
aspirate generate
aspirate run
```

### Deploying to Azure ContainerApps

Create Publish profile in Visual Studio. It will create new Azure ContainerApps resource. It uses azd cli to deploy the application.
