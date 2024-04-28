namespace TradeTracker;

public partial class App : Application
{
	public App()
	{
		UserAppTheme = AppTheme.Light;
		
		InitializeComponent();

		MainPage = new AppShell();
	}
}
