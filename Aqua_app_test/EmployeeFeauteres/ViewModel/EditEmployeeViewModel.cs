using Aqua.Core.Contracts;
using Aqua.Core.Entity;
using Aqua.Core.Models;
using CommunityToolkit.Mvvm.Input;

namespace Aqua.App.ViewModel;

public class EditEmployeeViewModel : BaseViewModel
{
	public Array Positions { get; } = Enum.GetValues(typeof(PositionType));

	private int _id;

	private PositionType? _position;
	private string? _fullName;
	private DateTime? _birthDate;

	public PositionType? Position
	{
		get => _position;
		set
		{
			_position = value;

			OnPropertyChanged();
			SaveCommand.NotifyCanExecuteChanged();
		}
	}

	public string? FullName
	{
		get => _fullName;
		set
		{
			_fullName = value;

			OnPropertyChanged();
			SaveCommand.NotifyCanExecuteChanged();
		}
	}

	public DateTime? BirthDate
	{
		get => _birthDate;
		set
		{
			_birthDate = value;

			OnPropertyChanged();
			SaveCommand.NotifyCanExecuteChanged();
		}
	}

	public RelayCommand SaveCommand { get; }

	public EditEmployeeViewModel(Employee employee)
	{
		SaveCommand = new RelayCommand(Save, CanSave);

		_id = employee.Id;
		FullName = employee.FullName;
		Position = employee.Position;
		BirthDate = employee.BirthDate;
	}

	public Action<EmployeePatch> OnSave { get; set; }

	private void Save()
	{
		var employee = new EmployeePatch(_id, FullName, Position, BirthDate);

		OnSave?.Invoke(employee);
	}

	private bool CanSave()
	{
		if (BirthDate > DateTime.Now)
			return false;

		return true;
	}
}