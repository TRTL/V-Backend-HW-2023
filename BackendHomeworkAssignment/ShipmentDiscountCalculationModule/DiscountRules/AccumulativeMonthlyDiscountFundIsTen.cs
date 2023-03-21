using ShipmentDiscountCalculationModule.DiscountRules.Interfaces;
using ShipmentDiscountCalculationModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.DiscountRules
{
    internal class AccumulativeMonthlyDiscountFundIsTen : IDiscountRule
    {
        private decimal _maxMonthlyDiscountFund = 10;
        private int? _monthTracker;

        public void TryApplyDiscountRule(Shipment shipment, ref decimal remainingMonthlyDiscountFund)
        {
            // month tracked was never set
            if (_monthTracker == null) _monthTracker = shipment.Date.Month;

            // new month in transaction → reset remainingMonthlyDiscountFund to default and track new month
            if (_monthTracker != shipment.Date.Month)
            {
                remainingMonthlyDiscountFund = _maxMonthlyDiscountFund;
                _monthTracker = shipment.Date.Month;
            }
        }
    }
}
