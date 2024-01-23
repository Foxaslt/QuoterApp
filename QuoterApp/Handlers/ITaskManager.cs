using System.Collections.Generic;
using QuoterApp.Entities;

namespace QuoterApp.Handlers
{
    public interface ITaskManager
    {
        IEnumerable<MarketOrder> GetOrders();
    }
}
