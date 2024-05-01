using TradeTracker.Services;
using TradeTracker.ViewModel;

namespace TradeTracker.Page.Settings;

public partial class Partners : ContentPage
{

	public Partners()
	{
		var database = DependencyService.Get<DataService>();
		InitializeComponent();
	}

}

