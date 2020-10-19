using System;
using System.Collections.Generic;
using System.Linq;
using BursaCalculator.Core.Infrastructure;

namespace BursaCalculator.Core
{
    public static class BrokerageFeeExtensions
    {
        public static decimal CalculateBrokerageFee(BrokerageFeeRate rate, decimal amount) =>
            PercentageBrokerageFee(rate.PercentageRates, amount) -
            CalculateOnlineTradingRebate(rate, amount);

        public static decimal CalculateIntraDayBrokerageFee(BrokeragePercentageRate rate, decimal amount) =>
            throw new NotImplementedException();


        public static decimal CalculateIntraDayFee(BrokerageFeeRate feeRate, decimal amount) =>
            feeRate.IntraDayFee * amount;

        public static decimal CalculateOnlineTradingRebate(BrokerageFeeRate feeRate, decimal amount) =>
            feeRate.OnlineTradingRebate * amount;

        private static Percent Lookup(IEnumerable<BrokeragePercentageRate> enumerable, decimal amount) => 
            enumerable.Filter(r => amount >= r.AmountFrom).Last().Rate;

        private static decimal PercentageBrokerageFee(IEnumerable<BrokeragePercentageRate> rates, decimal amount) =>
            Lookup(rates, amount) * amount;
    }
}