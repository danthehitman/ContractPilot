using System;
using System.Threading.Tasks;
using ContractPilot.Command;

namespace ContractPilot.Extensions;

public static class TaskUtilities
{
	public static async void FireAndForgetSafeAsync(this Task task, IErrorHandler handler = null)
	{
		try
		{
			await task;
		}
		catch (Exception ex2)
		{
			Exception ex = ex2;
			handler?.HandleError(ex);
		}
	}
}
