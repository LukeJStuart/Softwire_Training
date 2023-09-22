using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SupportBank;

[XmlType("SupportTransaction")]
public class Payment
{
    [XmlAttribute("Date")]
    public string Date { get; set; }
    
    [XmlElement("From")]
    [JsonProperty("FromAccount")]
    public string From { get; set; }
    
    [XmlElement("To")]
    [JsonProperty("ToAccount")]
    public string To { get; set; }
    
    [XmlElement("Description")]
    public string Narrative { get; set; }
    
    [XmlElement("Value")]
    public string Amount { get; set; }
}