using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecordStore.Core.Models;
using RecordStore.Data.Repositories;
using RecordStore.Web.Services;

namespace RecordStore.Web.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly RecordRepository _recordRepository;
        private readonly CartService _cartService;

        public DetailsModel(RecordRepository recordRepository, CartService cartService)
        {
            _recordRepository = recordRepository;
            _cartService = cartService;
        }

        public Record Record { get; set; }

        public IActionResult OnGet(int id)
        {
            Record = _recordRepository.GetRecordById(id);
            
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