using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Runtime.CompilerServices;
using System.IO;

namespace NetWorthTracker
{
    // responsible for fetching quotes for displaying on UI
    internal class Quote
    {
        private static string FilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\assets\quotes.json"));

        public string Author { get; private set; }
        public string Text { get; private set; }

        public Quote(string author, string text)
        {
            Author = author;
            Text = text;
        }

        public static Quote? GetRandomQuote()
        {
            string jsonContent = File.ReadAllText(FilePath);
            List<Quote>? quotes = JsonSerializer.Deserialize<List<Quote>>(jsonContent);

            if (quotes != null)
            {
                Random random = new Random();
                int randomInt = random.Next(0, quotes.Count);

                return quotes[randomInt];
            }
            return null;
        }
    }
}
