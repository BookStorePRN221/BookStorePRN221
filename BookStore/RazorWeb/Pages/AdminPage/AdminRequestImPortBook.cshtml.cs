using AutoMapper;
using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.AdminPage
{
    public class AdminRequestImPortBookModel : PageModel
    {
        IRequestService _request;
        private ICategoryService _category;
        IMapper _map;
        private IBookService _book;
        public AdminRequestImPortBookModel(IRequestService request, IMapper map, ICategoryService category, IBookService book)
        {
            _request = request;
            _map = map;
            _category = category;
            _book = book;
        }
        [BindProperty]
        public List<BookingRequest> requestDTOs { get; set; }
        [BindProperty]
        public BookingRequest request { get; set; }
        [BindProperty]
        public List<Category> listCate { get; set; }
        [BindProperty]
        public List<Book> books { get; set; }
        [BindProperty]
        public bool is_Book_Create { get; set; }
        [BindProperty]
        public int m_Message { get; set; } = 0;
        public async Task OnGet()
        {
            var list = await _category.GetAllCategory();
            listCate = list.ToList();
            var listRequest = await _request.GetAllRequest();
           requestDTOs = listRequest.ToList();
            var listBook = await _book.GetBook();
            books=listBook.ToList();
        }
        public async Task<IActionResult> OnPost()
        {
            bool _update;
            if (is_Book_Create)
            {
                request.Request_Amount = request.Request_Quantity * request.Request_Price;
                _update = await _request.CreateRequest(request, is_Book_Create);
            }
            else
            {
                var book = await _book.GetBookById(request.Book_Id);
                request.Request_Amount = request.Request_Quantity * book.Book_Price;
                _update = await _request.CreateRequest(request, is_Book_Create);
            }
            await OnGet();
            if (_update)
            {
                m_Message = 1;
                return Page();
            }
            m_Message = 2;
            return Page();
        }
    }
}
