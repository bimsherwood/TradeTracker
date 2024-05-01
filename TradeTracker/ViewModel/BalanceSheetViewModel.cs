using System.Collections.ObjectModel;
using System.Windows.Input;
using TradeTracker.DataModel;
using TradeTracker.Services;

namespace TradeTracker.ViewModel;

public class BalanceSheetViewModel : BindableObject {

    private DataService DataService;

    public BalanceSheetViewModel(DataService dataService) {
        this.DataService = dataService;
        this.BalanceSheets = new ObservableCollection<BalanceSheetTableViewModel>();
    }

    #region Properties

    private ObservableCollection<BalanceSheetTableViewModel> _balanceSheets;
    public ObservableCollection<BalanceSheetTableViewModel> BalanceSheets {
        get { return this._balanceSheets; }
        set {
            this._balanceSheets = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Commands

    public ICommand NewTradeCommand => new Command(NewTrade);
    private async void NewTrade(){
        await Shell.Current.GoToAsync("TradeEditor?id=new");
    }

    #endregion

    public async Task RefreshBalanceSheet(){
        
        var partners = await this.DataService.Operation(async db =>
            await db.Table<PartnerDataModel>()
            .OrderBy(o => o.Name)
            .ToListAsync());
        var partnerNames = partners.Select(o => o.Name).ToList();
        var currencies = await this.DataService.Operation(async db =>
            await db.Table<CurrencyDataModel>()
            .OrderBy(o => o.Name)
            .ToListAsync());
        var currencyNames = currencies.Select(o => o.Name).ToList();

        var sheets = new List<BalanceSheetTableViewModel>();
        foreach(var currency in currencyNames){
            var rows = new List<BalanceSheetRowViewModel>();
            foreach(var partner in partnerNames){
                var balance = await this.DataService.Operation(async db =>
                    await db.ExecuteScalarAsync<double>(
                        "SELECT IFNULL(SUM(Price), 0) FROM TradeTransaction WHERE Currency = ? AND Partner = ?",
                        currency,
                        partner));
                var row = new BalanceSheetRowViewModel(partner, balance);
                rows.Add(row);
            }
            var sheet = new BalanceSheetTableViewModel(currency, rows);
            sheets.Add(sheet);
        }
        this.BalanceSheets = new ObservableCollection<BalanceSheetTableViewModel>(sheets);

    }

}