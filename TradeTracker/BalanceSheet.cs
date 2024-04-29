using TradeTracker.ViewModel;

namespace TradeTracker;

public partial class BalanceSheet : ContentPage
{
	
	public BalanceSheet()
	{
		var viewModel = new BalanceSheetViewModel();

        var exampleAUD = new BalanceSheetTableViewModel();
        exampleAUD.Rows.Add(new BalanceSheetRowViewModel("Bim Sherwood", 14));
        exampleAUD.Rows.Add(new BalanceSheetRowViewModel("Libniz", 30));
        exampleAUD.Rows.Add(new BalanceSheetRowViewModel("Grumbo", -123));
        exampleAUD.Currency = "AUD";
        exampleAUD.Total = -79;
        viewModel.BalanceSheets.Add(exampleAUD);

        var exampleUSD = new BalanceSheetTableViewModel();
        exampleUSD.Rows.Add(new BalanceSheetRowViewModel("Bim Sherwood", 25));
        exampleUSD.Rows.Add(new BalanceSheetRowViewModel("Libniz", 30));
        exampleUSD.Currency = "USD";
        exampleUSD.Total = 55;
        viewModel.BalanceSheets.Add(exampleUSD);

		this.BindingContext = viewModel;

		InitializeComponent();
	}

}

