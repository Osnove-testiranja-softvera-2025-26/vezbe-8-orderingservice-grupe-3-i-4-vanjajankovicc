using System;

namespace OTS2023_V9.Models
{

    public enum MembershipTier
    {
        Standard,
        Gold,
        Premium
    }

    public class Customer
    {
        public Guid Id { get; set; }
        public MembershipTier MembershipTier {get; set;}
        public string FullName { get; set; }
        public string Telephone { get; set; }

    }
}
