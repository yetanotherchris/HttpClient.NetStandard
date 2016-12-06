#$apiKey = Read-Host "Enter an apikey"
$apiKey='470046d3-8356-4f21-a983-49e14a6108c5'


rm *.nupkg
nuget pack .\HttpClient.NetStandard.csproj -properties Configuration=release
nuget push *.nupkg $apiKey -source https://www.nuget.org/api/v2/package