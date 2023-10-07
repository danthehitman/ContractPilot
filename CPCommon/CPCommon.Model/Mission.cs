using System;
using System.Collections.Generic;

namespace CPCommon.Model;

public class Mission : BaseModel
{
	public string Name { get; set; }

	public string Description { get; set; }

	public bool IsActive { get; set; }

	public bool IsComplete { get; set; }

	public DateTime? StartingDateTime { get; set; }

	public DateTime? TargetDateTime { get; set; }

	public Guid StartingAirportId { get; set; }

	public Airport StartingAirport { get; set; }

	public Guid DestinationAirportId { get; set; }

	public Airport DestinationAirport { get; set; }

	public Guid? RequiredAirplaneMakeModelInfoId { get; set; }

	public AirplaneMakeModelInfo RequiredAirplaneMakeModelInfo { get; set; }

	public Cargo Cargo { get; set; }

	public ICollection<Passenger> Passengers { get; set; }
}
