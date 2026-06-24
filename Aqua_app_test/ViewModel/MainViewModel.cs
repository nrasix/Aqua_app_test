using Aqua.App.EmployeeFeauters.ViewModel;

namespace Aqua.App.ViewModel;

public class MainViewModel : BaseViewModel
{
	public EmployeeViewModel EmployeeViewModel { get; }

	public MainViewModel(
		EmployeeViewModel employeeViewModel
	)
	{
		EmployeeViewModel = employeeViewModel;
	}
}