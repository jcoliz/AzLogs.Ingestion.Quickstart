//
// Deploys a complete set of needed resources for the sample at:
//    https://github.com/jcoliz/AzLogs.Ingestion
//
// Requires:
//    * Log Analytics Workspace (LAW)
//
// Includes:
//    * Custom table on Log Analytics Workspace (LAW)
//    * Data Collection Endpoint (DCE)
//    * Data Collection Rule (DCR) with connection to DCE and LAW
//    * Monitoring Metrics Publisher role on DCR for the Service Principal of your choice
//

@description('Unique suffix for all resources in this deployment')
@minLength(5)
param suffix string = uniqueString(resourceGroup().id)

@description('Location for all resources.')
param location string = resourceGroup().location

@description('Name of required Log Analytics Workspace')
param logAnalyticsName string

@description('Schema of table in log workspace')
param tableSchema object

@description('KQL query to transform input to putput ')
param transformKql string

@description('Columns of input schema')
param inputColumns array

@description('The principal that will be assigned Monitoring Metrics Publisher role for the Data Collection Rule resource')
param principalId string

@description('The type of the given principal')
param principalType string = 'ServicePrincipal'

// Deploy custom table on that workspace

module table 'AzDeploy.Bicep/OperationalInsights/workspace-table.bicep' = {
  name: 'table'
  params: {
    logAnalyticsName: logAnalyticsName
    tableSchema: tableSchema
  }
}

// Deploy Data Collection Endpoint

module dcep 'AzDeploy.Bicep/Insights/datacollectionendpoint.bicep' = {
  name: 'dcep'
  params: {
    suffix: suffix
    location: location
  }
}

// Deploy Data Collection Rule (DCR) with connection to DCE and LAW custom table

module dcr 'AzDeploy.Bicep/Insights/datacollectionrule.bicep' = {
  name: 'dcr'
  params: {
    suffix: suffix
    location: location
    logAnalyticsName: logAnalyticsName
    tableName: tableSchema.name
    endpointName: dcep.outputs.name
    transformKql: transformKql
    inputColumns: inputColumns
  }
  dependsOn: [
    table
  ]
}

// Deploy Monitoring Metrics Publisher role on DCR for supplied Service Principal

module publisherRole 'AzDeploy.Bicep/Insights/monitoring-metrics-publisher-role.bicep' = {
  name: 'publisherRole'
  params: {
    dcrName: dcr.outputs.name
    principalId: principalId
    principalType: principalType
  }
}

// Return necessary outputs to user. Put these in `LogIngestion` section
// of application configuration, e.g. `config.toml` 

output DcrImmutableId string = dcr.outputs.DcrImmutableId
output EndpointUri string = dcep.outputs.EndpointUri
output Stream string = dcr.outputs.Stream
