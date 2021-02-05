namespace BursaCalculator.Core
{
    public record Fee
    {
        public Fee(BrokerageFeeRate brokerageFeeRate, ClearingFeeRate clearingFeeRate, StampDutyRate stampDutyRate, SalesServiceTaxRate salesServiceTaxRate)
        {
            BrokerageFeeRate = brokerageFeeRate;
            ClearingFeeRate = clearingFeeRate;
            StampDutyRate = stampDutyRate;
            SalesServiceTaxRate = salesServiceTaxRate;
        }

        public BrokerageFeeRate BrokerageFeeRate { get; private init; }
        public ClearingFeeRate ClearingFeeRate { get; private init; }
        public StampDutyRate StampDutyRate { get; private init; }
        public SalesServiceTaxRate SalesServiceTaxRate { get; private init; }
    }

}