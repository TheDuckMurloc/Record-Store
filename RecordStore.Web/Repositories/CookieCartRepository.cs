using Microsoft.AspNetCore.Http;
using RecordStore.Core.Interfaces;
using RecordStore.Core.Models;
using System.Collections.Generic;
using System.Text.Json;

namespace RecordStore.Web.Repositories
{
    public class CookieCartRepository : ICartRepository
    {
        private readonly IHttpContextAccessor _accessor;
        private const string CartCookieKey = "Cart";
        private const int ExpireDays = 7;

        public CookieCartRepository(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public List<CartItem> LoadCart()
        {
            var request = _accessor.HttpContext?.Request;
            var cookie = request?.Cookies[CartCookieKey];
            if (string.IsNullOrEmpty(cookie))
                return new List<CartItem>();

            try
            {
                return JsonSerializer.Deserialize<List<CartItem>>(cookie) ?? new List<CartItem>();
            }
            catch
            {
                return new List<CartItem>();
            }
        }

        public void SaveCart(List<CartItem> cart)
        {
            var response = _accessor.HttpContext?.Response;
            if (response == null) return;

            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(ExpireDays),
                IsEssential = true,
                HttpOnly = false
            };

            var json = JsonSerializer.Serialize(cart);
            response.Cookies.Append(CartCookieKey, json, options);
        }

        public void ClearCart()
        {
            _accessor.HttpContext?.Response.Cookies.Delete(CartCookieKey);
        }
    }
}
