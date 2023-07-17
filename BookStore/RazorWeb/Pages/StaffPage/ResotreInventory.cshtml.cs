using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.StaffPage
{
    public class ResotreInventoryModel : PageModel
    {
        private readonly IInventoryService _inventoryService;
        public ResotreInventoryModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }
        [BindProperty(SupportsGet = true)]
        public Guid Inventory_Id { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var isSuccess = await _inventoryService.RestoreInventory(Inventory_Id);
            if (!isSuccess)
            {
                Console.WriteLine("Restore not success");
            }
            return RedirectToPage("./StaffManageInventoryRstore");
        }
    }
}
