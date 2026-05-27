using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS2023_V9.Services.Fakes
{
    internal class FakeCouponService : ICouponService

    {
        public Coupon GetCouponByid(Guid id)
            {

            Coupon coupon = new Coupon();
            coupon.Id = Guid.Parse();
            coupon.GetC
                return coupon;
        
        }

        public void UseCoupon(Guid id);
    }
}
