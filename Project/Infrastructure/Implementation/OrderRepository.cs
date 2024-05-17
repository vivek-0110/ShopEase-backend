using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Service;
using Project.Model;
using System.Diagnostics.Contracts;

namespace Project.Infrastructure.Implementation
{
    public class OrderRepository : IOrder
    {
        private readonly ProjDbContext context;
        private readonly ICart cart;
        public OrderRepository(ProjDbContext context, ICart cart)
        {
            this.context = context;
            this.cart = cart;
        }

        public int GetTotalAmount(int userId)
        {
            return cart.GetToTalPrice(userId);
        }

        public int GetNetAmount(int userId)
        {
            var total = context.Orders.FirstOrDefault(t => t.UserId == userId).NetAmount;
            return total;
        }
        
      public async Task<Order> BuyNow(int userId)

        {

            var order = new Order

            {

                UserId = userId,

                TotalAmount = GetTotalAmount(userId),

                ShippingCharge = 50,

                NetAmount = GetTotalAmount(userId) + 50

            };

            var result=await context.Orders.AddAsync(order);

            context.SaveChanges();
            return result.Entity;

        }
    }
    }

