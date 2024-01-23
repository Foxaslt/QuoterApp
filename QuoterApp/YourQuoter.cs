using System;
using System.Threading;
using System.Threading.Tasks;
using QuoterApp.Repositories;

namespace QuoterApp
{
    public class YourQuoter : IQuoter
    {
        ITaskManager _taskManager;

        public YourQuoter(ITaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        public double GetQuote(string instrumentId, int quantity)
        {
            if (string.IsNullOrWhiteSpace(instrumentId))
            {
                throw new ArgumentException(nameof(instrumentId));
            }

            double price = double.MaxValue;
            bool isMinimumPrice = false;
            var order = _taskManager.GetOrder();
            while (order != null)
            {
                if (!InstrumentMatches(order, instrumentId) || !QuantityIsLess(order, quantity)) continue;

                if (order.Price < price)
                {
                    price = order.Price;
                    isMinimumPrice = true;
                }

                order = _taskManager.GetOrder();
            }

            if (isMinimumPrice)
            {
                return price;
            }

            throw new Exception($"Unable to find best price for instrument {instrumentId} and quantity {quantity}");
        }

        public double GetVolumeWeightedAveragePrice(string instrumentId)
        {
            throw new NotImplementedException();
        }

        #region Private methods

        private bool QuantityIsLess(MarketOrder order, int quantity)
        {
            return order.Quantity < quantity;
        }

        private bool InstrumentMatches(MarketOrder order, string instrumentId)
        {
            return order.InstrumentId == instrumentId;
        }

        #endregion
    }
}
