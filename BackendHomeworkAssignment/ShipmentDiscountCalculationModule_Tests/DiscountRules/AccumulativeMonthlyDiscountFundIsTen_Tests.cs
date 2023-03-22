using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShipmentDiscountCalculationModule.DiscountRules;
using ShipmentDiscountCalculationModule.Models;
using ShipmentDiscountCalculationModule.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShipmentDiscountCalculationModule.DiscountRules_Tests
{
    [TestClass]
    public class AccumulativeMonthlyDiscountFundIsTen_Tests
    {
        private AccumulativeMonthlyDiscountFundIsTen _discountRule;

        [TestInitialize()]
        public void TestInitialize()
        {
            _discountRule = new AccumulativeMonthlyDiscountFundIsTen();
        }

        [TestMethod]
        public void TryApplyDiscountRule_NewMonth_ResetsRemainingMonthlyDiscountFundToDefault()
        {
            // Arrange
            var shipment1 = new Shipment(new DateOnly(2023, 2, 25), EPackageSize.S, EProvider.LP);
            var shipment2 = new Shipment(new DateOnly(2023, 3, 11), EPackageSize.M, EProvider.MR);
            var remainingMonthlyDiscountFund = 5.10M;
            var expectedRemainingMonthlyDiscountFund = 10M;

            // Act
            _discountRule.TryApplyDiscountRule(shipment1, ref remainingMonthlyDiscountFund);
            _discountRule.TryApplyDiscountRule(shipment2, ref remainingMonthlyDiscountFund);
            var actualRemainingMonthlyDiscountFund = remainingMonthlyDiscountFund;

            // Assert
            Assert.AreEqual(expectedRemainingMonthlyDiscountFund, actualRemainingMonthlyDiscountFund);
        }

        [TestMethod]
        public void TryApplyDiscountRule_SameMonth_DoesntResetRemainingMonthlyDiscountFundToDefault()
        {
            // Arrange
            var shipment1 = new Shipment(new DateOnly(2023, 3, 22), EPackageSize.L, EProvider.MR);
            var shipment2 = new Shipment(new DateOnly(2023, 3, 23), EPackageSize.S, EProvider.MR);
            var remainingMonthlyDiscountFund = 9.50M;
            var expectedRemainingMonthlyDiscountFund = remainingMonthlyDiscountFund;

            // Act
            _discountRule.TryApplyDiscountRule(shipment1, ref remainingMonthlyDiscountFund);
            _discountRule.TryApplyDiscountRule(shipment2, ref remainingMonthlyDiscountFund);
            var actualRemainingMonthlyDiscountFund = remainingMonthlyDiscountFund;

            // Assert
            Assert.AreEqual(expectedRemainingMonthlyDiscountFund, actualRemainingMonthlyDiscountFund);
        }

        [TestMethod]
        public void TryApplyDiscountRule_SameMonthButDifferentYear_ResetsRemainingMonthlyDiscountFundToDefault()
        {
            // Arrange
            var shipment1 = new Shipment(new DateOnly(2022, 3, 12), EPackageSize.L, EProvider.MR);
            var shipment2 = new Shipment(new DateOnly(2023, 3, 15), EPackageSize.S, EProvider.MR);
            var remainingMonthlyDiscountFund = 9.50M;
            var expectedRemainingMonthlyDiscountFund = 10M;

            // Act
            _discountRule.TryApplyDiscountRule(shipment1, ref remainingMonthlyDiscountFund);
            _discountRule.TryApplyDiscountRule(shipment2, ref remainingMonthlyDiscountFund);
            var actualRemainingMonthlyDiscountFund = remainingMonthlyDiscountFund;

            // Assert
            Assert.AreEqual(expectedRemainingMonthlyDiscountFund, actualRemainingMonthlyDiscountFund);
        }
    }
}