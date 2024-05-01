using System.Collections.ObjectModel;
using System.Windows.Input;
using TradeTracker.DataModel;
using TradeTracker.Services;

namespace TradeTracker.ViewModel;

public class TradeEditorViewModel : BindableObject, IQueryAttributable {

    private const string IGave = "I Gave";
    private const string IReceivedFrom = "I Received From";

    private DataService DataService;
    private TransactionDataModel Transaction;

    public TradeEditorViewModel(DataService dataService)
    {
        this.DataService = dataService;
        this.Transaction = null;
    }

    #region Properties

    private string _title;
    public string Title {
        get { return this._title; }
        set {
            this._title = value;
            OnPropertyChanged();
        }
    }

    private DateTime _date;
    public DateTime Date {
        get { return this._date; }
        set {
            this._date = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<string> _directions;
    public ObservableCollection<string> Directions {
        get { return this._directions; }
        set {
            this._directions = value;
            OnPropertyChanged();
        }
    }

    private string _direction;
    public string Direction {
        get { return this._direction; }
        set {
            this._direction = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<string> _partners;
    public ObservableCollection<string> Partners {
        get { return this._partners; }
        set {
            this._partners = value;
            OnPropertyChanged();
        }
    }

    private string _selectedPartner;
    public string SelectedPartner {
        get { return this._selectedPartner; }
        set {
            this._selectedPartner = value;
            OnPropertyChanged();
        }
    }
    
    private string _price;
    public string Price {
        get { return this._price; }
        set {
            this._price = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<string> _currencies;
    public ObservableCollection<string> Currencies {
        get { return this._currencies; }
        set {
            this._currencies = value;
            OnPropertyChanged();
        }
    }

    private string _selectedCurrency;
    public string SelectedCurrency {
        get { return this._selectedCurrency; }
        set {
            this._selectedCurrency = value;
            OnPropertyChanged();
        }
    }
    
    private string _description;
    public string Description {
        get { return this._description; }
        set {
            this._description = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Commands

    public ICommand SaveCommand => new Command(Save);
    private async void Save(){

        var price = double.Parse(this.Price);
        if(this.Direction == IGave){
            price *= -1;
        }

        this.Transaction.Date = this.Date;
        this.Transaction.Partner = this.SelectedPartner;
        this.Transaction.Price = price;
        this.Transaction.Currency = this.SelectedCurrency;
        this.Transaction.Description = this.Description;

        await this.DataService.Operation(async db => {
            if(this.Transaction.Id == 0){
                await db.InsertAsync(this.Transaction);
            } else {
                await db.UpdateAsync(this.Transaction);
            }
        });

        await Shell.Current.GoToAsync($"..", new Dictionary<string, object>{ { "partner", this.SelectedPartner } });

    }

    #endregion

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {

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

        var transaction = await LoadTransaction(query);
        UpdateOptions(partnerNames, currencyNames);
        if(transaction != null){
            LoadTransaction(transaction);
        } else {
            LoadBlankTransaction();
        }
    }

    private async Task<TransactionDataModel> LoadTransaction(IDictionary<string, object> query){
        var idArg = query["id"] as string;
        if(int.TryParse(idArg, out int id)){
            var transactions = await this.DataService.Operation(async db =>
                await db.Table<TransactionDataModel>()
                .Where(o => o.Id == id)
                .ToListAsync());
            return transactions.SingleOrDefault();
        } else {
            return null;
        }
    }

    private void UpdateOptions(
            List<string> partners,
            List<string> currencies){
        this.Directions = new ObservableCollection<string>(new[]{ IGave, IReceivedFrom });
        this.Partners = new ObservableCollection<string>(partners);
        this.Currencies = new ObservableCollection<string>(currencies);
    }

    private void LoadBlankTransaction(){

        this.Title = "New Trade";

        this.Transaction = new TransactionDataModel();

        this.Direction = IGave;
        this.SelectedPartner = this.Partners.FirstOrDefault();
        this.Price = "0.00";
        this.SelectedCurrency = this.Currencies.FirstOrDefault();
        this.Date = DateTime.Today;
        this.Description = "";

    }

    private void LoadTransaction(TransactionDataModel transaction){

        this.Title = "Edit Trade";

        this.Transaction = transaction;

        this.Date = transaction.Date;
        this.Direction = transaction.Price < 0 ? IGave : IReceivedFrom;
        this.SelectedPartner = transaction.Partner;
        this.Price = Math.Abs(transaction.Price).ToString("F2");
        this.SelectedCurrency = transaction.Currency;
        this.Description = transaction.Description;

    }

}