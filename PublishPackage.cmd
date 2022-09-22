@ECHO OFF
IF "%~2"=="" (
   ECHO Syntax: %~n0 ^<packagename^> ^<version^>
   ECHO I.e:    %~n0 My.Package 1.0.0
   EXIT /B
)
dotnet nuget push %~1\bin\Release\%~1.%~2.nupkg --api-key %NUGET.CODETUNER.APIKEY% --source https://api.nuget.org/v3/index.json