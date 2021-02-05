using System.Collections.Generic;
using System.Linq;

namespace BursaCalculator.Persistence
{
    public class FeeSettings
    {
        public IEnumerable<BrokeragePercentageRateSettings> BrokeragePercentageRates { get; set; } = Enumerable.Empty<BrokeragePercentageRateSettings>();
        public decimal BrokerageOnlineTradingRebate { get; set; }
        public decimal BrokerageMinimumFee { get; set; }
        public decimal BrokerageIntraDayFee { get; set; }

        
        public decimal ClearingClearingRate { get; set; }
        public decimal ClearingMaximumFee { get; set; }

        public decimal StampDutyAmount { get; set; }
        public decimal StampDutyForEvery { get; set; } = 1;
        public decimal StampDutyMaxAmount { get; set; }

        public decimal SalesServiceTaxRateBrokerage { get; set; }
        public decimal SalesServiceTaxRateClearingFee { get; set; }
        public decimal SalesServiceTaxRateStampDuty { get; set; }
    }
}