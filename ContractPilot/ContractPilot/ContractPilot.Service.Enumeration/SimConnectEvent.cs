namespace ContractPilot.Service.Enumeration;

internal enum SimConnectEvent
{
	Sim,
	SimStart,
	SimStop,
	Crashed,
	PositionChanged,
	FlightLoaded,
	FlightPlanActivated,
	FlightPlanDeactivated
}
