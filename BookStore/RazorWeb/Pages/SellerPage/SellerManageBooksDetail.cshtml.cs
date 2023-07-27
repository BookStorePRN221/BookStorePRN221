using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.SellerPage
{
    public class SellerManageBooksDetailModel : PageModel
    {
        private readonly IBookService _bookService;

        public SellerManageBooksDetailModel(IBookService bookService)
        {
            _bookService = bookService;
        }
        
        public BookDetailDTO Book { get; set; }
        
        [BindProperty] public int CartQuantity { get; set; }
        
        public Category Category { get; set; }

        public void OnGet(Guid bookId, int categoryId, string bookName)
        {
            Book = _bookService.GetBookById(bookId).Result;
        }
    }
}