using System;


namespace OTS2023_V9.Models
{

    public enum Status
    {
        Cancelled,
        Delivered,
        New,
        Rejected
    }

    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderCreatedDate { get; set; }
        public DateTime OrderDeadlineDate { get; set; }
        public double Total { get; set; }
        public Status Status {get; set;}

    }
}
