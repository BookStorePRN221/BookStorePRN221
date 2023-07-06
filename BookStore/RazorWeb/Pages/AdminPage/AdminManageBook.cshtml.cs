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
        [BindProperty(SupportsGet = true)]
        public string search { get; set; }
        public int number { get; set; }
        public async Task OnGet()
        {
            var listBook = await _book.GetBook();
            var listBookPage = await _book.TakePageBook(number,listBook);
            books = listBookPage.ToList();
        }
        public async Task OnPost()
        {
            var searchBook= await _book.GetBookByName(search);
            var listBookPage = await _book.TakePageBook(number, searchBook);
            books = listBookPage.ToList();
        }
    }
}
