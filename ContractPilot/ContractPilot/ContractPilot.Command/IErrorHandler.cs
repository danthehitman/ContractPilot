using System;

namespace ContractPilot.Command;

public interface IErrorHandler
{
	void HandleError(Exception ex);
}
