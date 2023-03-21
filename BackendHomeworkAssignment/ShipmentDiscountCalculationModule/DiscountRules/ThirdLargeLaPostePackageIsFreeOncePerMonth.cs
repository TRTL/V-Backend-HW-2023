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
    internal class ThirdLargeLaPostePackageIsFreeOncePerMonth : IDiscountRule
    {
        private int largeLaPostePackageCountTillFree = 3;
        private bool freeLargeLaPostePackageUsed = false;
        private int? monthTracker;

        public void TryApplyDiscountRule(Shipment shipment, ref decimal remainingMonthlyDiscountFund)
        {
            // logic for L size package and LP Provider
            if (shipment.PackageSizeCode == EPackageSize.L &&
                shipment.CarrierCode == EProvider.LP &&
                remainingMonthlyDiscountFund > 0)
            {
                // month tracked was never set
                if (monthTracker == null) monthTracker = shipment.Date.Month;

                // new month in transaction → reset everything to defaults and track new month
                if (monthTracker != shipment.Date.Month) Reset(shipment.Date.Month);

                // counts down till free package
                if (largeLaPostePackageCountTillFree > 0) largeLaPostePackageCountTillFree--;

                if (!freeLargeLaPostePackageUsed &&
                    largeLaPostePackageCountTillFree == 0)
                {
                    freeLargeLaPostePackageUsed = true;
                    GetDiscountForLargeLaPostePackage(shipment, ref remainingMonthlyDiscountFund);
                }
            }
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

        private void Reset(int newMonth)
        {
            largeLaPostePackageCountTillFree = 3;
            freeLargeLaPostePackageUsed = false;
            monthTracker = newMonth;
        }
    }
}
