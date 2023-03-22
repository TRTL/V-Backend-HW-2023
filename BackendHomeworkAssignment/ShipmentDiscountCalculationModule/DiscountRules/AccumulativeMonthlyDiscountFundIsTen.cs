using ShipmentDiscountCalculationModule.DiscountRules.Interfaces;
using ShipmentDiscountCalculationModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.DiscountRules
{
    public class AccumulativeMonthlyDiscountFundIsTen : IDiscountRule
    {
        private decimal _maxMonthlyDiscountFund = 10;
        private string? _monthTracker;

        public void TryApplyDiscountRule(Shipment shipment, ref decimal remainingMonthlyDiscountFund)
        {
            if (WasMonthTrackerNeverSet()) SetMonthTracker(shipment.Date);

            if (IsNewMonthAcordingToMonthTracker(shipment.Date))
            {
                remainingMonthlyDiscountFund = _maxMonthlyDiscountFund;
                SetMonthTracker(shipment.Date);
            }
        }

        private bool WasMonthTrackerNeverSet() => _monthTracker == null;

        private void SetMonthTracker(DateOnly shipmentDate)
        {
            _monthTracker = shipmentDate.Year.ToString() + shipmentDate.Month.ToString();
        }

        private bool IsNewMonthAcordingToMonthTracker(DateOnly shipmentDate)
        {
            var shipmentDateYearMonth = shipmentDate.Year.ToString() + shipmentDate.Month.ToString();
            if (_monthTracker != shipmentDateYearMonth) return true;
            return false;
        }
    }
}
