using TradeTracker.Services;
using TradeTracker.ViewModel;

namespace TradeTracker;

public partial class BalanceSheet : ContentPage
{
	
    private BalanceSheetViewModel ViewModel;

	public BalanceSheet()
	{
		var database = DependencyService.Get<DataService>();
		this.ViewModel = new BalanceSheetViewModel(database);
		this.BindingContext = this.ViewModel;
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await this.ViewModel.RefreshBalanceSheet();
    }

}

