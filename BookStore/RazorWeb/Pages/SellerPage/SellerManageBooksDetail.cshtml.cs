using System.Text.Json;
using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.SellerPage
{
    public class SellerManageBooksDetailModel : PageModel
    {
        private readonly IBookService _bookService;

        public SellerManageBooksDetailModel(IBookService bookService)
        {
            _bookService = bookService;
        }
        
        public BookDetailDTO Book { get; set; }

        
        public Category Category { get; set; }

        private Dictionary<Guid, int> Cart { get; set; }

        public void OnGet(Guid bookId, int categoryId, string bookName)
        {
            Book = _bookService.GetBookById(bookId).Result;
        }

        public IActionResult OnPost(int cartQuantity, Guid bookId)
        {
            
            Book = _bookService.GetBookById(bookId).Result;
            
            // If the cart is not initialized yet, initialize it in the session
            if (HttpContext.Session.TryGetValue("Cart", out byte[] cartData))
            {
                Cart = JsonSerializer.Deserialize<Dictionary<Guid, int>>(cartData);
            }
            else
            {
                Cart = new Dictionary<Guid, int>();
            }

            if (Cart.TryGetValue(bookId, out int existingQuantity))
            {
                Cart[bookId] = existingQuantity + cartQuantity;
            } else
            {
                // If the book is not in the cart, add it with the specified quantity
                Cart.Add(bookId, cartQuantity);
            }
            
            // Save the cart back to the session
            HttpContext.Session.Set("Cart", JsonSerializer.SerializeToUtf8Bytes(Cart));
            
            // Redirect to the cart page
            return RedirectToPage("/SellerPage/SellerManageCart");
        }
    }
}