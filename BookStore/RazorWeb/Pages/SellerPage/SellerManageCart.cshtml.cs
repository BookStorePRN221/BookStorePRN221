using System.Text;
using System.Text.Json;
using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.SellerPage
{
    public class SellerManageCartModel : PageModel
    {
        private readonly IBookService _bookService;

        private readonly IHttpClientFactory _clientFactory;

        public SellerManageCartModel(IBookService bookService, IHttpClientFactory clientFactory)
        {
            _bookService = bookService;
            _clientFactory = clientFactory;
        }

        public Dictionary<Guid, int> Cart { get; set; } = default!;
        public Dictionary<Guid, BookDetailDTO> CartItems { get; set; } = new Dictionary<Guid, BookDetailDTO>();

        public void OnGet()
        {
           GetCart();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            GetCart();

            var loginJson = HttpContext.Session.GetString("UserLogin");
            if (loginJson == null)
            {
                return RedirectToPage("/login");
            }
            
            var user = JsonSerializer.Deserialize<User>(loginJson);
            
            int order_Quantity = Cart?.Count ?? 0;
            float order_Amount = GetTotalPrice();

            var data = new
            {
                user_Id = user.User_Id,
                order_Code = "string",
                order_Customer_Name = "string",
                order_Customer_Address = "string",
                order_Customer_Phone = "string",
                order_Quantity,
                order_Amount
            };

            var apiClient = _clientFactory.CreateClient("Api");

            // Call the API and handle the response
            var response = await apiClient.PostAsync("/api/order/createOrder",
                new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                // Handle the successful response here
                var result = await response.Content.ReadAsStringAsync();
                var isSuccess = await PostOrderDetail();
                if (isSuccess)
                {
                    HttpContext.Session.Remove("Cart");
                    return RedirectToPage("/SellerPage/SellerManageBooks");
                }
                
            }
            else
            {
                // Handle the error response here
                Console.WriteLine(response.ReasonPhrase);
            }

            return Page();
        }

        public void OnPostCartUpdate(Guid bookId, int quantityUpdate)
        {
            GetCart();
            Cart[bookId] = quantityUpdate;
            HttpContext.Session.Set("Cart", JsonSerializer.SerializeToUtf8Bytes(Cart));
        }

        public void OnPostCartDelete(Guid bookId)
        {
            GetCart();
            Cart.Remove(bookId);
            HttpContext.Session.Set("Cart", JsonSerializer.SerializeToUtf8Bytes(Cart));
        }

        private async Task<bool> PostOrderDetail()
        {
            var status = false;
            var apiClient = _clientFactory.CreateClient("Api");
            
            string orderId = await GetOrderIdJustCreated();

            if (orderId == null)
            {
                return false;
            }
            
            foreach (var (key, value) in Cart)
            {
             
                var data = new
                {
                    order_Id = orderId,
                    book_Id = key,
                    order_Detail_Quantity = value,
                    order_Detail_Amount = CartItems[key].Book_Price * value,
                    order_Detail_Price = CartItems[key].Book_Price
                };
                
                var response = await apiClient.PostAsync("/api/orderDetail/createOrderDetail",
                    new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    status = true;
                }
            }

            return status;
        }

        private async Task<string> GetOrderIdJustCreated()
        {
            var apiClient = _clientFactory.CreateClient("Api");
            var response = await apiClient.GetAsync("/api/order/getOrderIdJustCreated");
            
            var orderId = await response.Content.ReadAsStringAsync();

            if (orderId == "00000000-0000-0000-0000-000000000000")
            {
                return null;
            }

            orderId = orderId.Replace("\"", "");
            
            return orderId;
        }

        public float GetTotalPrice()
        {
            float sum = 0;

            if (Cart == null)
                return sum;

            foreach (var (key, value) in Cart)
            {
                sum += CartItems[key].Book_Price * value;
            }

            return sum;
        }

        private void GetCart()
        {
            if (HttpContext.Session.TryGetValue("Cart", out byte[] cartData))
            {
                Cart = JsonSerializer.Deserialize<Dictionary<Guid, int>>(cartData);
            }
            else
            {
                Cart = new Dictionary<Guid, int>();
            }
            
            foreach (var (key, value) in Cart)
            {
                CartItems.Add(key, _bookService.GetBookById(key).Result);
            }
        }
    }
}