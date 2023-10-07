using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lnm2CpLoader.Model;

[Table("airport")]
public class LnmAirportModel
{
    [Key]
    public int airport_id { get; set; }

    public string name { get; set; }

    public string city { get; set; }

    public int rating { get; set; }

    public string ident { get; set; }

    public double lonx { get; set; }

    public double laty { get; set; }

    public int altitude { get; set; }

    public int longest_runway_length { get; set; }
}
