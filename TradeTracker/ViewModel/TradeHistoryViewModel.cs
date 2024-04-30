using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TradeTracker.ViewModel;

public class TradeHistoryViewModel : BindableObject, IQueryAttributable {
    
    public TradeHistoryViewModel(){
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
    private async void NewTrade(){
        await Shell.Current.GoToAsync("TradeEditor?id=new");
    }

    #endregion

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        this.PartnerSelected = query["partner"] as string;
    }

}