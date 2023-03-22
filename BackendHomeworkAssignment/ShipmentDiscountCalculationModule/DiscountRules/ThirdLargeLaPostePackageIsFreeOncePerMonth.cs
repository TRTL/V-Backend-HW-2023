using ShipmentDiscountCalculationModule.DiscountRules.Interfaces;
using ShipmentDiscountCalculationModule.Models;
using ShipmentDiscountCalculationModule.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.DiscountRules
{
    public class ThirdLargeLaPostePackageIsFreeOncePerMonth : IDiscountRule
    {
        private int _largeLaPostePackageCountTillFree = 3;
        private bool _freeLargeLaPostePackageUsed = false;
        private string? _monthTracker;

        public void TryApplyDiscountRule(Shipment shipment, ref decimal remainingMonthlyDiscountFund)
        {
            if (shipment.PackageSizeCode == EPackageSize.L &&
                shipment.CarrierCode == EProvider.LP &&
                remainingMonthlyDiscountFund > 0)
            {
                if (WasMonthTrackerNeverSet() || IsNewMonthAcordingToMonthTracker(shipment.Date)) SetMonthTracker(shipment.Date);

                if (_largeLaPostePackageCountTillFree > 0) _largeLaPostePackageCountTillFree--;

                if (!_freeLargeLaPostePackageUsed &&
                    _largeLaPostePackageCountTillFree == 0)
                {
                    _freeLargeLaPostePackageUsed = true;
                    GetDiscountForLargeLaPostePackage(shipment, ref remainingMonthlyDiscountFund);
                }
            }
        }

        private bool WasMonthTrackerNeverSet() => _monthTracker == null;

        private void SetMonthTracker(DateOnly shipmentDate)
        {
            _monthTracker = shipmentDate.Year.ToString() + shipmentDate.Month.ToString();
            MonthlyDiscountReset();
        }

        private bool IsNewMonthAcordingToMonthTracker(DateOnly shipmentDate)
        {
            var shipmentDateYearMonth = shipmentDate.Year.ToString() + shipmentDate.Month.ToString();
            if (_monthTracker != shipmentDateYearMonth) return true;
            return false;
        }

        private void MonthlyDiscountReset()
        {
            _largeLaPostePackageCountTillFree = 3;
            _freeLargeLaPostePackageUsed = false;
        }

        private void GetDiscountForLargeLaPostePackage(Shipment shipment, ref decimal remainingMonthlyDiscountFund)
        {
            if (remainingMonthlyDiscountFund >= shipment.ShipmentPrice)
            {
                remainingMonthlyDiscountFund = remainingMonthlyDiscountFund - (decimal)shipment.ShipmentPrice;
                shipment.ShipmentDiscount = shipment.ShipmentPrice;
                return;
            }

            shipment.ShipmentDiscount = remainingMonthlyDiscountFund;
            remainingMonthlyDiscountFund -= remainingMonthlyDiscountFund;
        }
    }
}
