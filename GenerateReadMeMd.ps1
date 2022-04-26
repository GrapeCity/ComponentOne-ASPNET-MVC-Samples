Get-ChildItem ".\" -Recurse | where {$_.Name -eq "readme.txt"} | % {
    $readme = $_.FullName | Resolve-Path -Relative
    $sampleFolder = "Mvc"
    
    if($readme -match '(?<sampleFolder>\\.*)\\readme.txt')
    {
        $sampleFolder = $Matches.sampleFolder
        $sampleFolder = $sampleFolder.Replace('\', '/')
        #$sampleFolder
    }
    $url="https://downgit.github.io/#/home?url=https://github.com/GrapeCity/ComponentOne-ASPNET-MVC-Samples/tree/master$sampleFolder"
    $command = "$pwd\CreateMDFiles.exe $readme $url"
    Invoke-Expression $command
} 
