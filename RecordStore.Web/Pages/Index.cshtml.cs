using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecordStore.Core.Models;
using RecordStore.Data.Services;

namespace RecordStore.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly RecordService _recordService;
        private readonly CartService _cartService;

        public IndexModel(RecordService recordService, CartService cartService)
        {
            _recordService = recordService;
            _cartService = cartService;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public List<Record> Records { get; set; }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToPage("/Login");
            }

            Records = _recordService.GetRecords(SearchTerm);
            return Page();
        }

        public IActionResult OnPostAddToCart(int recordId)
        {
            _cartService.AddToCart(recordId);
            return RedirectToPage();
        }
    }
}
