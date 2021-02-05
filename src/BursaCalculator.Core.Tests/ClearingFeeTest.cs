using FluentAssertions;
using Xunit;
using static BursaCalculator.Core.Infrastructure.PercentExtensions;

namespace BursaCalculator.Core.Tests
{
    public class ClearingFeeTest
    {
        [Theory]
        [InlineData(500, 0.15)]
        [InlineData(1_200, 0.36)]
        [InlineData(15_020, 4.51)]
        public void CalculateClearingFee_returns_expexted_result(decimal amount, decimal expectedResult)
        {
            var rate = new ClearingFeeRate(Percent(0.03m), 1_000m);

            var result = ClearingFeeExtensions.ClearingFee(rate, amount);

            result.Should().BeApproximately(expectedResult, 0.01m);
        }
    }
}