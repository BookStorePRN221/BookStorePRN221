using BookStoreAPI.Core.DiplayDTO;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml.Style;
using Service.Service;
using Service.Service.IService;

namespace RazorWeb.Pages.StaffPage
{
    public class DeleteOrderModel : PageModel
    {
		private readonly IOrderService _orderService;
		private readonly IOrderDetailService _orderDetailService;
		public DeleteOrderModel(IOrderService orderService, IOrderDetailService orderDetailService)
		{
			_orderService = orderService;
			_orderDetailService = orderDetailService;
		}
		[BindProperty(SupportsGet = true)]
		public Guid Order_Id { get; set; }
		[BindProperty(SupportsGet = true)]
		public Order orders { get; set; }
		[BindProperty(SupportsGet = true)]
		public List<DisplayOrderDetailDTO> orderDetails { get; set; }
		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPost()
		{
			orders = _orderService.GetOrderByOrderId(Order_Id).Result;
			orderDetails = _orderDetailService.GetOrderDetailByOrderId(Order_Id).Result.ToList();
			var isSuccess = await _orderService.DeleteOrder(Order_Id);
			if (!isSuccess)
			{
				Console.WriteLine("delete not success");
			}
			return RedirectToPage("./SellerManageOrder");
		}
	}
}
