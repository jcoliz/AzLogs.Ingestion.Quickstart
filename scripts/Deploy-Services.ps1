param(
    [Parameter(Mandatory=$true)]
    [string]
    $ResourceGroup,
    [Parameter(Mandatory=$true)]
    [string]
    $Location,
    [Parameter(Mandatory=$true)]
    [GUID]
    $ServicePrincipal
)

Write-Output "Creating Resource Group $ResourceGroup in $Location"
az group create --name $ResourceGroup --location $Location

Write-Output "Deploying to Resource Group $ResourceGroup"
$result = az deployment group create --name "Deploy-$(Get-Random)" --resource-group $ResourceGroup --template-file .azure\deploy\azlogs-ingestion.bicep --parameters .azure\deploy\azlogs-ingestion.parameters.json --parameters principalId=$ServicePrincipal | ConvertFrom-Json

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
