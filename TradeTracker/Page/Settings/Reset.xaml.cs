using TradeTracker.Services;
using TradeTracker.ViewModel;

namespace TradeTracker.Page.Settings;

public partial class Reset : ContentPage
{

	private ResetViewModel ViewModel;

	public Reset()
	{
		var database = DependencyService.Get<DataService>();
		var files = DependencyService.Get<FileService>();
		var csvService = DependencyService.Get<CsvService>();
		this.ViewModel = new ResetViewModel(database, files, csvService);
		this.BindingContext = this.ViewModel;
		InitializeComponent();
	}

}

