using System;
using QuoterApp.Calculator;
using QuoterApp.Handlers;
using QuoterApp.Repositories;

namespace QuoterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new HardcodedMarketOrderSource();
            var manager = new TaskManager(source);
            var gq = new YourQuoter(manager);
            var qty = 120;

            var quote = gq.GetQuote("DK50782120", qty);
            var vwap = gq.GetVolumeWeightedAveragePrice("DK50782120");

            Console.WriteLine($"Quote: {quote}, {quote / (double)qty}");
            Console.WriteLine($"Average Price: {vwap}");
            Console.WriteLine();
            Console.WriteLine($"Done");
        }
    }
}
