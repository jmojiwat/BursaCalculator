using FluentAssertions;
using Xunit;
using static BursaCalculator.Core.Infrastructure.UnitsQuantityExtensions;

namespace BursaCalculator.Core.Tests
{
    public class QuantityTest
    {
        [Fact]
        public void QuantityEquality_returns_expected_result()
        {
            var left = 1 * bl;
            var right = 100 * s;

            left.Should().BeEquivalentTo(right);
        }

        [Fact]
        public void QuantityEquality_returns_expected_result1()
        {
            var left = 1.Lots();
            var right = 100.Shares();

            left.Should().BeEquivalentTo(right);
        }
    }
}