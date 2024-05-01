using TradeTracker.Services;
using TradeTracker.ViewModel;

namespace TradeTracker.Page;

public partial class TradeHistory : ContentPage
{
	
	private TradeHistoryViewModel ViewModel;

	public TradeHistory()
	{
		var database = DependencyService.Get<DataService>();
		this.ViewModel = new TradeHistoryViewModel(database);
		this.BindingContext = this.ViewModel;
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await this.ViewModel.OnScreenAppearing();
    }

	private async void PartnerSelectionChanged(object sender, EventArgs e){
		await this.ViewModel.OnPartnerChanged();
	}

}

