using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShipmentDiscountCalculationModule.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.Services_Tests
{
    [TestClass()]
    public class TransactionValidator_Tests
    {
        private TransactionValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new TransactionValidator();
        }

        [TestMethod]
        public void Validate_WhenValidInput_ReturnsTrue()
        {
            // Arrange
            var inputLine = "2015-02-05 S LP";

            // Act
            var isValid = _validator.Validate(inputLine);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Validate_WhenInputIsEmpty_ReturnsFalse()
        {
            // Arrange
            var inputLine = " ";

            // Act
            var isValid = _validator.Validate(inputLine);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Validate_WhenInputIsNull_ReturnsFalse()
        {
            // Arrange
            string? inputLine = null;

            // Act
            var isValid = _validator.Validate(inputLine);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Validate_WhenInputMoreThanThreeElements_ReturnsFalse()
        {
            // Arrange
            var inputLine = "2015-12-05 S LP 1";

            // Act
            var isValid = _validator.Validate(inputLine);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Validate_WhenInputLessThanThreeElements_ReturnsFalse()
        {
            // Arrange
            var inputLine = "2015-12-05 S";

            // Act
            var isValid = _validator.Validate(inputLine);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Validate_WhenDateIsIncorrect1_ReturnsFalse()
        {
            // Arrange
            var inputLine = "2015-22-05 S LP";

            // Act
            var isValid = _validator.Validate(inputLine);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Validate_WhenDateIsIncorrect2_ReturnsFalse()
        {
            // Arrange
            var inputLine = "2015-2-0 S LP";

            // Act
            var isValid = _validator.Validate(inputLine);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Validate_WhenDateIsIncorrect3_ReturnsFalse()
        {
            // Arrange
            var inputLine = "1999-1-A S LP";

            // Act
            var isValid = _validator.Validate(inputLine);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Validate_WhenPackageSizeCodeIsIncorrect_ReturnsFalse()
        {
            // Arrange
            var inputLine = "2015-2-10 X LP";

            // Act
            var isValid = _validator.Validate(inputLine);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Validate_WhenCarrierCodeIsIncorrect_ReturnsFalse()
        {
            // Arrange
            var inputLine = "2015-2-10 S LPD";

            // Act
            var isValid = _validator.Validate(inputLine);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}