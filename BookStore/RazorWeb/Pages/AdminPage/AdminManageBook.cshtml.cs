using AutoMapper;
using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;
using System.Reflection.Metadata;

namespace RazorWeb.Pages.AdminPage
{
    public class AdminManageBookModel : PageModel
    {
        IBookService _book;
        ICategoryService _category;
        IMapper _map;
        public AdminManageBookModel(IBookService book, ICategoryService category,IMapper map)
        {
            _book = book;
            _category = category;
            _map = map;
        }
        [BindProperty]
        public List<BookDTO> books { get; set; }
        public List<Category> categories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string search { get; set; }
        public int number { get; set; }
        public string choice { get; set; }
        public async Task OnGet()
        {
            var listBook = await _book.GetBook();
            var listBookPage = await _book.TakePage(number,listBook);
            books =_map.Map<List<BookDTO>>(listBookPage).ToList();
            var listCategory = await _category.GetAllCategory();
            categories = listCategory.ToList();
        }
        public async Task OnPost()
        {
            var searchBook= await _book.GetBookByName(search);
            var listBookPage = await _book.TakePage(number, searchBook);
            books = _map.Map<List<BookDTO>>(listBookPage).ToList();
        }
        public async Task<IActionResult> OnPostCategory()
        {
            var ID = Request.Form["choiceCategory"];
            return RedirectToPage("AdminManageCategoryBook", new{ idc = ID});
        }
    }
}
