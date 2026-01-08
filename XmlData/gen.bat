set WORKSPACE=..
set LUBAN_DLL=%WORKSPACE%\Tools\Luban\Luban.dll
set OUT_DIR=%WORKSPACE%\Assets
set CONF_ROOT=.

dotnet %LUBAN_DLL% ^
    -t client ^
    -c cs-simple-json ^
    -d json ^
    --conf %CONF_ROOT%\luban.conf ^
    -x inputDataDir="%~dp0Datas" ^
    -x outputCodeDir="%OUT_DIR%\Scripts\Game\LubanGen\Scripts" ^
    -x outputDataDir="%OUT_DIR%\Tables\Data"^
pause