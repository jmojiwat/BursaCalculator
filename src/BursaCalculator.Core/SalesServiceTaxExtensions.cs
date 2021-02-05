namespace BursaCalculator.Core
{
    public static class SalesServiceTaxExtensions
    {
        public static SalesServiceTax CalculateSalesServiceTax(SalesServiceTaxRate taxRate, in decimal brokerageFee,
            in decimal clearingFee, in decimal stampDuty) =>
            new SalesServiceTax
            {
                Brokerage = taxRate.Brokerage * brokerageFee,
                ClearingFee = taxRate.ClearingFee * clearingFee,
                StampDuty = taxRate.StampDuty * stampDuty
            };

        public static SalesServiceTax CalculateSalesServiceTax(SalesServiceTaxRate taxRate, TaxableFees fees) =>
            new SalesServiceTax
            {
                Brokerage = taxRate.Brokerage * fees.Brokerage,
                ClearingFee = taxRate.ClearingFee * fees.ClearingFee,
                StampDuty = taxRate.StampDuty * fees.StampDuty
            };

        public static decimal ToTotal(SalesServiceTax tax) =>
            tax.Brokerage + tax.ClearingFee + tax.StampDuty;
    }
}