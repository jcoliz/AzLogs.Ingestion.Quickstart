param([Parameter(Mandatory=$true)][string]$ResourceGroup)

Write-Host "Removing resource group $ResourceGroup"

az group delete --name $ResourceGroup
