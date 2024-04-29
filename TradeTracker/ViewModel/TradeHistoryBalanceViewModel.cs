using System.Collections.ObjectModel;

namespace TradeTracker.ViewModel;

public class TradeHistoryBalanceViewModel : BindableObject {
    
    public TradeHistoryBalanceViewModel() {
        
    }
    
    #region Properties
    
    private string _currency;
    public string Currency {
        get { return this._currency; }
        set {
            this._currency = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(AbsoluteBalance));
        }
    }


    private double _balance;
    public double Balance {
        get { return this._balance; }
        set {
            this._balance = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(AbsoluteBalance));
        }
    }

    public string Description {
        get { return this._balance < 0 ? "I am owed" : "I owe"; }
    }

    public string AbsoluteBalance {
        get { return $"{Math.Abs(this._balance):F2} {this._currency}"; }
    }

    #endregion

}