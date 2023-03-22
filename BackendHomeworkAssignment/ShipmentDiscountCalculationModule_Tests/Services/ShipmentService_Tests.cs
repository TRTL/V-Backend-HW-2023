using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShipmentDiscountCalculationModule.Models;
using ShipmentDiscountCalculationModule.Models.Enums;
using ShipmentDiscountCalculationModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.Services_Tests
{
    [TestClass]
    public class ShipmentService_Tests
    {
        [TestMethod]
        public void ApplyDiscountToPrice_GetsShipmentWithPriceAndDiscount_CalculatesPriceAfterDicount()
        {
            // Arrange
            var shipment = new Shipment(new DateOnly(2023, 1, 21), EPackageSize.S, EProvider.MR);
            shipment.ShipmentPrice = 5.0M;
            shipment.ShipmentDiscount = 1.5M;
            var shipmentService = new ShipmentService();

            var expectedShipmentPrice = shipment.ShipmentPrice - shipment.ShipmentDiscount;

            // Act
            shipmentService.ApplyDiscountToPrice(shipment);
            var actualShipmentPrice = shipment.ShipmentPrice;

            // Assert
            Assert.AreEqual(expectedShipmentPrice, actualShipmentPrice);
        }
    }
}