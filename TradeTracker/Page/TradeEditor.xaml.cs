using TradeTracker.Services;
using TradeTracker.ViewModel;

namespace TradeTracker.Page;

public partial class TradeEditor : ContentPage
{
	
	private TradeEditorViewModel ViewModel;

	public TradeEditor()
	{
		var database = DependencyService.Get<DataService>();
		this.ViewModel = new TradeEditorViewModel(database);
		this.BindingContext = this.ViewModel;
		InitializeComponent();
	}

}

