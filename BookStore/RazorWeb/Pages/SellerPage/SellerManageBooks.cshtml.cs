using AutoMapper;
using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Interface;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;
using static System.Reflection.Metadata.BlobBuilder;

namespace RazorWeb.Pages.SellerPage
{
    public class SellerManageBooksModel : PageModel
    {

        private readonly IBookService _book;
        private readonly ICategoryService _category;
        private readonly IImageService _image;
        private readonly IMapper _map;
        public SellerManageBooksModel(IBookService book, ICategoryService category, IMapper map, IImageService image)
        {
            _book = book;
            _category = category;
            _map = map;
            _image = image;
        }

        [BindProperty(SupportsGet = true)]
        public int SelectedCategory { get; set; } = -1;
        public List<BookDTO> Books { get; set; }
        public List<Category> Categories { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public async Task OnGet(int pageNumber = 1)
        {
            if (pageNumber < 1)
                pageNumber = 1;
            Categories = _category.GetAllCategory().Result.ToList();
            if (SelectedCategory == -1)
            {
                CurrentPage = pageNumber;
                var listBook = await _book.GetBook();
                var listBookPage = await _book.TakePage(pageNumber, listBook);
                Books = _map.Map<List<BookDTO>>(listBookPage).ToList();
                foreach (var book in Books)
                {
                    book.Image_URL = (await _image.GetAllImage(book.Book_Id)).ToList().First().Image_URL;
                }
                var listCategory = await _category.GetAllCategory();
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
            else
            {
                CurrentPage = pageNumber;
                var listBook = await _book.GetBookByCategory(SelectedCategory);
                var bookList = _map.Map<List<Book>>(listBook.ToList());
                var listBookPage = await _book.TakePage(pageNumber, bookList);
                Books = _map.Map<List<BookDTO>>(listBookPage).ToList();
                foreach (var book in Books)
                {
                    book.Image_URL = (await _image.GetAllImage(book.Book_Id)).ToList().First().Image_URL;
                }
                var listCategory = await _category.GetAllCategory();
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
            
        }

    }
}