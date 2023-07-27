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

        public SellerManageCartModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        public Dictionary<Guid, int> Cart { get; set; } = default!;
        public Dictionary<Guid, BookDetailDTO> CartItems { get; set; } = new Dictionary<Guid, BookDetailDTO>();

        public void OnGet()
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

        public IActionResult OnPost()
        {
            var loginJson = HttpContext.Session.GetString("UserLogin");
            if (loginJson != null)
            {
                var user = JsonSerializer.Deserialize<User>(loginJson);
            }

            return Page();
        }
    }
}