using BursaCalculator.Core.Infrastructure;
using FluentAssertions;
using LanguageExt.UnitTesting;
using Xunit;
using static BursaCalculator.Core.Infrastructure.MoneyExtensions;
using static BursaCalculator.Core.Infrastructure.PercentExtensions;
using static BursaCalculator.Core.PositionCalculatorExtensions;

namespace BursaCalculator.Core.Tests
{
    public class PositionTest
    {
        [Theory]
        [InlineData(100_000, 1.5, 1_500)]

        public void AccountRisk_returns_expected_result(decimal amount, decimal percent, decimal expectedResult)
        {
            var result = AccountRisk(Money(amount), Percent(percent));
            
            result.Should().Be(Money(expectedResult));
        }
        
        [Theory]
        [InlineData(3.35, 2.59, 1_500, 19)]
        [InlineData(1.07, 0.99, 1_500, 187)]

        public void Lots_returns_expected_result(decimal entryPrice, decimal stopLossPrice, decimal accountRisk, int expectedResult)
        {
            var result = Shares(Money(entryPrice), Money(stopLossPrice), Money(accountRisk));

            var quantity = expectedResult.Lots();
            
            result.ShouldBeSome(r => r.Lots.Should().Be(quantity.Lots));
        }
        
        [Theory]
        [InlineData(1.07, 0.92, 14.02)]
        public void StopLossPercentage_returns_expected_result(decimal entryPrice, decimal stopLossPrice, decimal expectedResult)
        {
            var result = StopLossPercentage(Money(entryPrice), Money(stopLossPrice));
            
            var percent = Percent(expectedResult);
            
            result.ShouldBeSome(r => r.Should().BeEquivalentTo(percent));
        }
        
        [Theory]
        [InlineData(1.07, 0.99, 16)]
        [InlineData(0.475, 0.450, 5)]

        public void StopLossTick_returns_expected_result(decimal entryPrice, decimal stopLossPrice, int expectedResult)
        {
            var result = StopLossTick(Money(entryPrice), Money(stopLossPrice));
            
            var ticks = new Tick(expectedResult);
            
            result.Should().Be(ticks);
        }
        
        [Theory]
        [InlineData(1.07, 1.22, 14.02)]
        public void TargetPercentage_returns_expected_result(decimal entryPrice, decimal targetPrice, decimal expectedResult)
        {
            var result = TargetPercentage(Money(entryPrice), Money(targetPrice));
            
            var percent = Percent(expectedResult);
            
            result.ShouldBeSome(r => r.Should().BeEquivalentTo(percent));
        }

        [Theory]
        [InlineData(1.07, 1.22, 15)]

        public void TargetTick_returns_expected_result(decimal entryPrice, decimal targetPrice, int expectedResult)
        {
            var result = TargetTick(Money(entryPrice), Money(targetPrice));
            
            var ticks = new Tick(expectedResult);
            
            result.Should().Be(ticks);
        }

        [Theory]
        [InlineData(1.07, 0.99, 18700, 1496)]

        public void StopLossAmount_returns_expected_result(decimal entryPrice, decimal stopLossPrice, int quantity, decimal expectedResult)
        {
            var result = StopLossAmount(Money(entryPrice), Money(stopLossPrice), quantity.Shares());
            
            result.Should().Be(Money(expectedResult));
        }
        
        [Theory]
        [InlineData(1.07, 1.2, 187, 2431)]

        public void TargetAmount_returns_expected_result(decimal entryPrice, decimal stopLossPrice, int quantity, decimal expectedResult)
        {
            var shares = quantity.Lots();
            var result = TargetAmount(Money(entryPrice), Money(stopLossPrice), shares);
            
            result.Should().Be(Money(expectedResult));
        }
        
        [Theory]
        [InlineData(1.07, 187, 20_009)]

        public void EntryAmount_returns_expected_result(decimal entryPrice, int lots, decimal expectedResult)
        {
            var result = EntryAmount(Money(entryPrice), lots * UnitsQuantityExtensions.bl);
            
            result.Should().Be(Money(expectedResult));
        }
        
        [Theory]
        [InlineData(0.975, 0.895, 1.16, 2.31)]

        public void RiskReward_returns_expected_result(decimal entryPrice, decimal stopLossPrice, decimal targetPrice, decimal expectedResult)
        {
            var result = RiskReward(Money(entryPrice), Money(stopLossPrice), Money(targetPrice));
            
            result.ShouldBeSome(rr => rr.Should().BeApproximately(expectedResult, 0.01m));
        }
        
    }
}