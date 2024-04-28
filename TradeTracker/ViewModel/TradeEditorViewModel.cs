using System.Collections.ObjectModel;

namespace TradeTracker.ViewModel;

public class TradeEditorViewModel : BindableObject {

    public TradeEditorViewModel() {

    }

    #region Properties

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

    public void Clear(){

        this.Directions = new ObservableCollection<string>();
        this.Directions.Add("I Gave");
        this.Directions.Add("I Received From");
        this.Direction = "I Gave";

        this.Partners = new ObservableCollection<string>();
        this.Partners.Add("Bim Sherwood");
        this.Partners.Add("Gumbo");
        this.SelectedPartner = "Bim Sherwood";

        this.Currencies = new ObservableCollection<string>();
        this.Currencies.Add("AUD");
        this.Currencies.Add("USD");
        this.Currencies.Add("EUR");
        this.SelectedCurrency = "AUD";

        this.Date = DateTime.Today.AddDays(-1);
        this.Description = "Wow, a thing!";

    }

}