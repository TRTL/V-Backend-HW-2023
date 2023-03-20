using ShipmentDiscountCalculationModule.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.Services
{
    internal class FileReader : IFileReader
    {
        readonly string _filePath;

        public FileReader(string filePath)
        {
            _filePath = Environment.CurrentDirectory + filePath;
        }

        public string[] ReadFileLines()
        {
            return File.ReadAllLines(_filePath);
        }
    }
}
