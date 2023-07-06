using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.AdminPage
{
    public class AdminManageBookModel : PageModel
    {
        IBookService _book;
        public AdminManageBookModel(IBookService book)
        {
            _book = book;
        }
        [BindProperty]
        public List<BookDTO> books { get; set; }
        public async Task OnGet()
        {
            
            var listBook = await _book.TakePageBook(1);
            books = listBook.ToList();
        }
    }
}
