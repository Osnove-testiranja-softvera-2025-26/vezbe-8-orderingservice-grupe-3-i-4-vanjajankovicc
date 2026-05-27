using System;


namespace OTS2023_V9.Models
{
    public class Coupon
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public double Amount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double MinimalRequiredOrderTotal { get; set; }
        public bool Used { get; set; }
    }
}
