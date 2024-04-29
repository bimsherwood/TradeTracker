using System.Collections.ObjectModel;

namespace TradeTracker.ViewModel;

public class BalanceSheetRowViewModel : BindableObject {

    public BalanceSheetRowViewModel(string accountName, double balance) {
        AccountName = accountName;
        Balance = balance;
    }

    #region Properties

    private string _accountName { get; set; }
    public string AccountName {
        get { return this._accountName; }
        set {
            this._accountName = value;
            OnPropertyChanged();
        }
    }

    private double _balance;
    public double Balance {
        get { return this._balance; }
        set {
            this._balance = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Balance));
        }
    }
    
    public Color BalanceColour {
        get { return this.Balance < 0 ? Colors.Red : Colors.Black; }
    }

    #endregion

}