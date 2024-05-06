using TradeTracker.Services;
using TradeTracker.ViewModel;

namespace TradeTracker.Page.Settings;

public partial class Buttons : ContentPage
{

	private ResetViewModel ViewModel;

	public Buttons()
	{
		var database = DependencyService.Get<DataService>();
		var files = DependencyService.Get<FileService>();
		var csvService = DependencyService.Get<CsvService>();
		this.ViewModel = new ResetViewModel(database, files, csvService);
		this.BindingContext = this.ViewModel;
		InitializeComponent();
	}

}

