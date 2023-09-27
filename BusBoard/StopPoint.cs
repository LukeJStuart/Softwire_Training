using Newtonsoft.Json;

namespace BusBoard;

public class StopPoint
{
    [JsonProperty("naptanId")]
    public string StopCode { get; set; }
    
    [JsonProperty("commonName")]
    public string CommonName { get; set; }
    
    [JsonProperty("distance")]
    public double DistanceInMetres { get; set; }
}