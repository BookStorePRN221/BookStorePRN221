using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;
using BookStoreAPI.Core.Model;
using BookStoreAPI.Core.DiplayDTO;
using Microsoft.EntityFrameworkCore;

namespace RazorWeb.Pages.SellerPage
{
	public class SellerManageOrderModel : PageModel
	{
        private readonly IHttpContextAccessor _context;
        private readonly IOrderService _orderService;
		private readonly IOrderDetailService _orderDetailService;

		public SellerManageOrderModel(IOrderService orderService, IOrderDetailService orderDetailService)
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