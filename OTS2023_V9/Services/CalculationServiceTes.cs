using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using OTS2023_V9.Services;
using OTS2023_V9.Models;
using OTS2023_V9.Services.Interfaces;

namespace OTS2023_V9.Services
{
    [TestFixture]
    internal class CalculationServiceTest
    {
        private Mock<IOrderService> _orderServiceMock;
        private Mock<ICouponService> _couponServiceMock;
        private Mock<ILoggerService> _loggerServiceMock;
        private CalculationService _calculationService;

        [SetUp]
        public void Setup()
        {
            _orderServiceMock = new Mock<IOrderService>();
            _couponServiceMock = new Mock<ICouponService>();
            _loggerServiceMock = new Mock<ILoggerService>();

            // Konstruktor mora da prati tačan redosled koji imaš u CalculationService.cs:
            // (IOrderService, ICouponService, ILoggerService)
            _calculationService = new CalculationService(
                _orderServiceMock.Object,
                _couponServiceMock.Object,
                _loggerServiceMock.Object
            );
        }

        #region Testovi za CheckCouponValidity

        [Test]
        public void CheckCouponValidity_ValidCoupon_ReturnsTrue()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();
            Guid couponId = Guid.NewGuid();

            var fakeOrder = new Order { Id = orderId, Total = 500.0 };
            var fakeCoupon = new Coupon
            {
                Id = couponId,
                Used = false,
                ExpirationDate = DateTime.Now.AddDays(2), // Da bude veće od DateTime.Now
                MinimalRequiredOrderTotal = 300.0        // Uslov ispunjen jer je 500 >= 300
            };

            _orderServiceMock.Setup(s => s.GetOrderById(orderId)).Returns(fakeOrder);
            _couponServiceMock.Setup(s => s.GetCouponById(couponId)).Returns(fakeCoupon);

            // Act
            bool result = _calculationService.CheckCouponValidity(orderId, couponId);

            // Assert
            Assert.IsTrue(result);
        }

        #endregion

        #region Testovi za CalculateUserDiscount

        [TestCase(6, 0.1)]  // Više od 5 porudžbina -> 10% popusta
        [TestCase(4, 0.05)] // Više od 3 porudžbine -> 5% popusta
        [TestCase(2, 0.0)]  // Ostali slučajevi -> 0% popusta
        public void CalculateUserDiscount_BasedOnDeliveredOrders_ReturnsCorrectDiscount(int deliveredCount, double expectedDiscount)
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            List<Order> fakeOrders = new List<Order>();

            // Generišemo onoliko dostavljenih porudžbina koliko test zahteva
            for (int i = 0; i < deliveredCount; i++)
            {
                fakeOrders.Add(new Order { Status = Status.Delivered });
            }

            _orderServiceMock.Setup(s => s.GetUserOrdersWithDeadlineBetween(userId, It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                             .Returns(fakeOrders);

            // Act
            double actualDiscount = _calculationService.CalculateUserDiscount(userId);

            // Assert
            Assert.AreEqual(expectedDiscount, actualDiscount, 0.001);
        }

        #endregion

        #region Testovi za ApplyCoupon

        [Test]
        public void ApplyCoupon_Success_CallsUseCouponAndUpdatesTotal()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();
            var coupon = new Coupon { Id = Guid.NewGuid(), Amount = 50.0 };

            // Podešavamo mock-ove pošto tvoj kod poziva ove dve metode unutar try bloka
            _couponServiceMock.Setup(s => s.UseCoupon(coupon.Id));
            _orderServiceMock.Setup(s => s.UpdateTotal(-coupon.Amount));

            // Act
            _calculationService.ApplyCoupon(orderId, coupon);

            // Assert
            // Proveravamo da li su metode pozvane tačno jednom sa dobrim parametrima
            _couponServiceMock.Verify(s => s.UseCoupon(coupon.Id), Times.Once);
            _orderServiceMock.Verify(s => s.UpdateTotal(-50.0), Times.Once);
        }

        [Test]
        public void ApplyCoupon_InvalidCouponException_LogsErrorMessage()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();
            var coupon = new Coupon { Id = Guid.NewGuid(), Amount = 50.0 };

            // Force-ujemo mock da baci InvalidCouponException kada se pozove UseCoupon
            _couponServiceMock.Setup(s => s.UseCoupon(coupon.Id))
                              .Throws(new InvalidCouponException("Kupon je nevalidan!"));

            // Act
            _calculationService.ApplyCoupon(orderId, coupon);

            // Assert
            // Proveravamo da li je u catch bloku pozvan loggerService sa porukom koja sadrži traženi string
            _loggerServiceMock.Verify(s => s.LogError(It.Is<string>(msg => msg.Contains("[CouponInvalidException]"))), Times.Once);

            // Pošto je bačen izuzetak, UpdateTotal u sledećoj liniji tvog koda se nikad ne izvršava
            _orderServiceMock.Verify(s => s.UpdateTotal(It.IsAny<double>()), Times.Never);
        }

        #endregion
    }
}