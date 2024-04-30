using System.Collections.ObjectModel;
using System.Windows.Input;
using TradeTracker.DataModel;
using TradeTracker.Services;

namespace TradeTracker.ViewModel;

public class TradeHistoryViewModel : BindableObject, IQueryAttributable {
    
    private string PartnerLoaded;
    private DataService DataService;

    public TradeHistoryViewModel(DataService dataService){
        this.PartnerLoaded = null;
        this.DataService = dataService;
        this.PartnerOptions = new ObservableCollection<string>();
        this.Balances = new ObservableCollection<TradeHistoryBalanceViewModel>();
        this.Tables = new ObservableCollection<TradeHistoryTableViewModel>();
    }
    
    #region Properties
    
    private ObservableCollection<string> _partnerOptions;
    public ObservableCollection<string> PartnerOptions {
        get { return this._partnerOptions; }
        set {
            this._partnerOptions = value;
            OnPropertyChanged();
        }
    }

    private string _partnerSelected;
    public string PartnerSelected {
        get { return this._partnerSelected; }
        set {
            this._partnerSelected = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<TradeHistoryBalanceViewModel> _balances;
    public ObservableCollection<TradeHistoryBalanceViewModel> Balances {
        get { return this._balances; }
        set {
            this._balances = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<TradeHistoryTableViewModel> _tables;
    public ObservableCollection<TradeHistoryTableViewModel> Tables {
        get { return this._tables; }
        set {
            this._tables = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Commands

    public ICommand NewTradeCommand => new Command(NewTrade);
    private async void NewTrade() {
        await Shell.Current.GoToAsync("TradeEditor?id=new");
    }

    #endregion

    public void ApplyQueryAttributes(IDictionary<string, object> query) {
        // Indicate that no data is loaded to force a refresh
        this.PartnerLoaded = null;
        this.PartnerSelected = query["partner"] as string;
    }

    public async Task OnScreenAppearing() {
        // TODO: Load these
        var partners = new List<string>() { "Bim Sherwood", "Grumbo", "Libniz" };
        this.PartnerOptions = new ObservableCollection<string>(partners);
        // The selection is cleared by the above operation, undo this: 
        this.PartnerSelected = this.PartnerLoaded ?? partners.First();
    }

    public async Task OnPartnerChanged() {
        // Do not load null partner data
        if(this.PartnerSelected != null) {
            await LoadPartnerData(this.PartnerSelected);
        }
    }
    
    private async Task LoadPartnerData(string partner) {

        // Do not reload the partner data if it is already loaded.
        if(this.PartnerLoaded == partner){ 
            return;
        } else {
            this.PartnerLoaded = partner;
        }

        // TODO: Load this
        var currencies = new List<string>() { "AUD", "USD", "EUR" };

        var balances = new List<TradeHistoryBalanceViewModel>();
        var tables = new List<TradeHistoryTableViewModel>();
        foreach(var currency in currencies) {

            // Load the balance
            var balance = await this.DataService.Operation(async db =>
                await db.ExecuteScalarAsync<double>(
                    "SELECT IFNULL(SUM(Price), 0) FROM TradeTransaction WHERE Currency = ? AND Partner = ?",
                    currency,
                    this.PartnerSelected));
            var balanceViewModel = new TradeHistoryBalanceViewModel(currency, balance);
            balances.Add(balanceViewModel);

            // Load recent transactions
            var rows = await this.DataService.Operation(async db =>
                await db.Table<TransactionDataModel>()
                .Where(o => o.Currency == currency && o.Partner == this.PartnerSelected)
                .OrderByDescending(o => o.Date)
                .Take(10)
                .ToListAsync());
            rows.Reverse();
            
            // Calculate the opening balance at the bottom of the history
            var openingBalance = balance - rows.Sum(o => o.Price);

            // Calculate a running balance
            var runningBalance = openingBalance;
            var rowViewModels = new List<TradeHistoryRowViewModel>();
            foreach(var row in rows){
                runningBalance += row.Price;
                var rowViewModel = new TradeHistoryRowViewModel(row, runningBalance);
                rowViewModels.Add(rowViewModel);
            }
            rowViewModels.Reverse();

            // Add the table
            var table = new TradeHistoryTableViewModel(currency, rowViewModels);
            tables.Add(table);

        }

        this.Balances = new ObservableCollection<TradeHistoryBalanceViewModel>(balances);
        this.Tables = new ObservableCollection<TradeHistoryTableViewModel>(tables);

    }

}