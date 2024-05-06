
using System.Windows.Input;
using CommunityToolkit.Maui.Storage;
using TradeTracker.DataModel;
using TradeTracker.Services;

namespace TradeTracker.ViewModel;

public class ResetViewModel : BindableObject {

    private DataService Database;
    private FileService FileService;
    private CsvService CsvService;

    public ResetViewModel(DataService database, FileService fileService, CsvService csvService) {
        this.Database = database;
        this.FileService = fileService;
        this.CsvService = csvService;
    }

    #region Properties

    #endregion

    #region Command

    public ICommand ResetDatabaseCommand => new Command(ResetDatabase);
    private async void ResetDatabase() {
        var accepted = await App.Current.MainPage.DisplayAlert(
            "Reset Database",
            "Are you sure you want to delete everything and reset the database?",
            "Delete Everything",
            "Cancel");
        if(accepted) {
            await this.Database.Operation(async db => {
                await db.DeleteAllAsync<TransactionDataModel>();
                await db.DeleteAllAsync<PartnerDataModel>();
                await db.DeleteAllAsync<CurrencyDataModel>();
            });
            this.FileService.DeleteAllTradePhotos();
            await App.Current.MainPage.DisplayAlert("Complete", "Data deleted successfully.", "OK");
        }
    }
    
    public ICommand ExportDatabaseCommand => new Command(ExportDatabase);
    private async void ExportDatabase() {
        var accepted = await App.Current.MainPage.DisplayAlert(
            "Export Database",
            "Are you sure you want to export all transactions?",
            "Export Transactions",
            "Cancel");
        if(accepted) {
            var currencies = await this.Database.Operation(async db => await db.Table<CurrencyDataModel>().ToListAsync());
            var partners = await this.Database.Operation(async db => await db.Table<PartnerDataModel>().ToListAsync());
            var exportFilePath = this.FileService.CsvExportFile();
            await ExportCsv(currencies, partners, exportFilePath);
            var success = await SaveCsv(exportFilePath);
            File.Delete(exportFilePath);
            if(success) {
                await App.Current.MainPage.DisplayAlert("Complete", "Data exported successfully.", "OK");
            } else {
                await App.Current.MainPage.DisplayAlert("Failed", "Data exported failed!", "Darn");
            }
        }
    }

    #endregion

    private async Task ExportCsv(
            IEnumerable<CurrencyDataModel> currencies,
            IEnumerable<PartnerDataModel> partners,
            string exportFilePath){
        using var exportStream = File.OpenWrite(exportFilePath);
        using var exportWriter = new StreamWriter(exportStream);
        await this.CsvService.WriteHeader(exportWriter);
        foreach(var currency in currencies) {
            foreach(var partner in partners) {
                var transactions = await this.Database.Operation(async db =>
                    await db.Table<TransactionDataModel>()
                    .Where(o => o.Currency == currency.Name)
                    .Where(o => o.Partner == partner.Name)
                    .OrderBy(o => o.Date)
                    .ToListAsync());
                await this.CsvService.Export(transactions, exportWriter);
            }
        }
    }

    private async Task<bool> SaveCsv(string exportFilePath) {
        using var exportStream = File.OpenRead(exportFilePath);
        var result = await FileSaver.Default.SaveAsync($"tradetracker-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.csv", exportStream);
        return result.IsSuccessful;
    }

}