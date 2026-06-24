using System.Windows;
using Aqua.App.ViewModel;

namespace Aqua_app;

public partial class MainWindow : Window
{
	public MainWindow(MainViewModel viewModel)
	{
		DataContext = viewModel;

		InitializeComponent();

		Loaded += async (_, __) =>
		{
			await viewModel.EmployeeViewModel.LoadAsync();
		};
	}
}