using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace BusBoard;

internal static class Program
{
    internal static void Main(string[]? args)
    {
        Console.Write("Please enter a postcode: ");
        // Softwire postcode is NW5 1TL
        var userInput = Console.ReadLine();

        var userPostcodeDetails = GetPostcodeDetails(userInput!);
        var twoNearestStops = GetTwoNearestStations(userPostcodeDetails.Latitude, userPostcodeDetails.Longitude);
        Console.WriteLine();
        
        foreach (var s in twoNearestStops)
        {
            Console.WriteLine("Stop: " + s.CommonName);
            Console.WriteLine("Distance: " + s.DistanceInMetres + " metres");
            Console.WriteLine();
            
            var nextFiveBuses = GetNextFiveBusesForStop(s.StopCode);
        
            Console.WriteLine("{0,-10}{1,-30}{2,-10}", "Route", "Destination", "Minutes");
            foreach (var b in nextFiveBuses)
            {
                Console.WriteLine("{0,-10}{1,-30}{2,-10}", b.Route, b.Destination, int.Parse(b.ArrivalTime) / 60);
            }
            
            Console.WriteLine();
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

    private static PostCodeResult GetPostcodeDetails(string postcode)
    {
        var responseContent = GetApiResponseContent("https://api.postcodes.io/postcodes/" + postcode);
        return JsonConvert.DeserializeObject<Postcode>(responseContent!)!.Result;
    }

    private static NearbyStations GetNearbyStations(double latitude, double longitude)
    {
        var responseContent = GetApiResponseContent("https://api.tfl.gov.uk/StopPoint/?lat=" + latitude +
                                                    "&lon=" + longitude +
                                                    "&stopTypes=NaptanPublicBusCoachTram&radius=400&useStopPointHierarchy=true&modes=bus");
        return JsonConvert.DeserializeObject<NearbyStations>(responseContent!)!;
    }

    private static IEnumerable<StopPoint> GetTwoNearestStations(double latitude, double longitude)
    {
        var nearbyStations = GetNearbyStations(latitude, longitude);
        return nearbyStations.StopPoints.Take(2);
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