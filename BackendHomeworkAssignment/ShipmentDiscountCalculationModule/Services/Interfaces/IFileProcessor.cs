using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.Services.Interfaces
{
    internal interface IFileProcessor
    {
        void Process(string[] fileLines, ref decimal remainingMonthlyDiscountFund);
    }
}
