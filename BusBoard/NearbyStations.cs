using Newtonsoft.Json;

namespace BusBoard;

public class NearbyStations
{
    [JsonProperty("stopPoints")]
    public List<StopPoint> StopPoints { get; set; }
}