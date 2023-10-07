namespace CPCommon.Model;

public class Cargo
{
	public string Name { get; set; }

	public string Description { get; set; }

	public double WeightPounds { get; set; }

	public bool IsIllicit { get; set; }

	public double GForceTolerance { get; set; }
}
