namespace CPCommon.Model
{
    public class AirplaneMakeModelInfo: BaseModel
    {
        public string Name { get; set; }
        public int Range { get; set; }
        public int NumerOfEngines { get; set; }
        public EngineType EngineType { get; set; }
    }
}
