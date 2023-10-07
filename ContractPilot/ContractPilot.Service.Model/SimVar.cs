using Microsoft.FlightSimulator.SimConnect;

namespace ContractPilot.Service.Model;

public class SimVar
{
	public string Unit { get; set; }

	public SIMCONNECT_DATATYPE DataType { get; set; }

	public bool Settable { get; set; }
}
