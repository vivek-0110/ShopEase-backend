using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Service;
using Project.Model;

namespace Project.Infrastructure.Implementation
{
    public class CartRepository : ICart
    {
        private readonly ProjDbContext context;
        public CartRepository(ProjDbContext context)
        {
            this.context = context;
        }
        public List<Cart> GetAllCartItems(int userId)
        {
            return context.Carts.Where(t => t.UserId == userId).ToList();
        }
        public async Task<Cart> AddToCart(int productId, int userId)
        {

            var productExist = await context.Carts.FirstOrDefaultAsync(t => t.ProductId == productId && t.UserId == userId);
            if (productExist == null)
            {
                productExist = new Cart
                {
                    ProductId = productId,
                    UserId = userId,
                    Quantity =  1,
                    UnitPrice = context.Products.SingleOrDefault(t => t.ProductId == productId).Price

                };
                await context.Carts.AddAsync(productExist);
            }
            else
            {
                productExist.Quantity++;
            }
            await context.SaveChangesAsync();
            return productExist;
        }

        public async Task<Cart> RemoveItem(int productId, int userId)
        {
            var result = context.Carts.FirstOrDefault(t => t.ProductId == productId && t.UserId == userId);
            if (result != null)
            {
                context.Carts.Remove(result);
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }


 /*       public async void UpdateCartItemDB(int userId, ShoppingCartUpdate[] CartItemUpdates)
        {
            int CartItemCount = CartItemUpdates.Count();
            var myCart = GetAllCartItems(userId);
            foreach (var cartItem in myCart)
            {
                // Iterate through all rows within shopping cart list
                for (int i = 0; i < CartItemCount; i++)
                {
                    if (cartItem.ProductId == CartItemUpdates[i].ProductId)
                    {
                        if (CartItemUpdates[i].PurchaseQuantity < 1 || CartItemUpdates[i].RemoveItem == true)
                        {
                            await RemoveItem(cartItem.ProductId, userId);
                            Console.WriteLine("Removed sucessfully");
                        }
                        else
                        {
                            UpdateItem(userId, cartItem.ProductId, CartItemUpdates[i].PurchaseQuantity);
                        }
                    }
                }
            }

        }*/

        public int GetToTalPrice(int userId)
        {
            int sum = 0;
            var items = GetAllCartItems(userId);
            foreach (var item in items)
            {
                sum += item.UnitPrice * item.Quantity;
            }
            return sum;
        }

        public void UpdateItem(int userId, int productId, int quantity)
        {
            try
            {
                var myItem = context.Carts.FirstOrDefault(t => t.UserId == userId && t.ProductId == productId);
                if (myItem != null)
                {
                    myItem.Quantity = quantity;
                    context.SaveChanges();
                }
            }
            catch (Exception exp)
            {
                throw new Exception("ERROR: Unable to Update Cart Item - " + exp.Message.ToString(), exp);
            }
        }

        public void EmptyCart(int userId)
        {
            var cartItems = context.Carts.Where(
          c => c.UserId == userId);
            foreach (var cartItem in cartItems)
            {
                context.Carts.Remove(cartItem);
            }
            // Save changes.             
            context.SaveChanges();
        }

        public int GetCount(int userId)
        {
            var items = context.Carts.Where(c => c.UserId == userId);
            int count = 0;
            foreach (var item in items)
            {
                count = count + item.Quantity;
            }
            // Return 0 if all entries are null         
            return count;
        }

       

   /* public class ShoppingCartUpdate
    {
        public int ProductId { get; set; }
        public int PurchaseQuantity { get; set; }
        public bool RemoveItem { get; set; }
    }*/
}
    }



