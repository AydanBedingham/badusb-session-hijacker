setlocal
cd /d %~dp0

FOR /D %%F IN (dumps\*) DO (
 set firstDirName=%%F
 goto tests
)
:tests
echo "%firstDirName%"

ChromeCookieImporter "%LOCALAPPDATA%\Google\Chrome\User Data\Default\Cookies" "%cd%%firstDirName%\CookieDump.txt"
start chrome https://www.facebook.com
PAUSE