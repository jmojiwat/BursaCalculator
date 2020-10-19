using FluentAssertions;
using Xunit;
using static BursaCalculator.Core.Infrastructure.PercentExtensions;
using static BursaCalculator.Core.SalesServiceTaxExtensions;

namespace BursaCalculator.Core.Tests
{
    public class SalesServiceTaxTest
    {
        [Theory]
        [InlineData(500, 105)]
        public void CalculateStampDuty_returns_expexted_result(decimal amount, decimal expectedResult)
        {
            var rate = new SalesServiceTaxRate(Percent(6), Percent(10), Percent(5));

            var result = ToTotal(CalculateSalesServiceTax(rate, amount));

            result.Should().Be(expectedResult);
        }
    }
}