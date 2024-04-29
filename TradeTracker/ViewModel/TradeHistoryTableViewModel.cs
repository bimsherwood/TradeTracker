using System.Collections.ObjectModel;

namespace TradeTracker.ViewModel;

public class TradeHistoryTableViewModel : BindableObject {
    
    public TradeHistoryTableViewModel(){
        this.Transactions = new ObservableCollection<TradeHistoryRowViewModel>();
    }
    
    #region Properties
    
    private string _currency { get; set; }
    public string Currency {
        get { return this._currency; }
        set {
            this._currency = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<TradeHistoryRowViewModel> _transactions;
    public ObservableCollection<TradeHistoryRowViewModel> Transactions {
        get { return this._transactions; }
        set {
            this._transactions = value;
            OnPropertyChanged();
        }
    }

    #endregion

}