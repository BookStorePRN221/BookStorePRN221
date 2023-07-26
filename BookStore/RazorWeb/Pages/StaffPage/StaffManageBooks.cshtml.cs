using AutoMapper;
using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.StaffPage
{
    public class StaffManageBooksModel : PageModel
    {
        IBookService _book;
        IMapper _map;
        ICategoryService _category;
        public StaffManageBooksModel(IBookService book, ICategoryService category,IMapper mapper)
        {
            _book = book;
            _category = category;
            _map = mapper;
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
            var listBookPage = await _book.TakePage(number, listBook);
            books = _map.Map<List<BookDTO>>(listBookPage).ToList();
            var listCategory = await _category.GetAllCategory();
            categories = listCategory.ToList();
        }
        public async Task OnPost()
        {
            var searchBook = await _book.GetBookByName(search);
            var listBookPage = await _book.TakePage(number, searchBook);
            books = _map.Map<List<BookDTO>>(listBookPage).ToList();
        }
        public async Task<IActionResult> OnPostCategory()
        {
            var ID = Request.Form["choiceCategory"];
            return RedirectToPage("StaffManageCategoryBook", new { idc = ID });
        }
    }
}
