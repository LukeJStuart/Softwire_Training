using System.Globalization;
using System.Text.RegularExpressions;
using CsvHelper;

namespace SupportBank
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var records = GetRecords().ToList();
            var accounts = records
                .Select(r => r.From)
                .Concat(
                    records.Select(r => r.To)
                )
                .Distinct()
                .ToDictionary(uniqueName => uniqueName, _ => 0.0);

            foreach (var p in records)
            {
                accounts[p.From] -= double.Parse(p.Amount);
                accounts[p.To] += double.Parse(p.Amount);

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
                            Console.WriteLine("{0,-15}{1,-10:£0.00}{2,-15}{3,-15}{4,-30}", r.Date, double.Parse(r.Amount), r.From, r.To, r.Narrative);
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

        private static System.Collections.Generic.IEnumerable<SupportBank.Payment> GetRecords()
        {
            using var reader = new StreamReader(@"C:\Users\lukstu\Documents\Softwire_Training\Transactions2014.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<Payment>().ToList();
        }
    }
}