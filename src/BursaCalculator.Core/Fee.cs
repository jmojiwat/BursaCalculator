namespace BursaCalculator.Core
{
    public class Fee
    {
        public Fee(BrokerageFeeRate brokerageFeeRate, ClearingFeeRate clearingFeeRate, StampDutyRate stampDutyRate,
            SalesServiceTaxRate salesServiceTaxRate)
        {
            BrokerageFeeRate = brokerageFeeRate;
            ClearingFeeRate = clearingFeeRate;
            StampDutyRate = stampDutyRate;
            SalesServiceTaxRate = salesServiceTaxRate;
        }

        public BrokerageFeeRate BrokerageFeeRate { get; }
        public ClearingFeeRate ClearingFeeRate { get; }
        public StampDutyRate StampDutyRate { get; }
        public SalesServiceTaxRate SalesServiceTaxRate { get; }
    }

    public class FeeExtensions
    {
    }
}