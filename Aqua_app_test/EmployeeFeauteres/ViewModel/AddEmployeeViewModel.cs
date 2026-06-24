using System.Windows.Input;
using Aqua.Core.Entity;
using Aqua.Core.Models;
using CommunityToolkit.Mvvm.Input;

namespace Aqua.App.ViewModel;

public class AddEmployeeViewModel : BaseViewModel
{
	public Array Positions { get; } = Enum.GetValues(typeof(PositionType));

	public PositionType? Position { get; set; }
	public string? FullName { get; set; }
	public DateTime? BirthDate { get; set; }

	public ICommand SaveCommand { get; }

	public AddEmployeeViewModel()
	{
		SaveCommand = new RelayCommand(Save, CanSave);
	}

	public Action<Employee> OnSave { get; set; }

	private void Save()
	{
		var employee = new Employee
		{
			FullName = FullName,
			Position = Position.Value,
			BirthDate = BirthDate.Value
		};

		OnSave?.Invoke(employee);
	}

	private bool CanSave()
	{
		if (BirthDate == default || BirthDate == null)
			return false;

		if (BirthDate > DateTime.Now)
			return false;

		return true;
	}
}