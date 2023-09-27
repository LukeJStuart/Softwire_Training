using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace BusBoard;

internal static class Program
{
    internal static void Main(string[]? args)
    {
        Console.Write("Please enter a stop code: ");
        // Example bus stop code: 490008660N
        var userInput = Console.ReadLine();
        Console.WriteLine();
        
        var nextFiveBuses = GetNextFiveBusesForStop(userInput!);
        
        Console.WriteLine("{0,-10}{1,-30}{2,-10}", "Route", "Destination", "Minutes");
        foreach (var b in nextFiveBuses)
        {
            Console.WriteLine("{0,-10}{1,-30}{2,-10}", b.Route, b.Destination, int.Parse(b.ArrivalTime) / 60);
        }
    }

    private static string? GetApiResponseContent(string apiEndPoint)
    {
        var client = new RestClient(apiEndPoint);
        var request = new RestRequest("/");
        request.AddHeader("Content-Type", "application/json");
        var response = (RestResponse)client.Execute<RestRequest>(request);
        var content = response.Content;
        return content;
    }

    private static IEnumerable<IncomingBus> GetArrivalsForStop(string stopCode)
    {
        var responseContent = GetApiResponseContent("https://api.tfl.gov.uk/StopPoint/" + stopCode + "/Arrivals");
        return JsonConvert.DeserializeObject<List<IncomingBus>>(responseContent!)!;
    }
    private static IEnumerable<IncomingBus> GetNextFiveBusesForStop(string stopCode)
    {
        return GetArrivalsForStop(stopCode).OrderBy(bus => int.Parse(bus.ArrivalTime)).Take(5);
    }
}