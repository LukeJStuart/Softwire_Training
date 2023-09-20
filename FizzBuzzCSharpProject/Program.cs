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
            if (x % 3 == 0 && x % 5 == 0)
            {
                return "FizzBuzz";
            }
            else if (x % 3 == 0)
            {
                return "Fizz";
            }
            else if (x % 5 == 0)
            {
                return "Buzz";
            }
            else
            {
                return x.ToString();
            }  
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

