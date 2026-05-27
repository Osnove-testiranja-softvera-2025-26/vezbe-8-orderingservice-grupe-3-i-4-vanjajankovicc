

using OTS2023_V9.Models;
using System;
using System.Collections.Generic;

namespace OTS2023_V9.Services
{
    public interface IOrderService
    {
        Order GetOrderById(Guid id);
        void UpdateTotal(double difference);
        List<Order> GetUserOrdersWithDeadlineBetween(Guid userId, DateTime monthBefore, DateTime now);
    }
}
