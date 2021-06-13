using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPCommon.Model
{
    public class UserAirport: BaseModel
    {
        public Guid AirportId { get; set; }
        public Airport Airport { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public bool IsHome { get; set; }
        public int Notoriety { get; set; }
    }
}
