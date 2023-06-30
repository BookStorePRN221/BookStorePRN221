using BookStoreAPI.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.IService
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(Order order);
        Task<IEnumerable<Order>> GetAllOrder();
        Task<Book> GetOrderById(Guid orderId);
        Task<bool> UpdateOrder(Order order);
        Task<bool> DeleteOrder(Guid orderId);
        Task<bool> RestoreOrder(Guid orderId);
    }
}
