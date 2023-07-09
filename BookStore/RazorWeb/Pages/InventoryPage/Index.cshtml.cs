using BookStoreAPI.Core.DiplayDTO;
using BookStoreAPI.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.InventoryPage
{
    public class IndexModel : PageModel
    {
        private readonly IInventoryService _inventoryService;

        public IndexModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [BindProperty]
        public List<DisplayInventoryDTO?> InventoryDTOs { get; set; } = new List<DisplayInventoryDTO?>();


        public async void OnGet()
        {
            InventoryDTOs = (await _inventoryService.GetAllInventory()).ToList();
        }
    }
}
