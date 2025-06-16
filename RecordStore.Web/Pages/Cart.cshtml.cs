using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecordStore.Core.Models;
using RecordStore.Data.Services;

namespace RecordStore.Web.Pages;

public class CartModel : PageModel
{
    private readonly CartService _cartService;

    public CartModel(CartService cartService)
    {
        _cartService = cartService;
    }

    public List<(CartItem Item, Record Record)> CartItems { get; set; }
    public decimal Total { get; set; }

    [TempData]
    public string Message { get; set; }

    public void OnGet()
    {
        CartItems = _cartService.GetCartWithRecords();
        Total = _cartService.GetCartTotal();
    }

    public IActionResult OnPostRemove(int recordId)
    {
        _cartService.RemoveFromCart(recordId);
        Message = "Item removed from cart successfully.";
        return RedirectToPage();
    }

    public IActionResult OnPostClear()
    {
        _cartService.ClearCart();
        return RedirectToPage();
    }

    public IActionResult OnPostCheckout()
    {
        return RedirectToPage("/Index");
    }
} 