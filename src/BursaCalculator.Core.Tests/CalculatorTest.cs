using System;
using AutoFixture;
using AutoFixture.Xunit2;
using BursaCalculator.Core.Infrastructure;
using FluentAssertions;
using Xunit;
using static BursaCalculator.Core.Infrastructure.PercentExtensions;

namespace BursaCalculator.Core.Tests
{
    public class CalculatorTest
    {
        [Theory, CalculatorAutoData]
        public void Calculate_returns_expected_result(BrokerageFeeRate brokerageFeeRate,
            ClearingFeeRate clearingFeeRate,
            StampDutyRate stampDutyRate, SalesServiceTaxRate salesServiceTaxRate)
        {
            var sut = new Calculator(brokerageFeeRate, clearingFeeRate, stampDutyRate, salesServiceTaxRate);

            var result = sut.Calculate(1, Lot(1), 1.5m);

            result.Profit.Should().Be(50);
        }


        [Theory, CalculatorAutoData]
        public void CalculateBrokerageFee_with_1000_returns_expected_result(BrokerageFeeRate brokerageFeeRate)
        {
            var sut = new Func<BrokerageFeeRate, decimal, decimal>(BrokerageFeeExtensions.CalculateBrokerageFee);

            var result = sut(brokerageFeeRate, 10);

            result.Should().Be(12m);
        }

        [Theory, CalculatorAutoData]
        public void CalculateBrokerageFee_with_equal_100000_returns_expected_result(BrokerageFeeRate brokerageFeeRate)
        {
            var sut = new Func<BrokerageFeeRate, decimal, decimal>(BrokerageFeeExtensions.CalculateBrokerageFee);

            var result = sut(brokerageFeeRate, 100_000);

            result.Should().Be(420m);
        }

        [Theory, CalculatorAutoData]
        public void CalculateBrokerageFee_with_equal_150000_returns_expected_result(BrokerageFeeRate brokerageFeeRate)
        {
            var sut = new Func<BrokerageFeeRate, decimal, decimal>(BrokerageFeeExtensions.CalculateBrokerageFee);

            var result = sut(brokerageFeeRate, 150_000);

            result.Should().Be(315m);
        }

        [Theory, CalculatorAutoData]
        public void CalculateBrokeragePercentageFee_with_1000_returns_expected_result(BrokerageFeeRate brokerageFeeRate)
        {
            var sut = new Func<BrokerageFeeRate, decimal, decimal>(BrokerageFeeExtensions
                .CalculateBrokeragePercentageFee);

            var result = sut(brokerageFeeRate, 1_000);

            result.Should().Be(4.2m);
        }

        [Theory, CalculatorAutoData]
        public void CalculateBrokeragePercentageFee_with_100000_returns_expected_result(
            BrokerageFeeRate brokerageFeeRate)
        {
            var sut = new Func<BrokerageFeeRate, decimal, decimal>(BrokerageFeeExtensions
                .CalculateBrokeragePercentageFee);

            var result = sut(brokerageFeeRate, 100_000);

            result.Should().Be(420m);
        }

        [Theory, CalculatorAutoData]
        public void CalculateBrokeragePercentageFee_with_150000_returns_expected_result(
            BrokerageFeeRate brokerageFeeRate)
        {
            var sut = new Func<BrokerageFeeRate, decimal, decimal>(BrokerageFeeExtensions
                .CalculateBrokeragePercentageFee);

            var result = sut(brokerageFeeRate, 150_000);

            result.Should().Be(315m);
        }

        [Theory, CalculatorAutoData]
        public void CalculateOnlineTradingRebate_returns_expected_result(BrokerageFeeRate brokerageFeeRate)
        {
            var sut = new Func<BrokerageFeeRate, decimal, decimal>(BrokerageFeeExtensions.CalculateOnlineTradingRebate);

            var result = sut(brokerageFeeRate, 10);

            result.Should().Be(0);
        }

        [Theory, CalculatorAutoData]
        public void CalculateStampDuty_with_1000_returns_expected_result(StampDutyRate stampDutyRate)
        {
            var sut = new Func<StampDutyRate, decimal, decimal>(StampDutyExtensions.CalculateStampDuty);

            var result = sut(stampDutyRate, 1_000);

            result.Should().Be(1);
        }

        [Theory, CalculatorAutoData]
        public void CalculateStampDuty_with_1001_returns_expected_result(StampDutyRate stampDutyRate)
        {
            var sut = new Func<StampDutyRate, decimal, decimal>(StampDutyExtensions.CalculateStampDuty);

            var result = sut(stampDutyRate, 1_001);

            result.Should().Be(2);
        }

        [Theory, CalculatorAutoData]
        public void CalculateStampDuty_with_500_returns_expected_result(StampDutyRate stampDutyRate)
        {
            var sut = new Func<StampDutyRate, decimal, decimal>(StampDutyExtensions.CalculateStampDuty);

            var result = sut(stampDutyRate, 500);

            result.Should().Be(1);
        }

        private class CalculatorAutoDataAttribute : AutoDataAttribute
        {
            public CalculatorAutoDataAttribute() : base(() => new Fixture().Customize(new Customization()))
            {
            }
        }

        private class Customization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                var brokerageFee = new BrokerageFeeRate(
                    Percent(0.42m),
                    new BrokeragePercentageRates(new[]
                    {
                        new BrokeragePercentageRate(100_000, Percent(0.21m))
                    }),
                    12, Percent(0), Percent(0.15m)
                );
                fixture.Register(() => brokerageFee);

                var clearingFee = new ClearingFeeRate(new Percent(0.03m), 1_000);
                fixture.Register(() => clearingFee);

                var stampDuty = new StampDutyRate(1, 1_000, 200);
                fixture.Register(() => stampDuty);

                var salesServiceTax = new SalesServiceTaxRate(Percent(6), Percent(0), Percent(0));
                fixture.Register(() => salesServiceTax);
            }
        }
    }
}