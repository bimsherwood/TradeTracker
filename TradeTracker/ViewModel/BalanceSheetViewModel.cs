using System.Collections.ObjectModel;
using System.Windows.Input;

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

    #region Commands

    public ICommand NewTradeCommand => new Command(NewTrade);
    private async void NewTrade(){
        await Shell.Current.GoToAsync("TradeEditor?id=new");
    }

    #endregion
}