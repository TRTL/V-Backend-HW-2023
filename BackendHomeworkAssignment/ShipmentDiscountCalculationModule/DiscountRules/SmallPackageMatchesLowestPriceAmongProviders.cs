using ShipmentDiscountCalculationModule.Data;
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
    public class SmallPackageMatchesLowestPriceAmongProviders : IDiscountRule
    {
        private decimal _lowestSmallPackagePrice;

        public SmallPackageMatchesLowestPriceAmongProviders()
        {
            _lowestSmallPackagePrice = GetLowestPrice(EPackageSize.S);
        }

        private decimal GetLowestPrice(EPackageSize packageSize)
        {
            var pricesForSize = ProviderPriceData.priceList.Where(p => p.PackageSize == packageSize);
            return pricesForSize.Min(p => p.Price);
        }

        public void TryApplyDiscountRule(Shipment shipment, ref decimal remainingMonthlyDiscountFund)
        {
            if (shipment.PackageSizeCode == EPackageSize.S &&
                remainingMonthlyDiscountFund > 0)
            {
                GetDiscountForSmallPackage(shipment, ref remainingMonthlyDiscountFund);
            }
        }

        private void GetDiscountForSmallPackage(Shipment shipment, ref decimal remainingMonthlyDiscountFund)
        {
            if (shipment.ShipmentPrice == _lowestSmallPackagePrice)
            {
                shipment.ShipmentDiscount =  null; 
                return;
            }

            if (remainingMonthlyDiscountFund >= shipment.ShipmentPrice - _lowestSmallPackagePrice)
            {
                remainingMonthlyDiscountFund = remainingMonthlyDiscountFund - (shipment.ShipmentPrice.Value - _lowestSmallPackagePrice);
                shipment.ShipmentDiscount = shipment.ShipmentPrice - _lowestSmallPackagePrice;
                return;
            }

            shipment.ShipmentDiscount = remainingMonthlyDiscountFund;
            remainingMonthlyDiscountFund -= remainingMonthlyDiscountFund;
        }
    }
}
