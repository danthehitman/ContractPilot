using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace CPCommon.Model;

public class Airport : BaseModel
{
	public string Name { get; set; }

	public string City { get; set; }

	public int Rating { get; set; }

	public string Ident { get; set; }

	public double LonX { get; set; }

	public double LatY { get; set; }

	public Point Location { get; set; }

	public int Altitude { get; set; }

	public int LongestRunwayLength { get; set; }

	public ICollection<Parking> Parking { get; set; }
}
