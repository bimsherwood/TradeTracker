using TradeTracker.Services;
using TradeTracker.ViewModel;

namespace TradeTracker.Page.Settings;

public partial class Partners : ContentPage
{

	private PartnersViewModel ViewModel;

	public Partners()
	{
		var database = DependencyService.Get<DataService>();
		this.ViewModel = new PartnersViewModel(database);
		this.BindingContext = this.ViewModel;
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await this.ViewModel.RefreshList();
    }

}

