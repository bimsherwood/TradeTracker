using TradeTracker.ViewModel;

namespace TradeTracker;

public partial class TradeHistory : ContentPage
{
	
	public TradeHistory()
	{
		var viewModel = new TradeHistoryViewModel();

		viewModel.PartnerOptions.Add("Bim Sherwood");
		viewModel.PartnerOptions.Add("Grumbo");

		viewModel.Balances.Add(new TradeHistoryBalanceViewModel(){ Balance = 14, Currency = "AUD" });
		viewModel.Balances.Add(new TradeHistoryBalanceViewModel(){ Balance = -234.56, Currency = "USD" });
		viewModel.Balances.Add(new TradeHistoryBalanceViewModel(){ Balance = 345.67, Currency = "EUR" });

		var tableAUD = new TradeHistoryTableViewModel();
		tableAUD.Currency = "AUD";
		tableAUD.Transactions.Add(new TradeHistoryRowViewModel(){ Date = DateTime.Today, Price=-6, Balance = -16, Description = "Island x4" });
		tableAUD.Transactions.Add(new TradeHistoryRowViewModel(){ Date = DateTime.Today, Price=-31, Balance = -10, Description = "Concordant Crossroads" });
		tableAUD.Transactions.Add(new TradeHistoryRowViewModel(){ Date = DateTime.Today, Price=21, Balance = 21, Description = "Sensei's Divining Top" });
		viewModel.Tables.Add(tableAUD);

		var tableUSD = new TradeHistoryTableViewModel();
		tableUSD.Currency = "USD";
		tableUSD.Transactions.Add(new TradeHistoryRowViewModel(){ Date = DateTime.Today, Price=-6, Balance = -16, Description = "Island x4" });
		tableUSD.Transactions.Add(new TradeHistoryRowViewModel(){ Date = DateTime.Today, Price=-31, Balance = -10, Description = "Concordant Crossroads" });
		tableUSD.Transactions.Add(new TradeHistoryRowViewModel(){ Date = DateTime.Today, Price=21, Balance = 21, Description = "Sensei's Divining Top" });
		viewModel.Tables.Add(tableUSD);

		this.BindingContext = viewModel;
		
		InitializeComponent();
		viewModel.PartnerSelected = "Bim Sherwood";
		
	}

}

