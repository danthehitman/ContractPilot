using System.Collections.Generic;

namespace CPCommon.Model;

public class AirplaneMakeModelInfo : BaseModel
{
	public string Name { get; set; }

	public int Range { get; set; }

	public int NumberOfEngines { get; set; }

	public double CapacityPounds { get; set; }

	public EngineType EngineType { get; set; }

	public ICollection<PayloadStation> PayloadStations { get; set; }

	public ICollection<FuelTank> FuelTanks { get; set; }

	public ICollection<Airplane> Airplanes { get; set; }
}
