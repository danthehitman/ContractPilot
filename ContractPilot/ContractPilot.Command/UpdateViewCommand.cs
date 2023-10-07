using System;
using System.Windows.Input;
using ContractPilot.ViewModel;

namespace ContractPilot.Command;

public class UpdateViewCommand : ICommand
{
	private MainWindowViewModel viewModel;

	public event EventHandler CanExecuteChanged;

	public UpdateViewCommand(MainWindowViewModel viewModel)
	{
		this.viewModel = viewModel;
	}

	public bool CanExecute(object parameter)
	{
		return true;
	}

	public void Execute(object parameter)
	{
		if (parameter.ToString() == "Map")
		{
			viewModel.ActiveViewModel = viewModel.MapViewModel;
		}
		else if (parameter.ToString() == "Status")
		{
			viewModel.ActiveViewModel = viewModel.StatusViewModel;
		}
		else if (parameter.ToString() == "AddAirplaneMakeModelInfo")
		{
			viewModel.ActiveViewModel = viewModel.AirplaneMakeModelInfoEditorViewModel;
		}
	}
}
