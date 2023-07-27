using BookStoreAPI.Core.DiplayDTO;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.AdminPage
{
    public class AdminManageOrderModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly ICategoryService _categoryService;

        public AdminManageOrderModel(IOrderService orderService, IOrderDetailService orderDetailService)
        {
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }

        [BindProperty(SupportsGet = true)]
        public List<Order> orders { get; set; }
        public List<DisplayOrderDetailDTO> orderDetails { get; set; }

        public void OnGet()
        {
            orders = _orderService.GetAllOrder().Result.ToList();
            orderDetails = _orderDetailService.GetDisplayOrderDetail().Result.ToList();
        }

    }
}