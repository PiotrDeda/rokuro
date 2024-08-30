$project = "Rokuro.csproj"

$version = Select-String -Path $project -Pattern '<Version>(.*?)<\/Version>' | ForEach-Object {
    $_.Matches[0].Groups[1].Value
}

Write-Host "=== Building ===`n"
dotnet build $project -c Release

Write-Host "`n=== Packing ===`n"
dotnet pack $project -c Release

Write-Host "`n=== Pushing ===`n"
dotnet nuget push Rokuro.$version.nupkg -s https://f.feedz.io/rokuro/rokuro/nuget/index.json -k $env:API_KEY
