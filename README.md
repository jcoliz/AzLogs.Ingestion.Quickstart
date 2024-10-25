# AzLogs.Ingestion.Quickstart Sample

This sample is a quick way to get started using the [Logs Ingestion API in Azure Monitor](https://learn.microsoft.com/en-us/azure/azure-monitor/logs/logs-ingestion-api-overview) on .NET 8.0.
The sample retrieves weather forecasts from the U.S. [National Weather Service API](https://www.weather.gov/documentation/services-web-api), then forwards them on to a [Logs Analytics Workspace](https://learn.microsoft.com/en-us/azure/azure-monitor/logs/log-analytics-workspace-overview) using a [Data Collection Rule](https://learn.microsoft.com/en-us/azure/azure-monitor/essentials/data-collection-rule-overview).

## Use case

Let's say we have some important data available in an external service, and we want some of that data in our Log Analytics Workspace. However, we don't control the external service, so we can't simply modify it to upload logs directly. What we can do instead is:

* **Extract** the data using an Azure Function, then send to a Data Collection Endpoint, which will
* **Transform** it into our desired schema using the Data Collection Rule, and
* **Load** it into a Log Analytics Workspace table.

## Prerequisites

In order to follow the instructions shown here, and run this sample, you will first need:

* An Azure Account. Set up a [Free Azure Account](https://azure.microsoft.com/en-us/pricing/purchase-options/azure-account) to get started.
* [Azure CLI tool with Bicep](https://learn.microsoft.com/en-us/azure/azure-resource-manager/bicep/install#azure-cli)

Please read through the [Logs Ingestion API in Azure Monitor](https://learn.microsoft.com/en-us/azure/azure-monitor/logs/logs-ingestion-api-overview) article carefully before proceeding.
This sample will first follow that article closely, before moving on to demonstrate publishing an Azure Function.

## Register a Microsoft Entra app

The very first step is to [Register an application with the Microsoft Identity Platform](https://learn.microsoft.com/en-us/entra/identity-platform/quickstart-register-app?tabs=client-secret). From a PowerShell window in this folder, simply run the Create-AppRegistration.ps1 script.

```dotnetcli
.\scripts\Create-AppRegistration -Name "azlogs-ingestion-quickstart"
```

After completion, this script will display configuration values you'll need later to configure the app.

```toml
[Identity]
TenantId = "<tenant_id>"
AppId = "<client_id>"
AppSecret = "<client_secret>" 
```

Additionally, it will display the Service Principal ID needed when deploying Azure resources, next:

```powershell
-ServicePrincipal <principal_id>
```

## Deploy Azure resources

This sample requires three Azure resources: Log Analytics Workspace, Data Collection Rule, and Data Collection Endpoint. There is an Azure Resource Manager (ARM) template here to set up everything you need, ready to go: [azlogs-ingestion.bicep](./.azure/deploy/azlogs-ingestion.bicep).
Be sure to clone this repo with submodules so you have the [AzDeploy.Bicep](https://github.com/jcoliz/AzDeploy.Bicep) project handy with the necessary module templates.

```powershell
git clone --recurse-submodules https://github.com/jcoliz/AzLogs.Ingestion.Quickstart.git
```

From a PowerShell window in this folder, simply run the Deploy-Services.ps1 script. You'll need to supply the `principal_id` for your app, from the previous step.
Also, feel free to choose any resource group name and Azure data center location:

```dotnetcli
./scripts/Deploy-Services -ResourceGroup "azlogs-ingestion-quickstart" -Location "West US 2" -ServicePrincipal "<principal_id>"
```

After completion, this script will display configuration values you'll need in the next step.

```toml
[LogIngestion]
EndpointUri = "<data_collection_endpoint_uri>" 
Stream = "<stream_name>" 
DcrImmutableId = "<data_collection_rule_id>"
```
