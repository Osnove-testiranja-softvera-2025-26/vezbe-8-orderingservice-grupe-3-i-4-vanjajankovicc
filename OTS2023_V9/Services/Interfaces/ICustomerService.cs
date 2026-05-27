

using OTS2023_V9.Models;
using System;

namespace OTS2023_V9.Services
{
    public interface ICustomerService
    {
        Customer GetCustomerById(Guid id);
    }
}
