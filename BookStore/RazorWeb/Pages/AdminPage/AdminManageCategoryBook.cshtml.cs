using AutoMapper;
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
        IMapper _mapper;
        public AdminManageCategoryBookModel(IBookService book, ICategoryService category, IMapper mapper)
        {
            _book = book;
            _category = category;
            _mapper = mapper;
        }
        [BindProperty]
        public string search { get; set; }
        public string nameCategory { get; set; }
        [BindProperty(Name = "idc", SupportsGet = true)]
        public int Id { get; set; }

        public List<BookDTO> books { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;

        public void OnGet(int pageNumber = 1)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            CurrentPage = pageNumber;
            var listBook = _book.GetBookByCategory(Id);
            var bookList = _mapper.Map<List<Book>>(listBook.Result.ToList());
            var listBookPage = _book.TakePageBook(pageNumber, bookList);
            books = listBookPage.Result.ToList();
            var name = _category.GetCategoryById(Id).Result.Category_Name;
            nameCategory = name;
            float i = ((float)listBook.Result.ToList().Count())/4;
            var checkI = i - (float)Math.Round(i);
            if (checkI > 0)
            {
                TotalPages = (int)Math.Round(i + 0.5);
            } else
            {
                TotalPages = (int)Math.Round(i);
            }

        }
    }
}
