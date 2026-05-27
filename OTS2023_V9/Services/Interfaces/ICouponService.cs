using OTS2023_V9.Models;
using System;


namespace OTS2023_V9.Services.Interfaces
{
    public interface ICouponService
    {
        Coupon GetCouponById(Guid id);
        void UseCoupon(Guid id);
    }
}
