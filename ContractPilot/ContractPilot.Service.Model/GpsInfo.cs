using System.Runtime.InteropServices;

namespace ContractPilot.Service.Model;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct GpsInfo
{
	public double GPS_IS_ACTIVE_FLIGHT_PLAN;

	public double GPS_IS_ARRIVED;
}
