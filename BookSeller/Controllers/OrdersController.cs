using BookSeller.Data;
using BookSeller.Models;
using BookSeller.Data.Cart;
using BookSeller.Data.Service;
using BookSeller.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace BookSeller.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderService _orderService;
        public OrdersController(ShoppingCart shoppingCart, AppDbContext context, IOrderService orderService)
        {
            _shoppingCart = shoppingCart;
            _context = context;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = "";
            var order = await _orderService.GetOrderByUserIdAsync(userId);
            return View(order);
        }

        public IActionResult ShoppingCart()
        {
            var item = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = item;
            var response = new ShoppingCartViewModel()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
            };
            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if(item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder([Bind("Id,UserId,Name,PhoneNumber,Address")] Order order)
        {
            var item = _shoppingCart.GetShoppingCartItems();
            string userId = "";
            string Name = order.Name;
            string PhoneNumber = order.PhoneNumber;
            string Address = order.Address;
            await _orderService.StoreOrderAsync(item, userId, Name, PhoneNumber, Address);
            await _shoppingCart.ClearShoppingCartAsync();
            return View("OrderCompleted");
        }
        public async Task<IActionResult> Checkout()
        {
            var item = _shoppingCart.GetShoppingCartItems();
            ViewBag.ShoppingItem = item;
            return View();
        }
    }
}
