using System;


namespace OTS2023_V9
{
    class InvalidCouponException : Exception
    {
        public InvalidCouponException (string message): base(message)
        {

        }

        public InvalidCouponException ()
        {

        }
    }
}
