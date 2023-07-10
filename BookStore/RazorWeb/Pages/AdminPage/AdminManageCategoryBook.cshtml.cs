using AutoMapper.Execution;
using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.AdminPage
{
    public class AdminManageCategoryBookModel : PageModel
    {
        IBookService _book;
        ICategoryService _category;
        public AdminManageCategoryBookModel(IBookService book, ICategoryService category)
        {
            _book = book;
            _category = category;
        }
        [BindProperty]
        public string search { get; set; }
        public string nameCategory { get; set; }
        [BindProperty(Name = "idc", SupportsGet = true)]
        public int Id { get; set; }

        public List<BookDTO> books { get; set; }
        public void OnGet()
        {
            var listBook = _book.GetBookByCategory(Id);
            books = listBook.Result.ToList();
            var name = _category.GetCategoryById(Id).Result.Category_Name;
            nameCategory = name;
        }
    }
}
