using Azure.Core;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.AdminPage
{
    public class AdminManageRequestModel : PageModel
    {
        IRequestService _request;
        private ICategoryService _category;
        IBookService _book;
        public AdminManageRequestModel(IRequestService request, ICategoryService category, IBookService book)
        {
            _request = request;
            _category = category;
            _book = book;
        }
        [BindProperty]
        public List<Category> listCate { get; set; }
        [BindProperty]
        public BookingRequest request { get; set; }
        [BindProperty]
        public Guid requestId { get; set; }
        [BindProperty]
        public List<BookingRequest> requestDTOs { get; set; }
        [BindProperty]
        public List<Book> books { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var list = await _category.GetAllCategory();
            listCate = list.ToList();
            var listRequest = await _request.GetAllRequest();
            requestDTOs = listRequest.ToList();
            var listBook = await _book.GetBook();
            books = listBook.ToList();
            return Page();
        }
        public async Task<IActionResult> OnPost(int isCheck)
        {
            switch (isCheck)
            {
                case 0:
                    await _request.UpdateRequestUnDone(requestId, "Admin Cancel Request");
                    break;
                case 1:
                    await _request.ConfirmRequest(requestId);
                    break;
                case 2: //again new
                    var m_update = await _request.GetRequestById(request.Request_Id);
                    if (m_update != null)
                    {
                        m_update.Request_Image_Url = request.Request_Image_Url;
                        m_update.Request_Book_Name = request.Request_Book_Name;
                        m_update.Request_Quantity = request.Request_Quantity;
                        m_update.Request_Price = request.Request_Price;
                        m_update.Request_Note= request.Request_Note;
                        m_update.Category_Id= request.Category_Id;
                        m_update.Request_Amount = request.Request_Price * request.Request_Quantity;
                    }
                    await _request.UpdateRequest(m_update);
                    break;
                case 3://again old
                    var m_update_old = await _request.GetRequestById(request.Request_Id);
                    if (m_update_old != null)
                    {
                        var books = await _book.GetBookById(request.Book_Id);
                        m_update_old.Book_Id= request.Book_Id;
                        m_update_old.Request_Book_Name = books.Book_Title;
                        m_update_old.Request_Image_Url = books.Image_URL.FirstOrDefault();
                        m_update_old.Request_Price = books.Book_Price;
                        m_update_old.Request_Quantity= request.Request_Quantity;
                        m_update_old.Request_Note=request.Request_Note;
                        m_update_old.Request_Amount = request.Request_Quantity * books.Book_Price;
                    }
                    await _request.UpdateRequest(m_update_old);
                    break;
            }
            await OnGet();
            return Page();
        }
    }
}
