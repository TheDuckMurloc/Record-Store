using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecordStore.Core.Interfaces;
using RecordStore.Core.Models;
using RecordStore.Core.Services;
using System.Collections.Generic;

namespace RecordStore.Web.Pages
{
    public class AdminPageModel : PageModel
    {
        private readonly IRecordRepository _recordRepository;
        private readonly IAdminRepository _adminService;

        public AdminPageModel(IRecordRepository recordRepository, IAdminRepository adminService)
        {
            _recordRepository = recordRepository;
            _adminService = adminService;
        }

        public List<Record> Records { get; set; } = new();

        [BindProperty]
        public int RecordId { get; set; }

        [BindProperty]
        public int NewStock { get; set; }

        public IActionResult OnGet()
        {
            var roles = HttpContext.Session.GetString("Role");
            if (roles == null || !roles.Contains("1"))
            {
                return RedirectToPage("/Login");
            }

            Records = new List<Record>(_recordRepository.GetAll());

            return Page();
        }

        public IActionResult OnPostUpdateStock()
        {
            if (NewStock < 0)
            {
                ModelState.AddModelError(string.Empty, "Stock cannot be negative.");
                Records = new List<Record>(_recordRepository.GetAll());
                return Page();
            }

            _adminService.UpdateStock(RecordId, NewStock);

            TempData["Message"] = "Stock updated successfully!";
            return RedirectToPage();
        }
    }
}
