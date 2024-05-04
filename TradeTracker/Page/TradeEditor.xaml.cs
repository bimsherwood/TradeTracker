using TradeTracker.Services;
using TradeTracker.ViewModel;

namespace TradeTracker.Page;

public partial class TradeEditor : ContentPage
{
	
	private TradeEditorViewModel ViewModel;

	public TradeEditor()
	{
		var database = DependencyService.Get<DataService>();
		var photoService = DependencyService.Get<PhotoService>();
		var fileService = DependencyService.Get<FileService>();
		this.ViewModel = new TradeEditorViewModel(database, photoService, fileService);
		this.BindingContext = this.ViewModel;
		InitializeComponent();
	}

}

