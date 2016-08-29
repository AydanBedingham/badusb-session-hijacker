setlocal
cd /d %~dp0
ChromeCookieImporter "%LOCALAPPDATA%\Google\Chrome\User Data\Default\Cookies" %1
start chrome https://www.facebook.com
PAUSE