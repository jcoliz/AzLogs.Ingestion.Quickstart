#
# WARNING: This script is only useful if you already have a log analytics workspace already in place,
# and you want to add a DCR/DCE to access it.
#
# For normal circumstances, please use the regular `Deploy-Services.ps1`
#

param(
    [Parameter(Mandatory=$true)]
    [string]
    $ResourceGroup,
    [Parameter(Mandatory=$true)]
    [string]
    $Location,
    [Parameter(Mandatory=$true)]
    [GUID]
    $ServicePrincipal,
    [Parameter(Mandatory=$true)]
    [string]
    $Suffix,
    [Parameter(Mandatory=$true)]
    [string]
    $LogsName
)

Write-Output "Deploying to Resource Group $ResourceGroup"
$result = az deployment group create --name "Deploy-$(Get-Random)" --resource-group $ResourceGroup --template-file .azure\deploy\azlogs-ingestion-elaw.bicep --parameters .azure\deploy\azlogs-ingestion.parameters.json --parameters principalId=$ServicePrincipal suffix=$Suffix logAnalyticsName=$LogsName | ConvertFrom-Json

Write-Output "OK"
Write-Output ""

Write-Output "Copy these values to config.toml:"
Write-Output ""

$dcrImmutableId = $result.properties.outputs.dcrImmutableId.value
$endpointUri = $result.properties.outputs.endpointUri.value
$stream = $result.properties.outputs.stream.value

Write-Output "[LogIngestion]"
Write-Output "EndpointUri = ""$endpointUri"""
Write-Output "DcrImmutableId = ""$dcrImmutableId"""
Write-Output "Stream = ""$stream"""
