namespace FizzBuzzCSharpProject
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            FizzBuzzIterate(100);
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
    }

}

