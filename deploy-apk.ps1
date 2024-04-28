dotnet build --framework net7.0-android --configuration release;
Copy-Item "TradeTracker\bin\Release\net7.0-android\*-Signed.apk" "C:\inetpub\wwwroot\tradetracker.apk" -Force;
