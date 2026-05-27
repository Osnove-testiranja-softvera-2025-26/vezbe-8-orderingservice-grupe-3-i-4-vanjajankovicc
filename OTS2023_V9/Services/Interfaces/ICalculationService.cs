using OTS2023_V9.Models;
using System;


namespace OTS2023_V9.Services
{
    public interface ICalculationService
    {
        double CalculateUserDiscount(Guid userId);
        bool CheckCouponValidity(Guid orderId, Guid couponId);
        void ApplyCoupon(Guid orderId, Coupon coupon);
    }
}
