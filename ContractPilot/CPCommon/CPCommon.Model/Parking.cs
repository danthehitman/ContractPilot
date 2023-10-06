using System;
using NetTopologySuite.Geometries;

namespace CPCommon.Model;

public class Parking : BaseModel
{
	public Guid AirportId { get; set; }

	public Airport Airport { get; set; }

	public string Type { get; set; }

	public string Name { get; set; }

	public int Number { get; set; }

	public double Radius { get; set; }

	public double Heading { get; set; }

	public double LonX { get; set; }

	public double LatY { get; set; }

	public Point Location { get; set; }
}
