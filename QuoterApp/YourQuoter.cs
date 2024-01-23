using System;
using System.Threading;
using System.Threading.Tasks;
using QuoterApp.Repositories;

namespace QuoterApp
{
    public class YourQuoter : IQuoter
    {
        private const int _timeout = 1000;
        private readonly IMarketOrderSource _source;
        public YourQuoter(IMarketOrderSource marketOrderSource)
        {
            _source = marketOrderSource;
        }

        public double GetQuote(string instrumentId, int quantity)
        {
            var token = new CancellationToken();
            Task<MarketOrder> task = Task<MarketOrder>.Factory.StartNew( () => _source.GetNextMarketOrder(), token);//.Wait(new TimeSpan(0, 0, 0, _timeout));
            var t =  Task.WhenAny(task, Task.Delay(_timeout, token));
            if (t == task)
            //{
            //}
            //else
            //{
            //    // task has been cancelled
            //}

            return 0;
        }

        public double GetVolumeWeightedAveragePrice(string instrumentId)
        {
            throw new NotImplementedException();
        }
    }
}
