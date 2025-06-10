using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecordStore.Core.Models;
using RecordStore.Core.Interfaces; 
using RecordStore.Data.Services;    

namespace RecordStore.Web.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly RecordService _recordService;
        private readonly CartService _cartService;

        public DetailsModel(RecordService recordService, CartService cartService)
        {
            _recordService = recordService;
            _cartService = cartService;
        }

        public Record Record { get; set; }

        public IActionResult OnGet(int id)
        {
            Record = _recordService.GetRecordById(id);

            if (Record == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _cartService.AddToCart(id);
            return RedirectToPage("/Cart");
        }
    }
}
