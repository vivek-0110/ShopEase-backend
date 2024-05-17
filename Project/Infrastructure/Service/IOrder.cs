using Project.Model;

namespace Project.Infrastructure.Service
{
    public interface IOrder
    {
        int GetTotalAmount(int userId);
        int GetNetAmount(int userId);
        Task<Order> BuyNow(int userId);

        
    }
}
