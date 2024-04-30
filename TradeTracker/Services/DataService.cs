using TradeTracker.DataModel;

namespace TradeTracker.Services;

public class DataService {

    private const string DatabaseFilename = "TradeTrackerSQLite.db3";

    private const SQLite.SQLiteOpenFlags Flags =
        SQLite.SQLiteOpenFlags.ReadWrite |
        SQLite.SQLiteOpenFlags.Create |
        SQLite.SQLiteOpenFlags.SharedCache;

    private SQLite.SQLiteAsyncConnection Connection;

    private async Task<SQLite.SQLiteAsyncConnection> Open(){
        var appData = FileSystem.Current.AppDataDirectory;
        if(!Directory.Exists(appData)){
            Directory.CreateDirectory(appData);
        }
        var databaseFile = Path.Combine(appData, DatabaseFilename);
        if (this.Connection == null){
            this.Connection = new SQLite.SQLiteAsyncConnection(databaseFile, Flags);
            await this.Connection.CreateTableAsync<TransactionDataModel>();
        }
        return this.Connection;
    }

    public async Task Operation(Func<SQLite.SQLiteAsyncConnection, Task> op){
        var db = await Open();
        await op(db); 
        await db.CloseAsync();
    }

    public async Task<T> Operation<T>(Func<SQLite.SQLiteAsyncConnection, Task<T>> op){
        var db = await Open();
        var result = await op(db); 
        await db.CloseAsync();
        return result;
    }

}