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
        public List<DisplayInventoryDTO?> InventoryDTOs { get; set; } 


        public async Task OnGet()
        {
            InventoryDTOs = (await _inventoryService.GetAllInventory()).Where(i => i.Is_Inventory_Status==true).ToList() ?? null;
        }
    }
}
