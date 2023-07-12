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
        IMapper _map;
        public AdminRequestImPortBookModel(IRequestService request, IMapper map)
        {
            _request = request;
            _map = map;
        }
        [BindProperty]
        public List<BookingRequest> requestDTOs { get; set; }
        [BindProperty]
        public BookingRequest request { get; set; }

        public async Task OnGet()
        {
           var list = await _request.GetAllRequest();
           requestDTOs = list.ToList();
        }
    }
}
