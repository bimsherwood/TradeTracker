using TradeTracker.ViewModel;

namespace TradeTracker;

public partial class TradeEditor : ContentPage
{
	
	private TradeEditorViewModel ViewModel;

	public TradeEditor()
	{
		this.ViewModel = new TradeEditorViewModel();
		this.BindingContext = this.ViewModel;
		InitializeComponent();
		this.ViewModel.Clear();
	}

}

