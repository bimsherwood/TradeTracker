
using System.Windows.Input;
using TradeTracker.DataModel;
using TradeTracker.Services;

namespace TradeTracker.ViewModel;

public class ResetViewModel : BindableObject {

    private DataService Database;

    public ResetViewModel(DataService database) {
        this.Database = database;
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
        }
    }

    #endregion

}