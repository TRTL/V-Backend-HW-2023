using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShipmentDiscountCalculationModule.Models.Enums;
using ShipmentDiscountCalculationModule.Models;
using ShipmentDiscountCalculationModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipmentDiscountCalculationModule.Data;

namespace ShipmentDiscountCalculationModule.Services_Tests
{
    [TestClass]
    public class CarrierService_Tests
    {
        [TestMethod]
        public void GetCarrierPrice_ChecksProviderPriceData_SetsShipmentPriceForSmallPackageAndMrProvider()
        {
            // Arange
            var shipment = new Shipment(new DateOnly(2023, 1, 21), EPackageSize.S, EProvider.MR);
            var carrierService = new CarrierService();
            var expectedShipmentPrice = ProviderPriceData.priceList.Find(p => p.PackageSize == EPackageSize.S && p.Provider == EProvider.MR).Price;

            // Act
            carrierService.GetCarrierPrice(shipment);
            var actualShipmentPrice = shipment.ShipmentPrice;


            // Assert
            Assert.AreEqual(expectedShipmentPrice, actualShipmentPrice);
        }

        [TestMethod]
        public void GetCarrierPrice_ChecksProviderPriceData_SetsShipmentPriceForMediunPackageAndLpProvider()
        {
            // Arange
            var shipment = new Shipment(new DateOnly(2023, 2, 18), EPackageSize.M, EProvider.LP);
            var carrierService = new CarrierService();
            var expectedShipmentPrice = ProviderPriceData.priceList.Find(p => p.PackageSize == EPackageSize.M && p.Provider == EProvider.LP).Price;

            // Act
            carrierService.GetCarrierPrice(shipment);
            var actualShipmentPrice = shipment.ShipmentPrice;


            // Assert
            Assert.AreEqual(expectedShipmentPrice, actualShipmentPrice);
        }
    }
}