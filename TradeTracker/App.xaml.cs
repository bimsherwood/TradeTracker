using TradeTracker.Services;

namespace TradeTracker;

public partial class App : Application
{
	public App()
	{
		UserAppTheme = AppTheme.Light;
		DependencyService.RegisterSingleton(new DataService());
		InitializeComponent();
		MainPage = new AppShell();
	}
}
