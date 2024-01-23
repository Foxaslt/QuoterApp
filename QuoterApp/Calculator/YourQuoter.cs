using QuoterApp.Handlers;
using System;
using System.Linq;

namespace QuoterApp.Calculator
{
    public class YourQuoter : IQuoter
    {
        private readonly ITaskManager _taskManager;

        public YourQuoter(ITaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        public double GetQuote(string instrumentId, int quantity)
        {
            ValidateInstrumentId(instrumentId);

            var orders = _taskManager.GetOrders();
            var filteredOrders =
                orders.Where(order => order.InstrumentId.Equals(instrumentId) && quantity <= order.Quantity).ToArray();

            return filteredOrders.Any() ? filteredOrders.Min(order => order.Price) : double.NaN;
        }

        public double GetVolumeWeightedAveragePrice(string instrumentId)
        {
            ValidateInstrumentId(instrumentId);

            var orders = _taskManager.GetOrders();

            var filteredOrders = orders.Where(order => order.InstrumentId.Equals(instrumentId)).ToArray();

            var vwap = filteredOrders.Sum(order => order.Quantity * order.Price) /
                       filteredOrders.Sum(order => order.Quantity);

            return vwap;
        }

        private static void ValidateInstrumentId(string instrumentId)
        {
            if (string.IsNullOrWhiteSpace(instrumentId))
            {
                throw new ArgumentException(nameof(instrumentId));
            }
        }
    }
}
