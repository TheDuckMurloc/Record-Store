using RecordStore.Core.Interfaces;
using RecordStore.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace RecordStore.Data.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IRecordRepository _recordRepository;

        public CartService(ICartRepository cartRepository, IRecordRepository recordRepository)
        {
            _cartRepository = cartRepository;
            _recordRepository = recordRepository;
        }

        public List<CartItem> GetCartItems() => _cartRepository.LoadCart();

        public void AddToCart(int recordId)
        {
            var record = _recordRepository.GetRecordById(recordId);
            if (record == null || record.Stock <= 0)
                return; 

            var cart = _cartRepository.LoadCart();
            var existing = cart.FirstOrDefault(c => c.RecordId == recordId);

            if (existing != null)
            {
                if (existing.Quantity < record.Stock)
                    existing.Quantity++;
               
            }
            else
            {
                cart.Add(new CartItem { RecordId = recordId, Quantity = 1 });
            }

            _cartRepository.SaveCart(cart);
        }


        public void RemoveFromCart(int recordId)
        {
            var cart = _cartRepository.LoadCart();
            var item = cart.FirstOrDefault(c => c.RecordId == recordId);

            if (item != null)
            {
                if (item.Quantity > 1)
                    item.Quantity--;
                else
                    cart.Remove(item);
            }

            _cartRepository.SaveCart(cart);
        }

        public void ClearCart() => _cartRepository.ClearCart();

        public List<(CartItem Item, Record Record)> GetCartWithRecords()
        {
            var cart = _cartRepository.LoadCart();
            var result = new List<(CartItem, Record)>();

            foreach (var item in cart)
            {
                var record = _recordRepository.GetRecordById(item.RecordId);
                if (record != null)
                    result.Add((item, record));
            }

            return result;
        }

        public decimal GetCartTotal()
        {
            var cart = _cartRepository.LoadCart();
            decimal total = 0;

            foreach (var item in cart)
            {
                var record = _recordRepository.GetRecordById(item.RecordId);
                if (record != null)
                    total += record.Price * item.Quantity;
            }

            return total;
        }
    }
}
