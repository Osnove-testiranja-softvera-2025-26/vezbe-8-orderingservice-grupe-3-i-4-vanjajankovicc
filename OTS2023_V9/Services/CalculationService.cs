using OTS2023_V9.Models;
using OTS2023_V9.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace OTS2023_V9.Services
{
    public class CalculationService : ICalculationService
    {
        private IOrderService orderService;
        private ICouponService couponService;
        private ILoggerService loggerService;
        public bool CheckCouponValidity(Guid orderId, Guid couponId)
        {
            Order order = orderService.GetOrderById(orderId);
            Coupon coupon = couponService.GetCouponById(couponId);
            return (!coupon.Used && coupon.ExpirationDate.CompareTo(DateTime.Now) > 0 && order.Total >= coupon.MinimalRequiredOrderTotal);
        }

        public double CalculateUserDiscount(Guid userId)
        {
            DateTime monthBefore = DateTime.Now.AddMonths(-1);
            List<Order> ordersInTheLastMonth = orderService.GetUserOrdersWithDeadlineBetween(userId, monthBefore, DateTime.Now);
            List<Order> deliveredOrders = ordersInTheLastMonth.FindAll(x => x.Status.Equals(Status.Delivered));
            if(deliveredOrders.Count > 5)
            {
                return 0.1;

            }else if(deliveredOrders.Count > 3)
            {
                return 0.05;
            }
            return 0;
        }

        public void ApplyCoupon(Guid orderId, Coupon coupon)
        {
            try
            {
                couponService.UseCoupon(coupon.Id);
                orderService.UpdateTotal(-coupon.Amount);
            }
            catch(InvalidCouponException  ex)
            {
                loggerService.LogError("[CouponInvalidException] " + ex.Message);
            }
        }


    }
}
