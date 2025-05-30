using Microsoft.AspNetCore.Http;
using RecordStore.Core.Models;
using System.Text.Json;

namespace RecordStore.Web.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RecordStore.Data.Repositories.RecordRepository _recordRepository;
        private const string CartSessionKey = "Cart";

        public CartService(IHttpContextAccessor httpContextAccessor, RecordStore.Data.Repositories.RecordRepository recordRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _recordRepository = recordRepository;
        }

        public List<CartItem> GetCartItems()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            string cartJson = session.GetString(CartSessionKey);
            
            if (string.IsNullOrEmpty(cartJson))
                return new List<CartItem>();

            return JsonSerializer.Deserialize<List<CartItem>>(cartJson);
        }

        public void AddToCart(int recordId)
        {
            var cart = GetCartItems();
            var existingItem = cart.FirstOrDefault(item => item.RecordId == recordId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem { RecordId = recordId, Quantity = 1 });
            }

            SaveCart(cart);
        }

        public void RemoveFromCart(int recordId)
        {
            var cart = GetCartItems();
            var item = cart.FirstOrDefault(item => item.RecordId == recordId);

            if (item != null)
            {
                if (item.Quantity > 1)
                    item.Quantity--;
                else
                    cart.Remove(item);
            }

            SaveCart(cart);
        }

        public void ClearCart()
        {
            _httpContextAccessor.HttpContext.Session.Remove(CartSessionKey);
        }

        private void SaveCart(List<CartItem> cart)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            string cartJson = JsonSerializer.Serialize(cart);
            session.SetString(CartSessionKey, cartJson);
        }

        public decimal GetCartTotal()
        {
            var cart = GetCartItems();
            decimal total = 0;

            foreach (var item in cart)
            {
                var record = _recordRepository.GetRecordById(item.RecordId);
                if (record != null)
                {
                    total += record.Price * item.Quantity;
                }
            }

            return total;
        }

        public List<(CartItem Item, Record Record)> GetCartWithRecords()
        {
            var cart = GetCartItems();
            var cartWithRecords = new List<(CartItem Item, Record Record)>();

            foreach (var item in cart)
            {
                var record = _recordRepository.GetRecordById(item.RecordId);
                if (record != null)
                {
                    cartWithRecords.Add((item, record));
                }
            }

            return cartWithRecords;
        }
    }
}
