using BookStoreAPI.Core.DiplayDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.InventoryPage
{
    public class HistoryInventoryModel : PageModel
    {
        private readonly IInventoryService _inventoryService;
        public HistoryInventoryModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [BindProperty]
        public List<DisplayInventoryDTO?> InventoryDTOs { get; set; }

        public async Task OnGet()
        {
            InventoryDTOs = (await _inventoryService.GetAllInventory()).Where(i => i.Is_Inventory_Status == false).ToList();
        }

        public async Task<IActionResult> OnPost(string id)
        {
            var isSuccess = await _inventoryService.RestoreInventory(Guid.Parse(id));
            if(isSuccess)
            {
                InventoryDTOs = (await _inventoryService.GetAllInventory()).Where(i => i.Is_Inventory_Status == false).ToList();
            }
            return Page();
        }
    }
}
