param(
    [string]$IndexHtmlPath = "dist/wwwroot/index.html",
    [string]$ManifestJsonPath = "dist/wwwroot/manifest.json"
)

# Process index.html
if (Test-Path $IndexHtmlPath) {
    $content = [IO.File]::ReadAllText($IndexHtmlPath)
    
    # Replace base href from / to /RubyingChinesePronunciationPage/
    # Handle various formatting: <base href="/" />, <base href="/"/>, etc.
    $content = [System.Text.RegularExpressions.Regex]::Replace(
        $content,
        '<base\s+href="/"\s*/>',
        '<base href="/RubyingChinesePronunciationPage/" />',
        [System.Text.RegularExpressions.RegexOptions]::IgnoreCase
    )
    
    # Replace service worker registration path from '/service-worker.js' to '/RubyingChinesePronunciationPage/service-worker.js'
    $content = $content -replace "navigator\.serviceWorker\.register\('/service-worker\.js'\)", "navigator.serviceWorker.register('/RubyingChinesePronunciationPage/service-worker.js')"
    
    [IO.File]::WriteAllText($IndexHtmlPath, $content)
    Write-Host "Base path replaced successfully in: $IndexHtmlPath"
} else {
    Write-Warning "File not found: $IndexHtmlPath"
}

# Process manifest.json
if (Test-Path $ManifestJsonPath) {
    $manifestContent = [IO.File]::ReadAllText($ManifestJsonPath)
    
    # Replace start_url from "/" to "/RubyingChinesePronunciationPage/"
    $manifestContent = $manifestContent -replace '"start_url"\s*:\s*"/"', '"start_url": "/RubyingChinesePronunciationPage/"'
    
    # Replace scope from "/" to "/RubyingChinesePronunciationPage/"
    $manifestContent = $manifestContent -replace '"scope"\s*:\s*"/"', '"scope": "/RubyingChinesePronunciationPage/"'
    
    [IO.File]::WriteAllText($ManifestJsonPath, $manifestContent)
    Write-Host "start_url and scope replaced successfully in: $ManifestJsonPath"
} else {
    Write-Warning "File not found: $ManifestJsonPath"
}
