using BursaCalculator.Core.Infrastructure;
using FluentAssertions;
using LanguageExt.UnitTesting;
using Xunit;
using static BursaCalculator.Core.Infrastructure.LotExtensions;
using static BursaCalculator.Core.Infrastructure.MoneyExtensions;
using static BursaCalculator.Core.Infrastructure.PercentExtensions;
using static BursaCalculator.Core.PositionAltCalculatorExtensions;

namespace BursaCalculator.Core.Tests
{
    public class PositionAltTest
    {
        [Theory]
        [InlineData(100_000, 1_500, 1.5)]

        public void Risk_returns_expected_result(decimal capital, decimal accountRisk, decimal expectedResult)
        {
            var result = Risk(Money(capital), Money(accountRisk));
            
            result.ShouldBeSome(r => r.Should().Be(Percent(expectedResult)));
        }
        
        [Theory]
        [InlineData(2_250, 0.5, 225, 0.4)]
        [InlineData(1_500, 0.5, 75, 0.3)]

        public void StopLossPrice_returns_expected_result(decimal accountRisk, decimal entryPrice, int lots, decimal expectedResult)
        {
            var result = StopLossPrice(Money(accountRisk), Money(entryPrice), Lot(lots));
            
            result.ShouldBeSome(r => r.Should().Be(Money(expectedResult)));
        }
        
        [Theory]
        [InlineData(0.5, 25, 0.375)]
        [InlineData(1, 50, 0.5)]

        public void StopLossPrice_from_percent_returns_expected_result(decimal entryPrice, decimal stopLossPercent, decimal expectedResult)
        {
            var result = StopLossPrice(Money(entryPrice), Percent(stopLossPercent));
            
            result.Should().Be(Money(expectedResult));
        }
        
        [Theory]
        [InlineData(0.5, 20, 0.4)]
        [InlineData(1, 60, 0.4)]

        public void StopLossPrice_from_tick_returns_expected_result(decimal entryPrice, int stopLossTick, decimal expectedResult)
        {
            var result = StopLossPrice(Money(entryPrice), TickExtensions.Tick(stopLossTick));
            
            result.Should().Be(Money(expectedResult));
        }
        
        [Theory]
        [InlineData(0.5, 20, 0.6)]
        [InlineData(0.5, 30, 0.65)]

        public void TargetPrice_from_percent_returns_expected_result(decimal entryPrice, decimal targetPercent, decimal expectedResult)
        {
            var result = TargetPrice(Money(entryPrice), Percent(targetPercent));
            
            result.Should().Be(Money(expectedResult));
        }
        
        [Theory]
        [InlineData(0.5, 20, 0.6)]
        [InlineData(0.5, 30, 0.65)]

        public void TargetPrice_from_tick_returns_expected_result(decimal entryPrice, int targetTick, decimal expectedResult)
        {
            var result = TargetPrice(Money(entryPrice), TickExtensions.Tick(targetTick));
            
            result.Should().Be(Money(expectedResult));
        }
    }
}