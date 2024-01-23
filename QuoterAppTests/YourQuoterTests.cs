using QuoterApp;
using QuoterApp.Repositories;
using Shouldly;

namespace QuoterAppTests
{
    public class YourQuoterTests
    {
        [TestCase("DK50782120", 120, 99.81)]
        public async Task WhenCallingGetQuote_ShouldReturnExpectedPrice(string instrumentId, int quantity, double expectedPrice)
        {
            // Arrange
            var source = new HardcodedMarketOrderSource();
            var quoter = new YourQuoter(source);

            // Act
            var price = await quoter.GetQuote(instrumentId, quantity);

            // Assert
            price.ShouldBe(expectedPrice);
        }
    }
}