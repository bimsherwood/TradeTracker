using TradeTracker.DataModel;

namespace TradeTracker.Services;

public class DataService {

    private const string DatabaseFilename = "TradeTrackerSQLite.db3";

    private const SQLite.SQLiteOpenFlags Flags =
        SQLite.SQLiteOpenFlags.ReadWrite |
        SQLite.SQLiteOpenFlags.Create |
        SQLite.SQLiteOpenFlags.SharedCache;

    private static bool Initialised = false;

    private static async Task<SQLite.SQLiteAsyncConnection> Open(){
        var appData = FileSystem.Current.AppDataDirectory;
        if(!Directory.Exists(appData)){
            Directory.CreateDirectory(appData);
        }
        var databaseFile = Path.Combine(appData, DatabaseFilename);
        var db = new SQLite.SQLiteAsyncConnection(databaseFile, Flags);
        if (!Initialised){
            await db.CreateTableAsync<TransactionDataModel>();
            Initialised = true;
        }
        return db;
    }

    public async Task Operation(Func<SQLite.SQLiteAsyncConnection, Task> op){
        var db = await Open();
        try {
            await op(db); 
        } finally {
            await db.CloseAsync();
        }
    }

    public async Task<T> Operation<T>(Func<SQLite.SQLiteAsyncConnection, Task<T>> op){
        var db = await Open();
        try {
            return await op(db); 
        } finally {
            await db.CloseAsync();
        }
    }

}