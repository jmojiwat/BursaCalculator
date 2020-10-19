using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using static BursaCalculator.Core.BrokerageFeeExtensions;
using static BursaCalculator.Core.Infrastructure.PercentExtensions;

namespace BursaCalculator.Core.Tests
{
    public class BrokerageFeeTest
    {
        [Theory]
        [InlineData(500, 3.5)]
        public void CalculateBrokerageFee_returns_expexted_result(decimal amount, decimal expectedResult)
        {
            var percentageRates = new List<BrokeragePercentageRate>
                {
                    new BrokeragePercentageRate(0, Percent(0.7m)),
                    new BrokeragePercentageRate(100_000, Percent(0.5m)),
                    new BrokeragePercentageRate(500_000, Percent(0.4m)),
                    new BrokeragePercentageRate(1_000_000, Percent(0.3m)),
                }
                .OrderBy(r => r.AmountFrom);

            var rate = new BrokerageFeeRate(percentageRates, Percent(0), 12, Percent(0));

            var result = CalculateBrokerageFee(rate, amount);

            result.Should().Be(expectedResult);
        }
    }
}