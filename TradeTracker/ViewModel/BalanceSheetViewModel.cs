using System.Collections.ObjectModel;

namespace TradeTracker.ViewModel;

public class BalanceSheetViewModel : BindableObject {

    public BalanceSheetViewModel() {
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

}