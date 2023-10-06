using System.Runtime.InteropServices;

namespace ContractPilot.Service.Model;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct PlaneLocation
{
	public double PLANE_ALTITUDE;

	public double PLANE_LONGITUDE;

	public double PLANE_LATITUDE;
}
