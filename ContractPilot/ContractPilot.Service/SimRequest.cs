using ContractPilot.Service.Enumeration;
using Microsoft.FlightSimulator.SimConnect;

namespace ContractPilot.Service;

public class SimRequest
{
	public DataDefinition Definition { get; set; }

	public Request Request { get; set; }

	public (string Name, string Unit) NameUnitTuple { get; set; }

	public SIMCONNECT_DATATYPE DataType { get; set; } = (SIMCONNECT_DATATYPE)4;

}
