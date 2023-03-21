using ShipmentDiscountCalculationModule.Models;
using ShipmentDiscountCalculationModule.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentDiscountCalculationModule.Data
{
    static public class ProviderPriceData
    {
        static public List<ProviderShippingPrice> priceList = new List<ProviderShippingPrice>()
        {
            new ProviderShippingPrice() { Provider = EProvider.LP, PackageSize = EPackageSize.S, Price = 1.50M },
            new ProviderShippingPrice() { Provider = EProvider.LP, PackageSize = EPackageSize.M, Price = 4.90M },
            new ProviderShippingPrice() { Provider = EProvider.LP, PackageSize = EPackageSize.L, Price = 6.90M },
            new ProviderShippingPrice() { Provider = EProvider.MR, PackageSize = EPackageSize.S, Price = 2.00M },
            new ProviderShippingPrice() { Provider = EProvider.MR, PackageSize = EPackageSize.M, Price = 3.00M },
            new ProviderShippingPrice() { Provider = EProvider.MR, PackageSize = EPackageSize.L, Price = 4.00M }
        };
    }
}
