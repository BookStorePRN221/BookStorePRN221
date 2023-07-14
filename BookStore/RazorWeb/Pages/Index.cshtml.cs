using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBookService _book;
        public IndexModel(ILogger<IndexModel> logger,IBookService book)
        {
            _logger = logger;
            _book = book;
        }

        public async Task OnGet()
        {
        }
    }
}