using BookSeller.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BookSeller.Data.Service
{
	public interface IOrderService
	{
		Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string name, string phoneNumber, string address);
	
		Task<List<Order>> GetOrderByUserIdAsync(string userId);

        Task<List<Order>> GetOrderByIdAsync(int Id);

    }
}
