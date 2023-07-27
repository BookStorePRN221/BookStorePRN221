using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.AdminPage
{
    public class DeleteImportationModel : PageModel
    {
        private readonly IImportationService _importationService;
        public DeleteImportationModel(IImportationService importationService)
        {
            _importationService = importationService;
        }
        [BindProperty(SupportsGet = true)]
        public Guid Importation_Id { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var isSuccess = await _importationService.DeleteImport(Importation_Id);
            if (!isSuccess)
            {
                Console.WriteLine("delete not success");
            }
            return RedirectToPage("./AdminManageImportation");
        }
    }
}
