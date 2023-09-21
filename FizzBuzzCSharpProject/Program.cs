using System.Text.RegularExpressions;

namespace FizzBuzzCSharpProject
{
    internal static partial class Program
    {
        private static void Main(string[] args)
        {
            var userNum = 100;
            try
            {
                Console.Write("How high should we go? ");
                var input = Console.ReadLine() ?? throw new InvalidOperationException();
                userNum = Math.Abs(int.Parse(input));
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Invalid input. We will go to 100.");
            }

            FizzBuzzIterate(userNum);
        }

        private static string FizzBuzz(int x)
        {
            var result = new List<string>();

            if (x % 11 == 0)
            {
                result.Add("Bong");
            }
            else
            {
                if (x % 3 == 0)
                {
                    result.Add("Fizz");
                }

                if (x % 5 == 0)
                {
                    result.Add("Buzz");
                }

                if (x % 7 == 0)
                {
                    result.Add("Bang");
                }
            }
            
            // Need to fix this so that can find where in result list to insert Fezz
            if (x % 13 == 0)
            {
                var bWord = result.Find(s => s.StartsWith("B"));
                
                if (bWord == null)
                {
                    result.Add("Fezz");
                }
                else
                {
                    var bWordIndex = result.FindIndex(bWord);
                    result = result.Insert(, "Fezz");
                }
            }
            
            if (x % 17 == 0 && result != "")
            {
                // Regex broken by switching to .NET 6.0 Need to replace anyway - change result to array as per Ata
                var resultArray = MyRegex().Matches(result)
                    .Cast<Match>()
                    .Select(m => m.Value);
                result = string.Join("", resultArray.Reverse());
            }
             
            if (result == "")
            {
                result = x.ToString();
            }
            
            return result;
        }

        private static void FizzBuzzIterate(int x)
        {
            for (var i = 1; i <= x; i++)
            {
                Console.WriteLine(FizzBuzz(i));
            }
        }

        [GeneratedRegex("([A-Z][a-z]+)")]
        private static partial Regex MyRegex();
    }

}

