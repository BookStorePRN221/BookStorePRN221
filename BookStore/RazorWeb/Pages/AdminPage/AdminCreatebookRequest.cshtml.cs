using AutoMapper;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.AdminPage
{
    public class AdminCreatebookRequestModel : PageModel
    {
        public IRequestService _request;
        IMapper _map;
        public AdminCreatebookRequestModel(IRequestService request, IMapper mapper)
        {
            _request = request;
            _map = mapper;
        }
        [BindProperty]
        public BookingRequest Request { get; set; }
        public void OnGet()
        {
        }
        public void OnPost()
        {

        }
    }
}
