using Moq;
using QuoterApp;
using QuoterApp.Repositories;
using Shouldly;

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

        [TestCase("DK50782120", 120, 99.81)]
        public void WhenCallingGetQuote_ShouldReturnExpectedPrice(string instrumentId, int quantity, double expectedPrice)
        {
            // Arrange
            var source = new HardcodedMarketOrderSource();
            var taskManager = Mock.Of<ITaskManager>();
            var quoter = new YourQuoter(taskManager);

            // Act
            var price = quoter.GetQuote(instrumentId, quantity);

            // Assert
            price.ShouldBe(expectedPrice);
        }
    }
}