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
        IImageService _image;
        IMapper _map;
        public AdminManageBookModel(IBookService book, ICategoryService category,IMapper map, IImageService image)
        {
            _book = book;
            _category = category;
            _map = map;
            _image = image;
        }
        [BindProperty]
        public List<BookDTO> books { get; set; }
        public List<Category> categories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string search { get; set; }
        public string choice { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public async Task OnGet(int pageNumber = 1)
        {
            if (pageNumber < 1)
                pageNumber = 1;
            CurrentPage = pageNumber;
            var listBook = await _book.GetBook();
            var listBookPage = await _book.TakePage(pageNumber,listBook);
            books =_map.Map<List<BookDTO>>(listBookPage).ToList();
            foreach(var book in books)
            {
                book.Image_URL = (await _image.GetAllImage(book.Book_Id)).ToList().First().Image_URL;
            }
            var listCategory = await _category.GetAllCategory();
            categories = listCategory.ToList();
            float i = ((float)listBook.ToList().Count()) / 4;
            var checkI = i - (float)Math.Round(i);
            if (checkI > 0)
            {
                TotalPages = (int)Math.Round(i + 0.5);
            }
            else
            {
                TotalPages = (int)Math.Round(i);
            }
        }
        public async Task<IActionResult> OnPostCategory()
        {
            var ID = Request.Form["choiceCategory"];
            return RedirectToPage("AdminManageCategoryBook", new{ idc = ID});
        }
    }
}
