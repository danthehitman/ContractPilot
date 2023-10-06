using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ContractPilot.Extensions;

namespace ContractPilot.Command;

public class AsyncCommand<T> : IAsyncCommand<T>, ICommand
{
	private bool _isExecuting;

	private readonly Func<T, Task> _execute;

	private readonly Func<T, bool> _canExecute;

	private readonly IErrorHandler _errorHandler;

	public event EventHandler CanExecuteChanged;

	public AsyncCommand(Func<T, Task> execute, Func<T, bool> canExecute = null, IErrorHandler errorHandler = null)
	{
		_execute = execute;
		_canExecute = canExecute;
		_errorHandler = errorHandler;
	}

	public bool CanExecute(T parameter)
	{
		return !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
	}

	public async Task ExecuteAsync(T parameter)
	{
		if (CanExecute(parameter))
		{
			try
			{
				_isExecuting = true;
				await _execute(parameter);
			}
			finally
			{
				_isExecuting = false;
			}
		}
		RaiseCanExecuteChanged();
	}

	public void RaiseCanExecuteChanged()
	{
		this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}

	bool ICommand.CanExecute(object parameter)
	{
		return CanExecute((T)parameter);
	}

	void ICommand.Execute(object parameter)
	{
		ExecuteAsync((T)parameter).FireAndForgetSafeAsync(_errorHandler);
	}
}
