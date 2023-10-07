using System;
using System.Windows.Input;

namespace ContractPilot.Command;

public class RelayCommand : ICommand
{
	private Action<object> execute;

	private Predicate<object> canExecute;

	private event EventHandler CanExecuteChangedInternal;

	public event EventHandler CanExecuteChanged
	{
		add
		{
			CommandManager.RequerySuggested += value;
			CanExecuteChangedInternal += value;
		}
		remove
		{
			CommandManager.RequerySuggested -= value;
			CanExecuteChangedInternal -= value;
		}
	}

	public RelayCommand(Action<object> execute)
		: this(execute, DefaultCanExecute)
	{
	}

	public RelayCommand(Action<object> execute, Predicate<object> canExecute)
	{
		if (execute == null)
		{
			throw new ArgumentNullException("execute");
		}
		if (canExecute == null)
		{
			throw new ArgumentNullException("canExecute");
		}
		this.execute = execute;
		this.canExecute = canExecute;
	}

	public bool CanExecute(object parameter)
	{
		return canExecute != null && canExecute(parameter);
	}

	public void Execute(object parameter)
	{
		execute(parameter);
	}

	public void OnCanExecuteChanged()
	{
		this.CanExecuteChangedInternal?.Invoke(this, EventArgs.Empty);
	}

	public void Destroy()
	{
		canExecute = (object _) => false;
		execute = delegate
		{
		};
	}

	private static bool DefaultCanExecute(object parameter)
	{
		return true;
	}
}
