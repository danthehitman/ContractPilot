using System;

namespace CPCommon.Model;

public class UserAirport : BaseModel
{
	public Guid AirportId { get; set; }

	public Airport Airport { get; set; }

	public Guid UserId { get; set; }

	public User User { get; set; }

	public int Notoriety { get; set; }
}
