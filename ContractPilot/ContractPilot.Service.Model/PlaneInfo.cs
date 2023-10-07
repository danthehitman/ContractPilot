using System.Runtime.InteropServices;

namespace ContractPilot.Service.Model;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct PlaneInfo
{
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
	public string TITLE;

	public double AIRSPEED_INDICATED;

	public double SIM_ON_GROUND;

	public double PAYLOAD_STATION_COUNT;

	public double TOTAL_WEIGHT;
}
