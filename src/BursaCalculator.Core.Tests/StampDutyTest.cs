using FluentAssertions;
using Xunit;
using static BursaCalculator.Core.StampDutyExtensions;

namespace BursaCalculator.Core.Tests
{
    public class StampDutyTest
    {
        [Theory]
        [InlineData(500, 1)]
        [InlineData(1_000, 1)]
        [InlineData(1_500, 2)]
        public void CalculateStampDuty_returns_expexted_result(decimal amount, decimal expectedResult)
        {
            var rate = new StampDutyRate(1m, 1_000m, 200m);

            var result = CalculateStampDuty(rate, amount);

            result.Should().Be(expectedResult);
        }
    }
}