using System.Collections.Generic;

namespace BursaCalculator.Persistence
{
    public static class Data
    {
        public static IEnumerable<DbFee> DefaultFeeProfiles()
        {
            return new[]
            {
                new DbFee
                {
                    Name = "Affin Hwang",
                    BrokerageInitialPercentageRate = 0.7m,
                    BrokeragePercentageRates = new[]
                    {
                        new DbBrokeragePercentageRate {AmountAbove = 100_000, Rate = 0.5m}
                    },
                    BrokerageMinimumFee = 28,
                    BrokerageOnlineTradingRebate = 0,
                    BrokerageIntraDayFee = 0.15m,

                    ClearingFeeClearingRate = 0.03m,
                    ClearingFeeMaximumFee = 1_000,

                    StampDutyAmount = 1m,
                    StampDutyForEvery = 1_000m,
                    StampDutyMaxAmount = 200m,

                    TaxBrokerage = 6,
                    TaxClearingFee = 0,
                    TaxStampDuty = 0
                },
                new DbFee
                {
                    Name = "AmEquities",
                    BrokerageInitialPercentageRate = 0.05m,
                    BrokeragePercentageRates = new DbBrokeragePercentageRate[] { },
                    BrokerageMinimumFee = 8,
                    BrokerageOnlineTradingRebate = 0,
                    BrokerageIntraDayFee = 0.15m,

                    ClearingFeeClearingRate = 0.03m,
                    ClearingFeeMaximumFee = 1_000,

                    StampDutyAmount = 1m,
                    StampDutyForEvery = 1_000m,
                    StampDutyMaxAmount = 200m,

                    TaxBrokerage = 6,
                    TaxClearingFee = 0,
                    TaxStampDuty = 0
                },
                new DbFee
                {
                    Name = "BIMB Securities",
                    BrokerageInitialPercentageRate = 0.3m,
                    BrokeragePercentageRates = new[]
                    {
                        new DbBrokeragePercentageRate {AmountAbove = 100_000, Rate = 0.2m}
                    },
                    BrokerageMinimumFee = 14,
                    BrokerageOnlineTradingRebate = 0,
                    BrokerageIntraDayFee = 0.15m,

                    ClearingFeeClearingRate = 0.03m,
                    ClearingFeeMaximumFee = 1_000,

                    StampDutyAmount = 1m,
                    StampDutyForEvery = 1_000m,
                    StampDutyMaxAmount = 200m,

                    TaxBrokerage = 6,
                    TaxClearingFee = 0,
                    TaxStampDuty = 0
                },
                new DbFee
                {
                    Name = "CIMB Clicks Trader",
                    BrokerageInitialPercentageRate = 0.0388m,
                    BrokeragePercentageRates = new DbBrokeragePercentageRate[] { },
                    BrokerageMinimumFee = 8.88m,
                    BrokerageOnlineTradingRebate = 0,
                    BrokerageIntraDayFee = 0.15m,

                    ClearingFeeClearingRate = 0.03m,
                    ClearingFeeMaximumFee = 1_000,

                    StampDutyAmount = 1m,
                    StampDutyForEvery = 1_000m,
                    StampDutyMaxAmount = 200m,

                    TaxBrokerage = 6,
                    TaxClearingFee = 0,
                    TaxStampDuty = 0
                },
                new DbFee
                {
                    Name = "HL Broking",
                    BrokerageInitialPercentageRate = 0.4028m,
                    BrokeragePercentageRates = new[]
                    {
                        new DbBrokeragePercentageRate {AmountAbove = 100_000, Rate = 0.1908m}
                    },
                    BrokerageMinimumFee = 12.72m,
                    BrokerageOnlineTradingRebate = 0,
                    BrokerageIntraDayFee = 0.1m,

                    ClearingFeeClearingRate = 0.03m,
                    ClearingFeeMaximumFee = 1_000,

                    StampDutyAmount = 1m,
                    StampDutyForEvery = 1_000m,
                    StampDutyMaxAmount = 200m,

                    TaxBrokerage = 6,
                    TaxClearingFee = 0,
                    TaxStampDuty = 0
                },
                new DbFee
                {
                    Name = "KenTrade",
                    BrokerageInitialPercentageRate = 0.42m,
                    BrokeragePercentageRates = new DbBrokeragePercentageRate[] { },
                    BrokerageMinimumFee = 28,
                    BrokerageOnlineTradingRebate = 0,
                    BrokerageIntraDayFee = 0.15m,

                    ClearingFeeClearingRate = 0.03m,
                    ClearingFeeMaximumFee = 1_000,

                    StampDutyAmount = 1m,
                    StampDutyForEvery = 1_000m,
                    StampDutyMaxAmount = 200m,

                    TaxBrokerage = 6,
                    TaxClearingFee = 0,
                    TaxStampDuty = 0
                },
                new DbFee
                {
                    Name = "M+ Silver",
                    BrokerageInitialPercentageRate = 0.08m,
                    BrokeragePercentageRates = new[]
                    {
                        new DbBrokeragePercentageRate {AmountAbove = 50_000, Rate = 0.05m}
                    },
                    BrokerageMinimumFee = 8,
                    BrokerageOnlineTradingRebate = 0,
                    BrokerageIntraDayFee = 0.1m,

                    ClearingFeeClearingRate = 0.03m,
                    ClearingFeeMaximumFee = 1_000,

                    StampDutyAmount = 1m,
                    StampDutyForEvery = 1_000m,
                    StampDutyMaxAmount = 200m,

                    TaxBrokerage = 6,
                    TaxClearingFee = 0,
                    TaxStampDuty = 0
                },
                new DbFee
                {
                    Name = "PB Sharelink",
                    BrokerageInitialPercentageRate = 0.42m,
                    BrokeragePercentageRates = new[]
                    {
                        new DbBrokeragePercentageRate {AmountAbove = 100_000, Rate = 0.21m}
                    },
                    BrokerageMinimumFee = 12,
                    BrokerageOnlineTradingRebate = 0,
                    BrokerageIntraDayFee = 0.15m,

                    ClearingFeeClearingRate = 0.03m,
                    ClearingFeeMaximumFee = 1_000,

                    StampDutyAmount = 1m,
                    StampDutyForEvery = 1_000m,
                    StampDutyMaxAmount = 200m,

                    TaxBrokerage = 6,
                    TaxClearingFee = 0,
                    TaxStampDuty = 0
                },
                new DbFee
                {
                    Name = "UOB Kay Hian",
                    BrokerageInitialPercentageRate = 0.42m,
                    BrokeragePercentageRates = new[]
                    {
                        new DbBrokeragePercentageRate {AmountAbove = 100_000, Rate = 0.21m}
                    },
                    BrokerageMinimumFee = 28,
                    BrokerageOnlineTradingRebate = 0,
                    BrokerageIntraDayFee = 0.1m,

                    ClearingFeeClearingRate = 0.03m,
                    ClearingFeeMaximumFee = 1_000,

                    StampDutyAmount = 1m,
                    StampDutyForEvery = 1_000m,
                    StampDutyMaxAmount = 200m,

                    TaxBrokerage = 6,
                    TaxClearingFee = 0,
                    TaxStampDuty = 0
                }
            };
        }
    }
}