using LiteDB;

namespace BursaCalculator.Persistence
{
    public class DbFee
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public decimal BrokerageInitialPercentageRate { get; set; }
        public DbBrokeragePercentageRate[] BrokeragePercentageRates { get; set; } = { };
        public decimal BrokerageMinimumFee { get; set; }
        public decimal BrokerageOnlineTradingRebate { get; set; }
        public decimal BrokerageIntraDayFee { get; set; }

        public decimal ClearingFeeClearingRate { get; set; }
        public decimal ClearingFeeMaximumFee { get; set; }

        public decimal StampDutyAmount { get; set; }
        public decimal StampDutyForEvery { get; set; }
        public decimal StampDutyMaxAmount { get; set; }

        public decimal TaxBrokerage { get; set; }
        public decimal TaxClearingFee { get; set; }
        public decimal TaxStampDuty { get; set; }
    }

    public class DbBrokeragePercentageRate
    {
        public decimal AmountAbove { get; set; }
        public decimal Rate { get; set; }
    }
}