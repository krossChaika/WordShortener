namespace WordShortener;

class Program
{
    static void Main(string[] args)
    {
         const int minLength = 3;
         int desiredLength = 0;
         List<string> inputs;

         while (true)
         {
             Console.WriteLine("Enter length: ");
             if (!int.TryParse(Console.ReadLine(), out desiredLength) || desiredLength < minLength)
             {
                 Console.WriteLine("Length is too short!");
                 continue;
             }

             Console.WriteLine("Enter character to use as filler: ");
             char dotChar = Console.ReadLine()[0];
             
             Console.WriteLine("Enter strings separated by spaces: ");
             inputs = Console.ReadLine().Split(' ').ToList();
             if (inputs.Min(x => x.Length) < desiredLength)
             {
                 Console.WriteLine("One of the strings is less than the desired result length!");
                 continue;
             }

             List<string> history = new(inputs.Count);
             foreach (string input in inputs)
             {
                 string result = "";
                 int dotsCount = 3;
                 if (input.Length == 3)
                 {
                     result = input[0] + $"{dotChar}" + input[2];
                 }
                 else if (input.Length == 4)
                 {
                     result = input[0] + $"{dotChar}{dotChar}" + input[3];
                 }
                 else
                 {
                     int prefixLen = (desiredLength - dotsCount) / 2;
                     int suffixLen = desiredLength - dotsCount - prefixLen;

                     result = input.Substring(0, prefixLen) 
                                       + new string(dotChar, dotsCount) + input.Substring(input.Length - suffixLen);
                 }
                 
                 string newResult = result;
                 while (history.Contains(newResult))
                 {
                     newResult = result;
                     result += history.Count(x => x == result);
                 }
                 Console.WriteLine(input + " -> " + newResult);
                 history.Add(newResult);
             }
         }
         
         Console.ReadKey();
    }
}