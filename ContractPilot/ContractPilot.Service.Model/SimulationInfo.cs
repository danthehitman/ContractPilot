using System.Runtime.InteropServices;

namespace ContractPilot.Service.Model;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SimulationInfo
{
	public double UNLIMITED_FUEL;

	public double REALISM_CRASH_DETECTION;

	public double SIMULATION_RATE;

	public double REALISM;
}
