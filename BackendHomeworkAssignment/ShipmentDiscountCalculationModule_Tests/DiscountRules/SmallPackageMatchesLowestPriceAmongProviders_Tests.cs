using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShipmentDiscountCalculationModule.Data;
using ShipmentDiscountCalculationModule.DiscountRules;
using ShipmentDiscountCalculationModule.Models;
using ShipmentDiscountCalculationModule.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.DiscountRules_Tests
{
    [TestClass]
    public class SmallPackageMatchesLowestPriceAmongProviders_Tests
    {
        private SmallPackageMatchesLowestPriceAmongProviders _discountRule;
        private decimal _lowestSmallPackagePrice;
        private decimal _priceIncreace = 0.50M;

        [TestInitialize]
        public void TestInitialize()
        {
            _discountRule = new SmallPackageMatchesLowestPriceAmongProviders();
            _lowestSmallPackagePrice = ProviderPriceData.priceList.Where(p => p.PackageSize == EPackageSize.S).Min(p => p.Price);
        }

        [TestMethod]
        public void TryApplyDiscountRule_WhenRemainingDiscountFundIsZero_ShouldNotApplyDiscount()
        {
            // Arrange
            var shipment = new Shipment(new DateOnly(2023, 3, 22), EPackageSize.S, EProvider.MR);
            var remainingDiscountFund = 0M;

            // Act
            _discountRule.TryApplyDiscountRule(shipment, ref remainingDiscountFund);

            // Assert
            Assert.IsNull(shipment.ShipmentDiscount);
        }

        [TestMethod]
        public void TryApplyDiscountRule_WhenPackageSizeIsNotSmallButMedium_ShouldNotApplyDiscount()
        {
            // Arrange
            var shipment = new Shipment(new DateOnly(2023, 3, 12), EPackageSize.M, EProvider.MR);
            var remainingDiscountFund = 10M;

            // Act
            _discountRule.TryApplyDiscountRule(shipment, ref remainingDiscountFund);

            // Assert
            Assert.IsNull(shipment.ShipmentDiscount);
        }

        [TestMethod]
        public void TryApplyDiscountRule_WhenPackageSizeIsNotSmallButLarge_ShouldNotApplyDiscount()
        {
            // Arrange
            var shipment = new Shipment(new DateOnly(2023, 3, 13), EPackageSize.L, EProvider.MR);
            var remainingDiscountFund = 10M;

            // Act
            _discountRule.TryApplyDiscountRule(shipment, ref remainingDiscountFund);

            // Assert
            Assert.IsNull(shipment.ShipmentDiscount);
        }

        [TestMethod]
        public void TryApplyDiscountRule_WhenShipmentPriceIsAlreadyLowest_ShouldNotApplyDiscount()
        {
            // Arrange
            var shipment = new Shipment(new DateOnly(2023, 3, 18), EPackageSize.S, EProvider.MR);
            shipment.ShipmentPrice = _lowestSmallPackagePrice;
            var remainingDiscountFund = 10m;

            // Act
            _discountRule.TryApplyDiscountRule(shipment, ref remainingDiscountFund);

            // Assert
            Assert.IsNull(shipment.ShipmentDiscount);
        }

        [TestMethod]
        public void TryApplyDiscountRule_WhenRemainingDiscountFundIsEnough_ShouldApplyDiscountAndReduceFundAccordingly()
        {
            // Arrange
            var shipment = new Shipment(new DateOnly(2023, 3, 21), EPackageSize.S, EProvider.MR);
            shipment.ShipmentPrice = _lowestSmallPackagePrice + _priceIncreace;
            var remainingDiscountFund = 10M;

            var expectedDiscount = _priceIncreace;
            var expectedRemainingDiscountFund = remainingDiscountFund - expectedDiscount;

            // Act
            _discountRule.TryApplyDiscountRule(shipment, ref remainingDiscountFund);
            var actualDiscount = shipment.ShipmentDiscount;
            var actualRemainingDiscountFund = remainingDiscountFund;

            // Assert
            Assert.AreEqual(expectedDiscount, actualDiscount);
            Assert.AreEqual(expectedRemainingDiscountFund, actualRemainingDiscountFund);
        }

        
        [TestMethod]
        public void TryApplyDiscountRule_WhenRemainingDiscountFundIsNotEnoughButNotZero_ShouldApplyDiscountAndReduceFundToZero()
        {
            // Arrange
            var shipment = new Shipment(new DateOnly(2023, 3, 24), EPackageSize.S, EProvider.MR);
            shipment.ShipmentPrice = _lowestSmallPackagePrice + _priceIncreace;
            var remainingDiscountFund = 0.10M;

            var expectedDiscount = remainingDiscountFund;
            var expectedRemainingDiscountFund = 0;

            // Act
            _discountRule.TryApplyDiscountRule(shipment, ref remainingDiscountFund);
            var actualDiscount = shipment.ShipmentDiscount;
            var actualRemainingDiscountFund = remainingDiscountFund;

            // Assert
            Assert.AreEqual(expectedDiscount, actualDiscount);
            Assert.AreEqual(expectedRemainingDiscountFund, actualRemainingDiscountFund);
        }


    }
}