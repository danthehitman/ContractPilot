using System;

namespace CPCommon.Model;

public class Airplane : BaseModel
{
	public string TailNumber { get; set; }

	public double Hours { get; set; }

	public Guid MakeModelInfoId { get; set; }

	public AirplaneMakeModelInfo MakeModelInfo { get; set; }
}
