using System.Collections.ObjectModel;
using System.Windows.Input;
using Aqua.App.EmployeeFeauters.View;
using Aqua.App.ViewModel;
using Aqua.Core.Abstract;
using Aqua.Core.Contracts;
using Aqua.Core.Entity;
using CommunityToolkit.Mvvm.Input;

namespace Aqua.App.EmployeeFeauters.ViewModel;

public class EmployeeViewModel : BaseViewModel
{
	private readonly IEmployeeRepository _emploeesRepository;

	public ObservableCollection<Employee> Employees { get; } = new();

	private Employee? _selectedEmployee;

	public Employee? SelectedEmployee
	{
		get => _selectedEmployee;
		set
		{
			_selectedEmployee = value;
			OnPropertyChanged();

			EditCommand.NotifyCanExecuteChanged();
			DeleteCommand.NotifyCanExecuteChanged();
		}
	}

	public ICommand LoadCommand { get; }
	public ICommand AddCommand { get; }
	public AsyncRelayCommand EditCommand { get; }
	public AsyncRelayCommand DeleteCommand { get; }

	public EmployeeViewModel(
		IEmployeeRepository emploeesRepository)
	{
		_emploeesRepository = emploeesRepository;

		LoadCommand = new AsyncRelayCommand(LoadAsync);
		AddCommand = new AsyncRelayCommand(AddAsync);
		EditCommand = new AsyncRelayCommand(EditAsync, CanEditOrDelete);
		DeleteCommand = new AsyncRelayCommand(DeleteAsync, CanEditOrDelete);
	}

	public override async Task LoadAsync()
	{
		Employees.Clear();

		var employees = await _emploeesRepository.GetAll();

		foreach (var employee in employees)
		{
			Employees.Add(employee);
		}
	}

	private async Task AddAsync()
	{
		var vm = new AddEmployeeViewModel();

		var window = new AddEmployeeWindow
		{
			DataContext = vm
		};

		Employee resultEmployee = null;

		vm.OnSave = (employee) =>
		{
			resultEmployee = employee;

			window.Close();
		};

		window.ShowDialog();

		if (resultEmployee != null)
		{
			await _emploeesRepository.Add(resultEmployee);
		}

		await LoadAsync();
	}

	private async Task EditAsync()
	{
		if (SelectedEmployee == null)
			return;

		var vm = new EditEmployeeViewModel(SelectedEmployee);

		var window = new EditEmployeeWindow
		{
			DataContext = vm
		};

		EmployeePatch result = null;

		vm.OnSave = (patch) =>
		{
			result = patch;

			window.Close();
		};

		window.ShowDialog();

		if (result == null)
			return;

		await _emploeesRepository.Patch(result);

		await LoadAsync();
	}

	private async Task DeleteAsync()
	{
		if (SelectedEmployee == null)
			return;

		await _emploeesRepository.Delete(SelectedEmployee.Id);

		await LoadAsync();
	}

	private bool CanEditOrDelete()
	{
		return SelectedEmployee != null;
	}
}