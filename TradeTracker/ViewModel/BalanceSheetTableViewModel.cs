using System.Collections.ObjectModel;

namespace TradeTracker.ViewModel;

public class BalanceSheetTableViewModel : BindableObject {

    public BalanceSheetTableViewModel(){
        this.Rows = new ObservableCollection<BalanceSheetRowViewModel>();
    }

    #region Properties

    private string _currency;
    public string Currency {
        get { return this._currency; }
        set {
            this._currency = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<BalanceSheetRowViewModel> _rows;
    public ObservableCollection<BalanceSheetRowViewModel> Rows {
        get { return this._rows; }
        set {
            this._rows = value;
            OnPropertyChanged();
        }
    }

    private double _total;
    public double Total {
        get { return this._total; }
        set {
            this._total = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(TotalColour));
        }
    }

    public Color TotalColour {
        get { return this.Total < 0 ? Colors.Red : Colors.Black; }
    }

    #endregion

}