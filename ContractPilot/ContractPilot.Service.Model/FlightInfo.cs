using System.Runtime.InteropServices;

namespace ContractPilot.Service.Model;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct FlightInfo
{
	public double MAX_G_FORCE;

	public double PLANE_ALT_ABOVE_GROUND;

	public double PLANE_BANK_DEGREES;

	public double PLANE_PITCH_DEGREES;
}
