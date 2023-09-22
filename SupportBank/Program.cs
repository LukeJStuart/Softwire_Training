using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using CsvHelper;
using Newtonsoft.Json;
using NLog;
using NLog.Config;
using NLog.Targets;


namespace SupportBank;

internal static class Program
{
        
    private static  ILogger logger = LogManager.GetCurrentClassLogger();
        
    internal static void Main(string[]? args)
    {
        var config = new LoggingConfiguration();
        var target = new FileTarget { FileName = @"C:\Users\lukstu\Documents\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
        config.AddTarget("File Logger", target);
        config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
        LogManager.Configuration = config;
            
        logger.Debug("Program Initialised");
            
        // Possible files are Transactions2014.csv , DodgyTransactions2015.csv , Transactions2013.json , Transactions2012.xml
        var records = GetRecords("Transactions2012.xml")!.ToList();
        logger.Debug("Read csv contents into list");
        var accounts = records
            .Select(r => r.From)
            .Concat(
                records.Select(r => r.To)
            )
            .Distinct()
            .ToDictionary(uniqueName => uniqueName, _ => 0.0);

        var dodgyRecords = new List<Payment>();

        foreach (var p in records)
        {
            try
            {
                accounts[p.From] -= double.Parse(p.Amount);
                accounts[p.To] += double.Parse(p.Amount);
            }
            catch (Exception)
            {
                logger.Debug("Invalid Amount Encountered: " + p.Amount + " Transaction Marked for Deletion");
                dodgyRecords.Add(p);
            }
        }
            
        //Removing transactions with invalid amounts from records so they will not show in results from List[]
        foreach (var p in dodgyRecords)
        {
            logger.Debug("Transaction with Invalid Amount '" + p.Amount + "' Removed from Set.");
            records.Remove(p);
        }
            
        var commandRegex = new Regex("List\\[[^\\]]*\\]", RegexOptions.IgnoreCase);
            
        // User Input
        while (true)
        {
            Console.Write("Enter 'List All' to output all account balances or 'List[");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Account");
            Console.ResetColor();
            Console.WriteLine("]' to show all transactions for a given account.");
                
            Console.WriteLine();
                
            Console.Write("Enter Command: ");
            var userInput = Console.ReadLine();

            if (userInput is null)
            {
                continue;
            }
            if (userInput == "List All")
            {
                Console.WriteLine("Listing all accounts:");
                Console.WriteLine();
            
                Console.WriteLine("{0,-10}{1,10}", "Name", "Balance");
                Console.WriteLine();
            
                foreach (var a in accounts)
                {
                    Console.WriteLine("{0,-10}{1,10:£0.00}", a.Key, a.Value);
                }
            }
            else if (commandRegex.IsMatch(userInput))
            {
                if (accounts.Select(a => a.Key).Contains(userInput[5..^1]))
                {
                    var chosenName = userInput[5..^1];
                    Console.WriteLine("Listing all transactions for " + chosenName + ":");
                    Console.WriteLine();
            
                    Console.WriteLine("{0,-15}{1,-10}{2,-15}{3,-15}{4,-30}", "Date", "Amount", "From", "To", "Narrative");
                    Console.WriteLine();

                    foreach (var r in records.Where(r => r.From == chosenName || r.To == chosenName))
                    {
                        Console.WriteLine("{0,-15}{1,-10:£0.00}{2,-15}{3,-15}{4,-30}", r.Date[..10], double.Parse(r.Amount), r.From, r.To, r.Narrative);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Account Name.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Command.");
            }

            Console.WriteLine();
        }
    }

    private static IEnumerable<Payment>? GetRecords(string file)
    {
        if (file.Contains(".csv"))
        {
            using var reader = new StreamReader(@"C:\Users\lukstu\Documents\Softwire_Training\" + file);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<Payment>().ToList();
        }

        if (!file.Contains(".xml"))
            return JsonConvert.DeserializeObject<List<Payment>>(
                File.ReadAllText(@"C:\Users\lukstu\Documents\Softwire_Training\" + file));
        {
            // XML Functionality NOT yet fully implemented - issue with mapping transaction participants from xml
            using var reader = new StreamReader(@"C:\Users\lukstu\Documents\Softwire_Training\" + file);
            var deserializer = new XmlSerializer(typeof(List<Payment>), new XmlRootAttribute("TransactionList"));
            return (List<Payment>)deserializer.Deserialize(reader)!;
        }

    }
}