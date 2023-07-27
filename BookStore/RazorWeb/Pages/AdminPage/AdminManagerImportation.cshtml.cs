using BookStoreAPI.Core.DiplayDTO;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.AdminPage;

public class AdminManagerImportation : PageModel
{

	private readonly IImportationService _importationService;
	private readonly IImportationDetailService _ImportationDetailService;
	private readonly ICategoryService _categoryService;

	public AdminManagerImportation(IImportationService importationService, IImportationDetailService ImportationDetailService)
	{
		_importationService = importationService;
		_ImportationDetailService = ImportationDetailService;
	}

	[BindProperty(SupportsGet = true)]
	public List<DisplayImportationDTO> Importations { get; set; }
	[BindProperty(SupportsGet = true)]
	public List<DiplayImportationDetailDTO> ImportationDetails { get; set; }

	public void OnGet()
	{
		Importations = _importationService.GetDiplayImport().Result.ToList();
		ImportationDetails = _ImportationDetailService.GetDiplayImportDetail().Result.ToList();
	}

}
