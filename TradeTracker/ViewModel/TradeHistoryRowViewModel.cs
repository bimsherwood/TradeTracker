using System.Windows.Input;
using TradeTracker.DataModel;

namespace TradeTracker.ViewModel;

public class TradeHistoryRowViewModel : BindableObject {
    
    private TransactionDataModel Transaction;

    public TradeHistoryRowViewModel(TransactionDataModel transaction, double runningBalance) {
        this.Transaction = transaction;
        this.Date = transaction.Date;
        this.Price = transaction.Price;
        this.Description = transaction.Description;
        this.Balance = runningBalance;
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
    
    private double _price;
    public double Price {
        get { return this._price; }
        set {
            this._price = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(PriceColour));
        }
    }

    public Color PriceColour {
        get { return this.Price < 0 ? Colors.Red : Colors.Black; }
    }
    
    private double _balance;
    public double Balance {
        get { return this._balance; }
        set {
            this._balance = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(BalanceColour));
        }
    }

    public Color BalanceColour {
        get { return this.Balance < 0 ? Colors.Red : Colors.Black; }
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

    public ICommand TradeTappedCommand => new Command(TradeTapped);
    private async void TradeTapped() {
        await Shell.Current.GoToAsync($"TradeEditor?id={this.Transaction.Id}");
    }

    #endregion

}