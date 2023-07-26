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
        public AdminManageRequestModel(IRequestService request)
        {
            _request = request;
        }
        [BindProperty(SupportsGet = true)]
        public string SelectedType { get; set; }

        [BindProperty]
        public List<BookingRequest> requestDTOs { get; set; }
        public async Task<IActionResult> OnGet()
        {
            // Mặc định ban đầu chọn "confirm" khi truy cập trang.
            SelectedType = string.IsNullOrEmpty(SelectedType) ? "confirm" : SelectedType;
            var listRequest = await _request.GetAllRequest();
            requestDTOs = listRequest.ToList();
            return Page();
        }
    }
}
