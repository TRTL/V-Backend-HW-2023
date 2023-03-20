using ShipmentDiscountCalculationModule.Models;
using ShipmentDiscountCalculationModule.Models.Enums;
using ShipmentDiscountCalculationModule.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.Services
{
    public class Validator : IValidator
    {
        public bool Validate(string inputLine)
        {
            if (string.IsNullOrEmpty(inputLine)) return false;

            var inputLineSplit = inputLine.Split(' ');

            if (inputLineSplit.Length != 3) return false;

            var dateFromString = inputLineSplit[0];
            if (!DateOnly.TryParse(dateFromString, out _)) return false;

            var packageSizeCodeFromString = inputLineSplit[1];
            if (!Enum.TryParse(typeof(EPackageSize), packageSizeCodeFromString, out _)) return false;

            var carrierCodeFromString = inputLineSplit[2];
            if (!Enum.TryParse(typeof(EProvider), carrierCodeFromString, out _)) return false;

            return true;
        }
    }
}
