using System;

namespace CPCommon.Model;

public class UserAirplane : BaseModel
{
	public Guid UserId { get; set; }

	public User User { get; set; }

	public Guid AirplaneId { get; set; }

	public Airplane Airplane { get; set; }

	public int Hours { get; set; }
}
