using BookStoreAPI.Core.DiplayDTO;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.AdminPage;

public class AdminManagerImportation : PageModel
{
    
    private readonly IImportationService _importationService;
    
    public AdminManagerImportation(IImportationService importationService)
    {
        _importationService = importationService;
    }
    
    [BindProperty(SupportsGet = true)]
    public string search { get; set; }
    
    public List<DisplayImportationDTO> Importations { get; set; } = default!;
    
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; } = 1;
    private const int PageSize = 10;

    public async Task OnGetAsync(int pageNumber = 1)
    {
        if (pageNumber < 1)
            pageNumber = 1;
        
        CurrentPage = pageNumber;
        
        Page<DisplayImportationDTO> importations = _importationService.GetPaginatedDisplayImport(PageSize, CurrentPage).Result;
        Importations = importations.Items;
        TotalPages = importations.TotalPages;
    }
}