namespace BursaCalculator.Core
{
    public static class FeeExtensions
    {
        public static Fee Fee(BrokerageFeeRate brokerageFeeRate, ClearingFeeRate clearingFeeRate, StampDutyRate stampDutyRate,
            SalesServiceTaxRate salesServiceTaxRate) =>
            new Fee(brokerageFeeRate, clearingFeeRate, stampDutyRate, salesServiceTaxRate);

    }

}