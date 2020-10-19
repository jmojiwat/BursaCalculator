using System;
using BursaCalculator.Core.Infrastructure;

namespace BursaCalculator.Core
{
    public class Calculator
    {
        private readonly BrokerageFeeRate brokerageFeeRate;
        private readonly ClearingFeeRate clearingFeeRate;
        private readonly StampDutyRate stampDutyRate;
        private readonly SalesServiceTaxRate taxRate;

        public Calculator(BrokerageFeeRate brokerageFeeRate, ClearingFeeRate clearingFeeRate,
            StampDutyRate stampDutyRate, SalesServiceTaxRate taxRate)
        {
            this.brokerageFeeRate = brokerageFeeRate;
            this.clearingFeeRate = clearingFeeRate;
            this.stampDutyRate = stampDutyRate;
            this.taxRate = taxRate;
        }

        public CalculatorResult Calculate(decimal purchasePricePerShare, Lot held, decimal sellPricePerShare)
        {
            throw new NotImplementedException();
            /*
            var purchasePrice = TotalPrice(purchasePricePerShare, held);
            var (purchaseBrokerageFee, purchaseClearingFee, purchaseStampDuty) = CalculateFees(purchasePrice);

            var purchasePriceWithFees = purchasePrice + purchaseBrokerageFee + purchaseClearingFee + purchaseStampDuty;

            var sellPrice = TotalPrice(sellPricePerShare, held);
            var (sellBrokerageFee, sellClearingFee, sellStampDuty) = CalculateFees(sellPrice);

            var sellPriceWithFees = sellPrice + sellBrokerageFee + sellClearingFee + sellStampDuty;

            var profit = Profit(purchasePrice, sellPrice);
            var profitPercent = ProfitPercent(purchasePrice, sellPrice);

            var profitAfterFees = Profit(purchasePriceWithFees, sellPriceWithFees);
            var profitAfterFeesPercent = ProfitPercent(purchasePriceWithFees, sellPriceWithFees);

            return new CalculatorResult(
                purchaseBrokerageFee,
                purchaseClearingFee,
                purchaseStampDuty,
                sellBrokerageFee,
                sellClearingFee,
                sellStampDuty,
                profit,
                profitPercent,
                profitAfterFees,
                profitAfterFeesPercent);
        */
        }

        /*
        public decimal Project(decimal purchasePricePerShare, Lot sharesHeld, decimal targetProfit)
        {
            var purchasePrice = TotalPrice(purchasePricePerShare, sharesHeld);
            var (purchaseBrokerageFee, purchaseClearingFee, purchaseStampDuty) = CalculateFees(purchasePrice);

            var purchasePriceWithFees = purchasePrice + purchaseBrokerageFee + purchaseClearingFee + purchaseStampDuty;

            var targetProfitAfterFees = 

        }
        */

        /*
        private (decimal brokerageFee, decimal clearingFee, decimal stampDuty) CalculateFees(decimal price)
        {
            var brokerageFeeAmount = BrokerageFeeExtensions.CalculateBrokerageFee(brokerageFee, price)
                .Apply(fee => BrokerageTax(taxRate, fee));

            var clearingFeeAmount = CalculateClearingFee(clearingFee, price)
                .Apply(fee => ClearingFeeTax(taxRate, fee));

            var stampDutyAmount = CalculateStampDuty(stampDutyRate, price)
                .Apply(fee => StampDutyTax(taxRate, fee));

            return (brokerageFeeAmount, clearingFeeAmount, stampDutyAmount);
        }
        */

        private static decimal Profit(decimal purchasePrice, decimal sellPrice) => sellPrice - purchasePrice;

        private static Percent ProfitPercent(decimal purchasePrice, decimal sellPrice) =>
            new Percent((sellPrice - purchasePrice) / purchasePrice);

//        private static decimal TotalPrice(decimal pricePerShare, Lot held) => pricePerShare * held;
    }
}