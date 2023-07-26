using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Interface;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.SellerPage
{
    public class SellerManageBooksModel : PageModel
    {

        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;

        public SellerManageBooksModel(IBookService bookService, ICategoryService categoryService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
        }

        [BindProperty(SupportsGet = true)]
        public int SelectedCategory { get; set; } = -1;
        public List<BookDTO> Books { get; set; }
        public List<Category> Categories { get; set; }

        public void OnGet()
        {
            Categories = _categoryService.GetAllCategory().Result.ToList();
            if (SelectedCategory == -1)
            {
               
                Books = _bookService.GetAllBook().Result.ToList();
            }
            else
            {
                Books = _bookService.GetBookByCategory(SelectedCategory).Result.ToList();
            }
        }
        
    }
}
