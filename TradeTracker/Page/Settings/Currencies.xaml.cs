using TradeTracker.Services;
using TradeTracker.ViewModel;

namespace TradeTracker.Page.Settings;

public partial class Currencies : ContentPage
{

	private CurrenciesViewModel ViewModel;

	public Currencies()
	{
		var database = DependencyService.Get<DataService>();
		this.ViewModel = new CurrenciesViewModel(database);
		this.BindingContext = this.ViewModel;
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await this.ViewModel.RefreshList();
    }

}

