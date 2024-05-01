using TradeTracker.Services;
using TradeTracker.ViewModel;

namespace TradeTracker.Page;

public partial class Settings : ContentPage
{
	
    private BalanceSheetViewModel ViewModel;

	public Settings()
	{
		var database = DependencyService.Get<DataService>();
		InitializeComponent();
	}

}

