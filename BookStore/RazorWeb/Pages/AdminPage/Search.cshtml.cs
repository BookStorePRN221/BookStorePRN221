using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWeb.Pages.AdminPage
{
    public class SearchModel : PageModel
    {
        public string name_search { get; set; } = default!;
        public void OnGet(string search)
        {

        }
    }
}
