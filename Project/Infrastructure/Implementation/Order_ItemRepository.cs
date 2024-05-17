using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Service;
using Project.Model;
using System.Diagnostics.Contracts;

namespace Project.Infrastructure.Implementation
{
    public class Order_ItemRepository : IOrder_Item
    {
        private readonly ProjDbContext context;
        public Order_ItemRepository(ProjDbContext context)
        {
            this.context = context;
        }
        public void AddOrderBillsItem(int userId, Guid orderId)
        {
            var cartItems = context.Carts.Where(t => t.UserId == userId).ToList();
            foreach (var cartItem in cartItems)
            {
                var product = context.Products.FirstOrDefault(t => t.ProductId == cartItem.ProductId);
                if (product != null)
                {
                    var orderItem = new Order_Item
                    {
                        ProductId = cartItem.ProductId,
                        OrderId = orderId,
                        ProductName = context.Products.First(t => t.ProductId == cartItem.ProductId).ProductName,
                        UserId = userId,
                        Quantity = cartItem.Quantity,
                        Price = cartItem.UnitPrice,
                        TotalAmount = cartItem.Quantity * cartItem.UnitPrice,

                    };
                    /*context.Order_Items.Add(orderItem);
                    context.SaveChanges();*/
                    context.Order_Items.Add(orderItem);

                    // Update stock quantity
                    product.Stock -= cartItem.Quantity;
                    context.Products.Update(product);
                }
                context.SaveChanges();


                    

            }
        }
        public async Task<IEnumerable<Order_Item>> GetOrderedItems(Guid orderId, int userId)
        {
            return await context.Order_Items.Where(t => t.UserId == userId && t.OrderId == orderId).ToListAsync();

            
        }
        public  IEnumerable<Order_Item> GetMyOrders(int userId)
        {
            var result =  context.Order_Items.Where(t => t.UserId == userId).ToList();
            return result;
        }

        public IEnumerable<Order_Item> GetAllOrders()
        {
            var result =  context.Order_Items.ToList();
            return result;
        }
    }
}
