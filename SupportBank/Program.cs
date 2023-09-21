using System.Globalization;
using CsvHelper;

namespace SupportBank
{
    internal static partial class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(GetRecords());
        }

        private static System.Collections.Generic.IEnumerable<SupportBank.Payment> GetRecords()
        {
            using var reader = new StreamReader(@"C:\Users\lukstu\Documents\Softwire_Training\Transactions2014.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<Payment>();
        }
    }

    public abstract class Payment
    {
        public string? Date { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Narrative { get; set; }
        public string? Amount { get; set; }
    }
}