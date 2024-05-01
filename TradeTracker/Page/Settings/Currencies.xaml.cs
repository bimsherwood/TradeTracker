using TradeTracker.Services;
using TradeTracker.ViewModel;

namespace TradeTracker.Page.Settings;

public partial class Currencies : ContentPage
{

	public Currencies()
	{
		var database = DependencyService.Get<DataService>();
		InitializeComponent();
	}

}

