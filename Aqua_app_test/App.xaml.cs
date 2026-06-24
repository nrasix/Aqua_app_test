using System.Windows;
using Aqua.App.EmployeeFeauters.ViewModel;
using Aqua.App.View;
using Aqua.App.ViewModel;
using Aqua.Core.Abstract;
using Aqua.DataAccess;
using AquaApp.Repositories;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

namespace Aqua_app;

public partial class App : Application
{
	private static string _connectionString =
		"Server=localhost;Port=3306;Database=mysql_db;User=user;Password=user1234;";

	private IServiceProvider _serviceProvider;

	protected override void OnStartup(StartupEventArgs e)
	{
		var serviceCollection = new ServiceCollection();
		ConfigureServices(serviceCollection);

		_serviceProvider = serviceCollection.BuildServiceProvider();

		var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();

		mainWindow.Show();
	}

	private void ConfigureServices(IServiceCollection services)
	{
		services.AddSingleton<ISessionFactory>(_ => NHibernateFactory.GetSessionFactory(_connectionString));

		services.AddTransient<IOrderRepository, OrderRepository>();
		services.AddTransient<IEmployeeRepository, EmploeesRepository>();
		services.AddTransient<IContractorRepository, ContractorRepository>();

		services.AddTransient<EmployeeViewModel>();
		services.AddTransient<EmployeeView>();

		services.AddTransient<MainViewModel>();
		services.AddTransient<MainWindow>();
	}

	private void OnExit(object sender, ExitEventArgs e)
	{
		if (_serviceProvider is IDisposable disposable)
		{
			disposable.Dispose();
		}
	}
}