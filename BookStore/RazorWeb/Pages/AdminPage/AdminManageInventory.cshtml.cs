using BookStoreAPI.Core.DiplayDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.StaffPage
{
    public class AdminManageInventoryModel : PageModel
    {
        private readonly IInventoryService _inventoryService;

        public AdminManageInventoryModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [BindProperty]
        public List<DisplayInventoryDTO?> InventoryDTOs { get; set; }


        public async Task OnGet()
        {
            InventoryDTOs = (await _inventoryService.GetAllInventory()).Where(i => i.Is_Inventory_Status == true).ToList() ?? null;
        }
    }
}
