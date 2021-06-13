//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

//namespace ContractPilot
//{
//    public class AirportContext : DbContext
//    {
//        public DbSet<Airport> Airports { get; set; }

//        // The following configures EF to create a Sqlite database file as `C:\blogging.db`.
//        // For Mac or Linux, change this to `/tmp/blogging.db` or any other absolute path.
//        protected override void OnConfiguring(DbContextOptionsBuilder options)
//            => options.UseSqlite(@"Data Source=D:\Dan\Dev\ContractPilot\Data\little_navmap_msfs.sqlite");
//    }

//    [Table("airport")]
//    public class Airport
//    {
//        [Key]
//        public int airport_id { get; set; }
//        public string ident { get; set; }

//        public double lonx { get; set; }
//        public double laty { get; set; }
//    }
//}
