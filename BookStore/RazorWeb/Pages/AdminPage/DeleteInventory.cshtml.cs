using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.AdminPage
{
    public class DeleteInventoryModel : PageModel
    {
        private readonly IInventoryService _inventoryService;
        public DeleteInventoryModel(IInventoryService inventoryService)
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
            var isSuccess = await _inventoryService.DeleteInventory(Inventory_Id);
            if (!isSuccess)
            {
                Console.WriteLine("delete not success");
            }
            return RedirectToPage("./AdminManageInventory");
        }
    }
}
