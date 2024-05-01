
using System.Collections.ObjectModel;
using System.Windows.Input;
using TradeTracker.DataModel;
using TradeTracker.Services;

namespace TradeTracker.ViewModel;

public class CurrenciesViewModel : BindableObject {

    private DataService Database;

    public CurrenciesViewModel(DataService database) {
        this.Database = database;
        this.Currencies = new ObservableCollection<string>();
    }

    #region Properties

    private ObservableCollection<string> _currencies;
    public ObservableCollection<string> Currencies {
        get { return this._currencies;}
        set {
            this._currencies = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Command

    public ICommand NewSettingCommand => new Command(NewSetting);
    private async void NewSetting() {
        string currencyName = await App.Current.MainPage.DisplayPromptAsync("New Currency", "Currency Name:");
        var currency = new CurrencyDataModel();
        currency.Name = currencyName;
        await this.Database.Operation(async db => await db.InsertOrReplaceAsync(currency)); 
        await RefreshList();
    }

    #endregion

    public async Task RefreshList() {
        var currencies = await this.Database.Operation(async db =>
            await db.Table<CurrencyDataModel>()
            .OrderBy(o => o.Name)
            .ToListAsync());
        var names = currencies.Select(o => o.Name);
        this.Currencies = new ObservableCollection<string>(names);
    }

}