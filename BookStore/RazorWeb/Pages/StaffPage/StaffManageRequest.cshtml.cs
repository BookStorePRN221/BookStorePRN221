using AutoMapper;
using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RazorWeb.Pages.StaffPage
{
    public class StaffManageRequestModel : PageModel
    {
        IRequestService _request;
        private ICategoryService _category;
        IMapper _map;
        private IBookService _book;
        private bool m_update;
        public StaffManageRequestModel(IRequestService request, ICategoryService category, IMapper map, IBookService book)
        {
            _request = request;
            _category = category;
            _map = map;
            _book = book;
        }
        [BindProperty]
        public bool is_Book_Create { get; set; }
        [BindProperty]
        public BookingRequest request { get; set; }
        [BindProperty]
        public List<BookingRequest> requestDTOs { get; set; }
        [BindProperty]
        public List<Category> listCate { get; set; }
      
        [BindProperty]
        public int m_Message { get; set; } = 0;
        [BindProperty]
        public BookDTO bookDTO { get; set; }
        [BindProperty]
        public List<Book> books { get; set; }

        public async Task OnGet()
        {
            var list = await _category.GetAllCategory();
            listCate = list.ToList();
            var listRequest = await _request.GetAllRequest();
            requestDTOs = listRequest.ToList();
            var listBook = await _book.GetBook();
            books = listBook.ToList();
        }
        public async Task<IActionResult> OnPost(int number)
        {
            switch (number)
            {
                //create request
                case 1:
                    if (is_Book_Create)
                    {
                        request.Request_Amount = request.Request_Quantity * request.Request_Price;
                        m_update = await _request.CreateRequest(request, is_Book_Create);
                    }
                    else
                    {
                        var books = await _book.GetBookById(request.Book_Id);
                        request.Request_Amount = request.Request_Quantity * books.Book_Price;
                        m_update = await _request.CreateRequest(request, is_Book_Create);
                    }
                    break;
                //create book
                case 2:
                    var book = _map.Map<Book>(bookDTO);
                    m_update = await _book.CreateBook(book, bookDTO.Image_URL, bookDTO.Request_Id);
                    break;
                case 3:
                    m_update = await _request.UpdateRequestUnDone(request.Request_Id, request.Request_Note);
                    break;
            }

            await OnGet();
            if (m_update)
            {
                m_Message = 1;
                return Page();
            }
            m_Message = 2;
            return Page();
        }
    }
}
