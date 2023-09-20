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
            catch (Exception ex)
            {
                Console.WriteLine("Error: Invalid input. We will go to 100.");
            }

            FizzBuzzIterate(userNum);
        }

        private static string FizzBuzz(int x)
        {
            var result = "";

            if (x % 11 == 0)
            {
                result = "Bong";
            }
            else
            {
                if (x % 3 == 0)
                {
                    result += "Fizz";
                }

                if (x % 5 == 0)
                {
                    result += "Buzz";
                }

                if (x % 7 == 0)
                {
                    result += "Bang";
                }
            }
            
            if (x % 13 == 0)
            {
                var bIndex = result.IndexOf("B", StringComparison.Ordinal);

                if (bIndex == -1)
                {
                    result += "Fezz";
                }
                else
                {
                    result = result.Insert(bIndex, "Fezz");
                }
            }
            
            if (x % 17 == 0 && result != "")
            {
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

