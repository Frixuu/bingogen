using System;
using System.IO;
using System.Linq;

namespace Frixu.BingoGen
{
    public class Program
    {
        /// <summary>Entry point for the application.</summary>
        public static void Main(string[] args)
        {
            //Reading command-line arguments
            var inputFile = args.Length > 0 ? args[0] :
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "words.txt");
            var title = args.Length > 1 ? args[1] : "Bingo!";
            var outputFile = args.Length > 2 ? args[2] : "bingo.pdf";

            var card = new BingoCard(title);

            try
            {
                var lines = File.ReadLines(inputFile).Distinct().ToList();
                if (lines.Count < 24)
                {
                    Console.Error.Write("Cannot create bingo card. ");
                    Console.Error.Write($"{inputFile} has less than 24 unique words. ");
                    Console.Error.WriteLine("Stopping.");
                    Environment.Exit(1);
                }
                lines.ForEach(line => card.AddWord(line));
                card.Create().Save(outputFile);
            }
            catch (FileNotFoundException)
            {
                Console.Error.WriteLine($"File {inputFile} doesn't exist. Stopping.");
                Environment.Exit(5);
            }
        }
    }
}
