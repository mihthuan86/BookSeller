using BookSeller.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSeller.Data.Service
{
	public class OrderService : IOrderService
	{
		private readonly AppDbContext _context;
		public OrderService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<List<Order>> GetOrderByIdAsync(int Id)
		{
            var order = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Book).Where(
                n => n.Id == Id).ToListAsync();
            return order;
        }

		public async Task<List<Order>> GetOrderByUserIdAsync(string userId)
		{
			var order = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Book).Where(
				n => n.UserId == userId).ToListAsync();
			return order;
		}

		public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string name, string phoneNumber, string address)
		{
			var order = new Order()
			{
				UserId = userId,
				Name = name,
				PhoneNumber = phoneNumber,
				Address = address,
				Status = 0,
				OrderDate = DateTime.Now,
			};
			await _context.Orders.AddAsync(order);
			await _context.SaveChangesAsync();

			foreach(var item in items)
			{
				var orderItem = new OrderItem()
				{
					Amount = item.Amount,
					BookId = item.Book.Id,
					OrderId = order.Id,
					Price = item.Book.Price
				};
				await _context.OrderItems.AddAsync(orderItem);
			}

			await _context.SaveChangesAsync();
		}
	}
}
