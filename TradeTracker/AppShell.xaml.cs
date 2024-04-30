namespace TradeTracker;

public partial class AppShell : Shell
{
	public AppShell()
	{
		Routing.RegisterRoute("TradeEditor", typeof(TradeEditor));
		InitializeComponent();
	}
}
