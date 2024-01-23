﻿using System;
using QuoterApp.Repositories;

namespace QuoterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new HardcodedMarketOrderSource();
            var gq = new YourQuoter(source);
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
