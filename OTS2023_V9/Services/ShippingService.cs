using OTS2023_V9.Services;

namespace OTS2023_V9
{

    public enum ShippingType
    {
        Standard,
        Express,
        Overnight
    }

    public class ShippingService: IShippingService
    {
        public double CalculateShippingCost(double distanceInMiles, double totalCost, double weight, bool isInternational, int productQuantity, ShippingType shippingType)
        {

            double shippingCost;

            if (isInternational && totalCost <= 100)
            {
                if (productQuantity >= 10 && ((!shippingType.Equals(ShippingType.Overnight) && weight <=50) || (shippingType.Equals(ShippingType.Overnight) && weight <= 30)) && distanceInMiles < 2000)
                {
                   
                    shippingCost = 40.0 + (distanceInMiles * 1.5);
                  
                }
                else
                {
                    shippingCost = 50.0 + (distanceInMiles * 2);
                }
            }
            else 
            {
                double baseCost = 10.0;
                double quantityDiscount = 0.0;

                if (productQuantity >= 10 && shippingType.Equals(ShippingType.Standard) && (totalCost >= 100 || weight < 50))
                {
                    quantityDiscount = totalCost * 0.05;
                }

                shippingCost = baseCost - quantityDiscount;
            }

            return shippingCost;
        }


    }
}
