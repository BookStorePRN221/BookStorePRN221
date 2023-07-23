using AutoMapper;
using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.StaffPage
{
    public class StaffManageRequestModel : PageModel
    {
        IRequestService _request;
        private ICategoryService _category;
        IMapper _map;
        private IBookService _book;
        public StaffManageRequestModel(IRequestService request, ICategoryService category, IMapper map, IBookService book)
        {
            _request = request;
            _category = category;
            _map = map;
            _book = book;
        }
        [BindProperty]
        public List<BookingRequest> requestDTOs { get; set; }
        [BindProperty]
        public List<Category> listCate { get; set; }
      
        [BindProperty]
        public int m_Message { get; set; } = 0;
        [BindProperty]
        public BookDTO bookDTO { get; set; }
        public async Task OnGet()
        {
            var list = await _category.GetAllCategory();
            listCate = list.ToList();
            var listRequest = await _request.GetAllRequest();
            requestDTOs = listRequest.ToList();
        }
        //public async Task<IActionResult> OnPost()
        //{
          
        //    //await OnGet();
        //    //if (_update)
        //    //{
        //    //    m_Message = 1;
        //    //    return Page();
        //    //}
        //    //m_Message = 2;
        //    //return Page();
        //}
    }
}
