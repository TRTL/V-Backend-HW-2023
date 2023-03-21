using ShipmentDiscountCalculationModule.Data;
using ShipmentDiscountCalculationModule.Models;
using ShipmentDiscountCalculationModule.Models.Enums;
using ShipmentDiscountCalculationModule.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.Services
{
    public class DiscountRule : IDiscountRule
    {
        private decimal lowestSmallPackagePrice;
        private int largeLaPostePackageCountTillFree = 3;
        private bool freeLargeLaPostePackageUsed = false;
        private int? monthTracker;
        private decimal monthlyDiscountFund = 10;

        public DiscountRule()
        {
            lowestSmallPackagePrice = GetLowestPrice(EPackageSize.S);
        }

        private decimal GetLowestPrice(EPackageSize packageSize)
        {
            var pricesForSize = ProviderPriceData.priceList.Where(p => p.PackageSize == packageSize);
            if (pricesForSize.Any())
                return pricesForSize.Min(p => p.Price);
            throw new ArgumentException($"No prices found for package size {packageSize}");
        }

        public Shipment GetShipmentPriceAndDiscount(Shipment transaction)
        {
            var shipment = new Shipment(transaction.Date, transaction.PackageSizeCode, transaction.CarrierCode);
            shipment.ShipmentPrice = GetShipmentPrice(shipment);
            shipment.ShipmentDiscount = CalculateShipmentDiscount(shipment);

            if (shipment.ShipmentDiscount != null) shipment.ShipmentPrice -=  shipment.ShipmentDiscount.Value;

            return shipment;
        }

        private decimal GetShipmentPrice(Shipment shipment) => ProviderPriceData.priceList.Find(p => p.PackageSize == shipment.PackageSizeCode && p.Provider == shipment.CarrierCode).Price;

        private decimal? CalculateShipmentDiscount(Shipment shipment)
        {
            // month tracked was never set
            if (monthTracker == null) monthTracker = shipment.Date.Month;

            // new month in transaction → reset everything to defaults and track new month
            if (monthTracker != shipment.Date.Month) Reset(shipment.Date.Month);

            // logic for L size package and LP Provider
            if (shipment.PackageSizeCode == EPackageSize.L && 
                shipment.CarrierCode == EProvider.LP)
            {
                // counts down till free package
                if (largeLaPostePackageCountTillFree > 0) largeLaPostePackageCountTillFree--;

                if (!freeLargeLaPostePackageUsed &&
                    largeLaPostePackageCountTillFree == 0 &&
                    monthlyDiscountFund != 0)
                {
                    freeLargeLaPostePackageUsed = true;
                    shipment.ShipmentDiscount = GetDiscountForLargeLaPostePackage(shipment);

                }
            }

            // logic for S size pachage
            if (shipment.PackageSizeCode == EPackageSize.S &&
                monthlyDiscountFund != 0)
            {
                shipment.ShipmentDiscount = GetDiscountForSmallPackage(shipment);
            }

            return shipment.ShipmentDiscount;
        }

        private decimal? GetDiscountForLargeLaPostePackage(Shipment shipment)
        {
            if (monthlyDiscountFund >= shipment.ShipmentPrice) 
            {
                monthlyDiscountFund = monthlyDiscountFund - (decimal)shipment.ShipmentPrice;
                return shipment.ShipmentPrice;                 
            }

            decimal discount = monthlyDiscountFund;
            monthlyDiscountFund -= monthlyDiscountFund;
            return discount;
        }

        private decimal? GetDiscountForSmallPackage(Shipment shipment)
        {
            if (shipment.ShipmentPrice == lowestSmallPackagePrice) return null;

            if (monthlyDiscountFund >= shipment.ShipmentPrice - lowestSmallPackagePrice)
            {
                monthlyDiscountFund = monthlyDiscountFund - (shipment.ShipmentPrice.Value - lowestSmallPackagePrice);
                return shipment.ShipmentPrice - lowestSmallPackagePrice;
            }

            decimal discount = monthlyDiscountFund;
            monthlyDiscountFund -= monthlyDiscountFund;
            return discount;
        }

        private void Reset(int newMonth)
        {
            largeLaPostePackageCountTillFree = 3;
            freeLargeLaPostePackageUsed = false;
            monthTracker = newMonth;
            monthlyDiscountFund = 10;
        }

    }
}
