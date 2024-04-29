using TradeTracker.ViewModel;

namespace TradeTracker;

public partial class BalanceSheetTable : ContentView
{
	
	public BalanceSheetTable() {
		this.BindingContext = new BalanceSheetViewModel();
		InitializeComponent();
	}

}

