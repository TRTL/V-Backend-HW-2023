using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShipmentDiscountCalculationModule.DiscountRules;
using ShipmentDiscountCalculationModule.Models.Enums;
using ShipmentDiscountCalculationModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.DiscountRules_Tests
{
    [TestClass]
    public class ThirdLargeLaPostePackageIsFreeOncePerMonth_Tests
    {
        private ThirdLargeLaPostePackageIsFreeOncePerMonth _discountRule;

        [TestInitialize()]
        public void TestInitialize()
        {
            _discountRule = new ThirdLargeLaPostePackageIsFreeOncePerMonth();
        }

        [TestMethod]
        public void TryApplyDiscountRule_NotLargePackage_ShouldNotApplyDiscount()
        {
            // Arrange
            var shipment = new Shipment(new DateOnly(2023, 2, 14), EPackageSize.S, EProvider.LP);
            var remainingMonthlyDiscountFund = 10M;

            var expectedRemainingMonthlyDiscountFund = remainingMonthlyDiscountFund;

            // Act
            _discountRule.TryApplyDiscountRule(shipment, ref remainingMonthlyDiscountFund);
            var actualRemainingMonthlyDiscountFund = remainingMonthlyDiscountFund;

            // Assert
            Assert.IsNull(shipment.ShipmentDiscount);
            Assert.AreEqual(expectedRemainingMonthlyDiscountFund, actualRemainingMonthlyDiscountFund);
        }

        [TestMethod]
        public void TryApplyDiscountRule_NotLaPosteProvider_ShouldNotApplyDiscount()
        {
            // Arrange
            var shipment = new Shipment(new DateOnly(2023, 2, 19), EPackageSize.L, EProvider.MR);
            var remainingMonthlyDiscountFund = 10M;

            var expectedRemainingMonthlyDiscountFund = remainingMonthlyDiscountFund;

            // Act
            _discountRule.TryApplyDiscountRule(shipment, ref remainingMonthlyDiscountFund);
            var actualRemainingMonthlyDiscountFund = remainingMonthlyDiscountFund;

            // Assert
            Assert.IsNull(shipment.ShipmentDiscount);
            Assert.AreEqual(expectedRemainingMonthlyDiscountFund, actualRemainingMonthlyDiscountFund);
        }

        [TestMethod]
        public void TryApplyDiscountRule_RemainingMonthlyDiscountFundIsZero_ShouldNotAppleDiscount()
        {
            // Arrange
            var shipment1 = new Shipment(new DateOnly(2023, 1, 11), EPackageSize.L, EProvider.LP);
            var shipment2 = new Shipment(new DateOnly(2023, 1, 17), EPackageSize.L, EProvider.LP);
            var shipment3 = new Shipment(new DateOnly(2023, 1, 23), EPackageSize.L, EProvider.LP);
            shipment3.ShipmentPrice = 6M;
            var remainingMonthlyDiscountFund = 0M;
            var expectedRemainingMonthlyDiscountFund = remainingMonthlyDiscountFund;

            // Act
            _discountRule.TryApplyDiscountRule(shipment1, ref remainingMonthlyDiscountFund);
            _discountRule.TryApplyDiscountRule(shipment2, ref remainingMonthlyDiscountFund);
            _discountRule.TryApplyDiscountRule(shipment3, ref remainingMonthlyDiscountFund);
            var actualRemainingMonthlyDiscountFund = remainingMonthlyDiscountFund;

            // Assert
            Assert.IsNull(shipment3.ShipmentDiscount);
            Assert.AreEqual(expectedRemainingMonthlyDiscountFund, actualRemainingMonthlyDiscountFund);
        }

        [TestMethod]
        public void TryApplyDiscountRule_NotThirdLargeLaPostePackageThisMonth_ShouldNotAppleDiscount()
        {
            // Arrange
            var shipment1 = new Shipment(new DateOnly(2023, 1, 29), EPackageSize.L, EProvider.LP);
            var shipment2 = new Shipment(new DateOnly(2023, 2, 11), EPackageSize.L, EProvider.LP);
            var shipment3 = new Shipment(new DateOnly(2023, 2, 27), EPackageSize.L, EProvider.LP);
            var remainingMonthlyDiscountFund = 10M;

            var expectedRemainingMonthlyDiscountFund = remainingMonthlyDiscountFund;

            // Act
            _discountRule.TryApplyDiscountRule(shipment1, ref remainingMonthlyDiscountFund);
            _discountRule.TryApplyDiscountRule(shipment2, ref remainingMonthlyDiscountFund);
            _discountRule.TryApplyDiscountRule(shipment3, ref remainingMonthlyDiscountFund);
            var actualRemainingMonthlyDiscountFund = remainingMonthlyDiscountFund;

            // Assert
            Assert.IsNull(shipment1.ShipmentDiscount);
            Assert.IsNull(shipment2.ShipmentDiscount);
            Assert.IsNull(shipment3.ShipmentDiscount);
            Assert.AreEqual(expectedRemainingMonthlyDiscountFund, actualRemainingMonthlyDiscountFund);
        }

        [TestMethod]
        public void TryApplyDiscountRule_AlreadyUsedOneFreeLargeLaPostePackageThisMonth_ShouldNotAppleDiscount()
        {
            // Arrange
            var shipment1 = new Shipment(new DateOnly(2023, 1, 11), EPackageSize.L, EProvider.LP);
            var shipment2 = new Shipment(new DateOnly(2023, 1, 17), EPackageSize.L, EProvider.LP);
            var shipment3 = new Shipment(new DateOnly(2023, 1, 23), EPackageSize.L, EProvider.LP);
            shipment3.ShipmentPrice = 6M;
            var shipment4 = new Shipment(new DateOnly(2023, 1, 26), EPackageSize.L, EProvider.LP);
            var remainingMonthlyDiscountFund = 10M;

            var expectedRemainingMonthlyDiscountFund = remainingMonthlyDiscountFund - shipment3.ShipmentPrice;

            // Act
            _discountRule.TryApplyDiscountRule(shipment1, ref remainingMonthlyDiscountFund);
            _discountRule.TryApplyDiscountRule(shipment2, ref remainingMonthlyDiscountFund);
            _discountRule.TryApplyDiscountRule(shipment3, ref remainingMonthlyDiscountFund);
            _discountRule.TryApplyDiscountRule(shipment4, ref remainingMonthlyDiscountFund);
            var actualRemainingMonthlyDiscountFund = remainingMonthlyDiscountFund;

            // Assert
            Assert.IsNull(shipment4.ShipmentDiscount);
            Assert.AreEqual(expectedRemainingMonthlyDiscountFund, actualRemainingMonthlyDiscountFund);
        }

        [TestMethod]
        public void TryAppleDiscountRule_ThirdLargeLaPostePackageThisMonthAndFundIsEnough_ShouldAppleDiscountAndReduceFundAccordingly()
        {
            // Arrange 
            var shipment1 = new Shipment(new DateOnly(2023, 3, 12), EPackageSize.L, EProvider.LP);
            var shipment2 = new Shipment(new DateOnly(2023, 3, 14), EPackageSize.L, EProvider.LP);
            var shipment3 = new Shipment(new DateOnly(2023, 3, 20), EPackageSize.L, EProvider.LP);
            shipment3.ShipmentPrice = 6M;
            var remainingMonthlyDiscountFund = 10M;

            var expectedDiscount = shipment3.ShipmentPrice;
            var expectedRemainingMonthlyDiscountFund = remainingMonthlyDiscountFund - shipment3.ShipmentPrice;

            // Act
            _discountRule.TryApplyDiscountRule(shipment1, ref remainingMonthlyDiscountFund);
            _discountRule.TryApplyDiscountRule(shipment2, ref remainingMonthlyDiscountFund);
            _discountRule.TryApplyDiscountRule(shipment3, ref remainingMonthlyDiscountFund);
            var actualDiscount = shipment3.ShipmentDiscount;
            var actualRemainingDiscountFund = remainingMonthlyDiscountFund;

            // Assert
            Assert.AreEqual(expectedDiscount, actualDiscount);
            Assert.AreEqual(expectedRemainingMonthlyDiscountFund, actualRemainingDiscountFund);
        }

        [TestMethod]
        public void TryAppleDiscountRule_ThirdLargeLaPostePackageThisMonthAndFundIsNotEnoughButNotZero_ShouldAppleDiscountAndReduceFundToZero()
        {
            // Arrange 
            var shipment1 = new Shipment(new DateOnly(2023, 1, 22), EPackageSize.L, EProvider.LP);
            var shipment2 = new Shipment(new DateOnly(2023, 1, 24), EPackageSize.L, EProvider.LP);
            var shipment3 = new Shipment(new DateOnly(2023, 1, 28), EPackageSize.L, EProvider.LP);
            shipment3.ShipmentPrice = 6M;
            var remainingMonthlyDiscountFund = 4M;

            var expectedDiscount = remainingMonthlyDiscountFund;
            var expectedRemainingMonthlyDiscountFund = 0M;

            // Act
            _discountRule.TryApplyDiscountRule(shipment1, ref remainingMonthlyDiscountFund);
            _discountRule.TryApplyDiscountRule(shipment2, ref remainingMonthlyDiscountFund);
            _discountRule.TryApplyDiscountRule(shipment3, ref remainingMonthlyDiscountFund);
            var actualDiscount = shipment3.ShipmentDiscount;
            var actualRemainingDiscountFund = remainingMonthlyDiscountFund;

            // Assert
            Assert.AreEqual(expectedDiscount, actualDiscount);
            Assert.AreEqual(expectedRemainingMonthlyDiscountFund, actualRemainingDiscountFund);
        }

    }
}