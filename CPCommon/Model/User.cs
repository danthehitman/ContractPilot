using System.Collections.Generic;

namespace CPCommon.Model
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public ICollection<UserAirport> UserAirports { get; set; }
        public ICollection<UserAirplane> UserAirplanes { get; set; }
        public int TotalHours { get; set; }
        public int CrossCountryHours { get; set; }
        public int InstrumentHours { get; set; }
        public int UnloggedHours { get; set; }
        public int PistonHours { get; set; }
        public int TurboPropHours { get; set; }
        public int JetHours { get; set; }
        public int TwinHours { get; set; }
    }
}
