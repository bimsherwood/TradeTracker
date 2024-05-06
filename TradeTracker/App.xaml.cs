using TradeTracker.Services;

namespace TradeTracker;

public partial class App : Application
{
	public App()
	{
		UserAppTheme = AppTheme.Light;
		DependencyService.RegisterSingleton(new DataService());
		DependencyService.RegisterSingleton(new PhotoService());
		DependencyService.RegisterSingleton(new FileService());
		DependencyService.RegisterSingleton(new CsvService());
		InitializeComponent();
		MainPage = new AppShell();
	}
}
