using QuoterApp.Entities;
using QuoterApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuoterApp.Handlers
{
    internal class TaskManager : ITaskManager
    {
        private const int Timeout = 1000;
        private readonly IMarketOrderSource _source;
        private Lazy<IEnumerable<MarketOrder>> _marketOrdersLazy;

        private IEnumerable<MarketOrder> MarketOrders => _marketOrdersLazy.Value;

        public TaskManager(IMarketOrderSource source)
        {
            _source = source; 
        }

        public IEnumerable<MarketOrder> GetOrders()
        {
            if (_marketOrdersLazy == null)
            {
                _marketOrdersLazy = new Lazy<IEnumerable<MarketOrder>>(GetOrdersInternal().ToArray);
            }

            return MarketOrders;
        }

        private IEnumerable<MarketOrder> GetOrdersInternal()
        {
            while (true)
            {
                var tokenSource = new CancellationTokenSource();
                var token = tokenSource.Token;

                Task<MarketOrder> task = Task<MarketOrder>.Factory.StartNew(() =>
                {
                    token.ThrowIfCancellationRequested();
                    return _source.GetNextMarketOrder();
                }, tokenSource.Token);

                task.Wait(Timeout);

                if (task.IsCompletedSuccessfully)
                    yield return task.Result;
                else
                {
                    tokenSource.Cancel();
                    yield break;
                }
            }
        }
    }
}
