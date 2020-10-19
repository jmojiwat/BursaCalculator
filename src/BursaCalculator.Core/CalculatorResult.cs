using BursaCalculator.Core.Infrastructure;

namespace BursaCalculator.Core
{
    public class CalculatorResult
    {
        public CalculatorResult(decimal purchaseBrokerageFee,
            decimal purchaseClearingFee,
            decimal purchaseStampDuty,
            decimal sellBrokerageFee,
            decimal sellClearingFee,
            decimal sellStampDuty,
            decimal profit,
            Percent profitPercent,
            decimal profitAfterFees,
            Percent profitWithFeesPercent)
        {
            PurchaseBrokerageFee = purchaseBrokerageFee;
            PurchaseClearingFee = purchaseClearingFee;
            PurchaseStampDuty = purchaseStampDuty;
            SellBrokerageFee = sellBrokerageFee;
            SellClearingFee = sellClearingFee;
            SellStampDuty = sellStampDuty;
            Profit = profit;
            ProfitPercent = profitPercent;
            ProfitAfterFees = profitAfterFees;
            ProfitWithFeesPercent = profitWithFeesPercent;
        }

        public decimal PurchaseBrokerageFee { get; }
        public decimal PurchaseClearingFee { get; }
        public decimal PurchaseStampDuty { get; }
        public decimal SellBrokerageFee { get; }
        public decimal SellClearingFee { get; }
        public decimal SellStampDuty { get; }
        public decimal Profit { get; }
        public Percent ProfitPercent { get; }
        public decimal ProfitAfterFees { get; }
        public Percent ProfitWithFeesPercent { get; }
    }
}