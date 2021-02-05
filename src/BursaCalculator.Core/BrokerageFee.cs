using System;

namespace BursaCalculator.Core
{
    public record BrokerageFee
    {
        public BrokerageFee(decimal percentageFee, decimal onlineTradingRebate, decimal stocks)
        {
            if(percentageFee < 0) throw new ArgumentOutOfRangeException(nameof(percentageFee));
            if(onlineTradingRebate < 0) throw new ArgumentOutOfRangeException(nameof(onlineTradingRebate));
            if(stocks < 0) throw new ArgumentOutOfRangeException(nameof(stocks));

            PercentageFee = percentageFee;
            OnlineTradingRebate = onlineTradingRebate;
            Stocks = stocks;
        }
        public decimal PercentageFee { get; private init; }
        public decimal OnlineTradingRebate { get; private init; }
        public decimal Stocks { get; private init; }
    }
}