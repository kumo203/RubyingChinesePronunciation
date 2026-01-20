param([string]$IndexHtmlPath)

$content = [IO.File]::ReadAllText($IndexHtmlPath)

# Remove integrity attribute from preload link
$content = $content -replace 'integrity="[^"]*"', ''

# Remove entire "integrity" object from importmap (multiline)
$content = [System.Text.RegularExpressions.Regex]::Replace(
    $content, 
    '"integrity"\s*:\s*\{[^}]*\}', 
    '',
    [System.Text.RegularExpressions.RegexOptions]::Multiline -bor [System.Text.RegularExpressions.RegexOptions]::Singleline
)

# Remove trailing comma after "scopes": {}
$content = $content -replace '("scopes"\s*:\s*\{\}),\s*', '$1 '

[IO.File]::WriteAllText($IndexHtmlPath, $content)

