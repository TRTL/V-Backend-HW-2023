using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShipmentDiscountCalculationModule.Models;
using ShipmentDiscountCalculationModule.Models.Enums;
using ShipmentDiscountCalculationModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShipmentDiscountCalculationModule.Services_Tests
{
    [TestClass]
    public class Adapter_Tests
    {
        [TestMethod]
        public void Bind_ReceivesCorrectString_ReturntNewShipmentObject()
        {
            // Arrange
            var adapter = new Adapter();
            var inputLine = "2023-03-16 S MR";

            var expectedDate = new DateOnly(2023, 3, 16);
            var expectedPackageSizeCode = EPackageSize.S;
            var expectedCarrierCode = EProvider.MR;

            // Act
            var shipment = adapter.Bind(inputLine);

            // Assert
            Assert.AreEqual(expectedDate, shipment.Date);
            Assert.AreEqual(expectedPackageSizeCode, shipment.PackageSizeCode);
            Assert.AreEqual(expectedCarrierCode, shipment.CarrierCode);
            Assert.IsNull(shipment.ShipmentPrice);
            Assert.IsNull(shipment.ShipmentDiscount);
        }
    }
}