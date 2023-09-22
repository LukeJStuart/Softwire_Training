using Newtonsoft.Json;

namespace BusBoard;


public class IncomingBus
{
    [JsonProperty("lineName")]
    public string Route { get; set; }

    [JsonProperty("destinationName")]
    public string Destination { get; set; }

    [JsonProperty("timeToStation")]
    public string ArrivalTime { get; set; }
}