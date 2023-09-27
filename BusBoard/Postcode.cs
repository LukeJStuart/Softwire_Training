using Newtonsoft.Json;

namespace BusBoard;

public class Postcode
{
    [JsonProperty("result")]
    public PostCodeResult Result { get; set; }
}