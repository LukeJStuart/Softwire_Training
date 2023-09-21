using System.Globalization;
using CsvHelper;

namespace SupportBank
{
    internal static partial class Program
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
                .ToDictionary(uniqueName => uniqueName, _ => 0);
            
            Console.WriteLine(accounts["Jon A"]);
        }

        private static System.Collections.Generic.IEnumerable<SupportBank.Payment> GetRecords()
        {
            using var reader = new StreamReader(@"C:\Users\lukstu\Documents\Softwire_Training\Transactions2014.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<Payment>().ToList();
        }
    }
}