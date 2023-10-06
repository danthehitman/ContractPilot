using System.ComponentModel.DataAnnotations;

namespace Lnm2CpLoader.Model;

public class LnmParkingModel
{
    [Key]
    public int parking_id { get; set; }

    public int airport_id { get; set; }

    public string type { get; set; }

    public string name { get; set; }

    public int number { get; set; }

    public double radius { get; set; }

    public double heading { get; set; }

    public double laty { get; set; }

    public double lonx { get; set; }
}
