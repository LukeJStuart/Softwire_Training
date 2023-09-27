using Newtonsoft.Json;

namespace BusBoard;

public class PostCodeResult
{
    [JsonProperty("longitude")]
    public double Longitude { get; set; }
    
    [JsonProperty("latitude")]
    public double Latitude { get; set; }
}