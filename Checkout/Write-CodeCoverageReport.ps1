$RawCoverageFolderName = ".coverage"
$HtmlCoverageFolderName = ".coverage-report"

$CurrentDir = Get-Location | Select-Object -ExpandProperty Path
$RawCoverage = [IO.Path]::Combine($CurrentDir, $RawCoverageFolderName)
$HtmlCoverageReports = [IO.Path]::Combine($CurrentDir, $HtmlCoverageFolderName)

if (Test-Path $RawCoverage)
{
    Remove-Item -Path $RawCoverage -Recurse
}

if (Test-Path $HtmlCoverageReports)
{
    Remove-Item -Path $HtmlCoverageReports -Recurse
}

dotnet test --collect:"XPlat Code Coverage" --results-directory:"./$RawCoverageFolderName"

reportgenerator "-reports:./$RawCoverageFolderName/**/*.cobertura.xml" "-targetdir:$HtmlCoverageFolderName/" -reporttypes:HTML

$HtmlReportIndex = [IO.Path]::Combine($HtmlCoverageFolderName, "index.html")
$ReportExists = Test-Path $HtmlReportIndex

if (-not $ReportExists)
{
    throw "Report cannot be found at $HtmlReportIndex"
}

Invoke-Item $HtmlReportIndex