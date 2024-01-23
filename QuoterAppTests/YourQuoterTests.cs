using Moq;
using QuoterApp.Calculator;
using QuoterApp.Entities;
using QuoterApp.Handlers;
using Shouldly;
using System.Collections;

namespace QuoterAppTests
{
    public class YourQuoterTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void WhenCallingGetQuote_WithIncorrectParameters_ShouldThrowArgumentException(string instrumentId)
        {
            // Arrange
            var taskManager = Mock.Of<ITaskManager>();
            var quoter = new YourQuoter(taskManager);

            // Act
            Action action = () => quoter.GetQuote(instrumentId, 1);

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [TestCaseSource(nameof(MarketOrderTestData))]
        public void WhenCallingGetQuote_ShouldReturnExpectedPrice(IEnumerable<MarketOrder> testData, string instrumentId, int quantity, double expectedPrice)
        {
            // Arrange
            var taskManagerMock = new Mock<ITaskManager>();
            taskManagerMock.Setup(m => m.GetOrders()).Returns(testData);
            var quoter = new YourQuoter(taskManagerMock.Object);

            // Act
            var price = quoter.GetQuote(instrumentId, quantity);

            // Assert
            price.ShouldBe(expectedPrice);
        }

        private static IEnumerable MarketOrderTestData 
        {
            get
            {
                yield return new TestCaseData(
                    new List<MarketOrder>(),
                    "DK50782120", 90,
                    double.NaN).SetName(
                    "No instruments in source, Instrument provided, Returns NaN");

                yield return new TestCaseData(
                    new List<MarketOrder>()
                        { new() { InstrumentId = "DK50782120", Quantity = 100, Price = 21.2 } },
                    "DK50782120", 90,
                    21.2).SetName(
                    "One instrument in source, Same instrument provided, Quantity provided is less than instrument");

                yield return new TestCaseData(
                    new List<MarketOrder>()
                        { new() { InstrumentId = "DK50782120", Quantity = 100, Price = 21.2 } },
                    "DK50782120", 110,
                    double.NaN).SetName(
                    "One instrument in source, Same instrument provided, Quantity provided is greater than instrument");

                yield return new TestCaseData(
                    new List<MarketOrder>()
                    {
                        new() { InstrumentId = "DK50782120", Quantity = 100, Price = 21.2 },
                        new() { InstrumentId = "DK50782120", Quantity = 120, Price = 31.5 }
                    },
                    "DK50782120", 110,
                    31.5).SetName(
                    "Two instruments in source, Same instrument provided, Quantity provided is less than second instrument quantity");

                yield return new TestCaseData(
                    new List<MarketOrder>()
                    {
                        new() { InstrumentId = "RR50782120", Quantity = 100, Price = 21.2 },
                        new() { InstrumentId = "RR50782120", Quantity = 120, Price = 31.5 }
                    },
                    "DK50782120", 110,
                    double.NaN).SetName(
                    "Two other instruments in source, Instrument provided, Returns NaN");
                //yield return new List<MarketOrder>()
                //{
                //    new MarketOrder() { InstrumentId = "DK50782120", Quantity = 100, Price = 21.2 },
                //    new MarketOrder() { InstrumentId = "DK50782120", Quantity = 100, Price = 21.2 }
                //};
                //yield return new MarketOrder() { InstrumentId = "DK50782120", Quantity = 100, Price = 21.2 };
            }
        }
    }
}