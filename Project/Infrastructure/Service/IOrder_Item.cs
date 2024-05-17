using Project.Model;

namespace Project.Infrastructure.Service
{
    public interface IOrder_Item
    {
        Task<IEnumerable<Order_Item>> GetOrderedItems(Guid orderId,int userId);
        void AddOrderBillsItem(int userId, Guid order_Id);

        IEnumerable<Order_Item> GetAllOrders();


        IEnumerable<Order_Item>  GetMyOrders(int userId);
    }
}

