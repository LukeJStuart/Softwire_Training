using Newtonsoft.Json;

namespace SupportBank;

public class Payment
{
    public string Date { get; set; }
    
    [JsonProperty("FromAccount")]
    public string From { get; set; }
    
    [JsonProperty("ToAccount")]
    public string To { get; set; }
    
    public string Narrative { get; set; }
    
    public string Amount { get; set; }
}