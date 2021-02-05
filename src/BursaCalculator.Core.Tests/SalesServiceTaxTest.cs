using FluentAssertions;
using Xunit;
using static BursaCalculator.Core.Infrastructure.PercentExtensions;
using static BursaCalculator.Core.SalesServiceTaxExtensions;

namespace BursaCalculator.Core.Tests
{
    public class SalesServiceTaxTest
    {
        [Theory]
        [InlineData(3.15, 0.15, 1, 0.19)] // based on 500 purchase value
        [InlineData(105.14, 4.51, 16, 6.31)] // based on 15,020 purchase value
        public void CalculateStampDuty_returns_expexted_result(decimal brokerageFee, decimal clearingFee, decimal stampDuty, decimal expectedResult)
        {
            var rate = new SalesServiceTaxRate(Percent(6), Percent(0), Percent(0));

            var result = ToTotal(CalculateSalesServiceTax(rate, brokerageFee, clearingFee, stampDuty));


            result.Should().BeApproximately(expectedResult, 0.01m);
        }

        [Theory]
        [InlineData(3.15, 0.15, 1, 0.19)] // based on 500 purchase value
        [InlineData(105.14, 4.51, 16, 6.31)] // based on 15,020 purchase value
        public void CalculateStampDuty_returns_expexted_result1(decimal brokerageFee, decimal clearingFee, decimal stampDuty, decimal expectedResult)
        {
            var rate = new SalesServiceTaxRate(Percent(6), Percent(0), Percent(0));
            var taxableFees = new TaxableFees(brokerageFee, clearingFee, stampDuty);

            var result = ToTotal(CalculateSalesServiceTax(rate, taxableFees));


            result.Should().BeApproximately(expectedResult, 0.01m);
        }

    }
}