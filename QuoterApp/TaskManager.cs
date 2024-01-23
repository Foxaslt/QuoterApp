using QuoterApp.Repositories;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace QuoterApp
{
    internal class TaskManager : ITaskManager
    {
        private const int _timeout = 1000;
        private readonly IMarketOrderSource _source;

        public MarketOrder GetOrder()
        {
            //while ( != null) { }
            //var token = new CancellationToken();
            //Task<MarketOrder> task = Task<MarketOrder>.Factory.StartNew(() => _source.GetNextMarketOrder(), token);//.Wait(new TimeSpan(0, 0, 0, _timeout));
            //var t = Task.WhenAny(task, Task.Delay(_timeout, token));
            //if (t == task)
            //    //{
            //    //}
            //    //else
            //    //{
            //    //    // task has been cancelled
            //    //}

                throw new NotImplementedException();
        }
    }
}
