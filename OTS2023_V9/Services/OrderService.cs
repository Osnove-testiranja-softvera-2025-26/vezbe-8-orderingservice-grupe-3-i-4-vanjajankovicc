

using OTS2023_V9.Models;
using OTS2023_V9.Services;
using System;
using System.Collections.Generic;

namespace OTS2023_V9
{
    public class OrderService : IOrderService
    {
        public Order GetOrderById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetUserOrdersWithDeadlineBetween(Guid userId, DateTime monthBefore, DateTime now)
        {
            throw new NotImplementedException();
        }

        public void UpdateTotal(double difference)
        {
            throw new NotImplementedException();
        }
    }
}
