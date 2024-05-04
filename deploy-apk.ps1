dotnet build --framework net8.0-android --configuration release;
Copy-Item "TradeTracker\bin\Release\net8.0-android\*-Signed.apk" "C:\inetpub\wwwroot\tradetracker.apk" -Force;
