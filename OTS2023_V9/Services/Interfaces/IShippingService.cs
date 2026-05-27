

namespace OTS2023_V9.Services
{
    public interface IShippingService
    {
        double CalculateShippingCost(double distanceInMiles, double totalCost, double weight, bool isInternational, int productQuantity, ShippingType shippingType);

    }
}
