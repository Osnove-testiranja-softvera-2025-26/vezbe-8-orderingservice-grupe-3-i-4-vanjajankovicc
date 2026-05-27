using NUnit.Framework;
using OTS2023_V9;
using OTS2023_V9.Services;

namespace OTS2023_V9.Services
{
    [TestFixture]
    internal class ShippingServiceTest
    {
        private ShippingService _shippingService;

        [SetUp]
        public void Setup()
        {
            _shippingService = new ShippingService();
        }

        // Testira međunarodnu dostavu sa popustom
        [TestCase(1000, 50, 25, true, 10, ShippingType.Overnight, 1540.0)]
        [TestCase(1000, 50, 45, true, 12, ShippingType.Standard, 1540.0)]
        public void CalculateShippingCost_InternationalWithDiscount_ReturnsCorrectCost(
            double distance, double total, double weight, bool isInt, int qty, ShippingType type, double expected)
        {
            double actual = _shippingService.CalculateShippingCost(distance, total, weight, isInt, qty, type);
            Assert.AreEqual(expected, actual, 0.001);
        }

        // Testira međunarodnu dostavu bez popusta
        [TestCase(2500, 50, 25, true, 10, ShippingType.Overnight, 5050.0)]
        [TestCase(1000, 50, 55, true, 10, ShippingType.Standard, 2050.0)]
        public void CalculateShippingCost_InternationalWithoutDiscount_ReturnsCorrectCost(
            double distance, double total, double weight, bool isInt, int qty, ShippingType type, double expected)
        {
            double actual = _shippingService.CalculateShippingCost(distance, total, weight, isInt, qty, type);
            Assert.AreEqual(expected, actual, 0.001);
        }

        // Testira domaću dostavu sa popustom na količinu
        [TestCase(100, 120, 60, false, 10, ShippingType.Standard, 4.0)]
        [TestCase(100, 80, 40, false, 10, ShippingType.Standard, 6.0)]
        public void CalculateShippingCost_DomesticWithQuantityDiscount_ReturnsCorrectCost(
            double distance, double total, double weight, bool isInt, int qty, ShippingType type, double expected)
        {
            double actual = _shippingService.CalculateShippingCost(distance, total, weight, isInt, qty, type);
            Assert.AreEqual(expected, actual, 0.001);
        }

        // Testira domaću dostavu bez popusta
        [TestCase(100, 80, 60, false, 10, ShippingType.Standard, 10.0)]
        [TestCase(100, 120, 30, false, 9, ShippingType.Standard, 10.0)]
        public void CalculateShippingCost_DomesticWithoutQuantityDiscount_ReturnsCorrectCost(
            double distance, double total, double weight, bool isInt, int qty, ShippingType type, double expected)
        {
            double actual = _shippingService.CalculateShippingCost(distance, total, weight, isInt, qty, type);
            Assert.AreEqual(expected, actual, 0.001);
        }
    }
}