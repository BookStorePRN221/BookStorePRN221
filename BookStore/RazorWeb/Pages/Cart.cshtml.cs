using System.Text.Json;
using BookStoreAPI.Core.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWeb.Pages
{
    public class CartModel : PageModel
    {

        
        public List<BookDTO> CartItems { get; set; }

        public float CartTotal => CartItems.Sum(item => item.Book_Price);
        
        public void OnGet()
        {
            GetItemFromSession();
        }

        public void OnPostDelete(Guid itemId)
        {
            Console.WriteLine(itemId);
            GetItemFromSession();
            var itemToRemove = CartItems.FirstOrDefault(item => item.Book_Id == itemId);
            if (itemToRemove != null)
            {
                CartItems.Remove(itemToRemove);
                SaveCartItemsToSession();
            }
        }

        private void GetItemFromSession()
        {
            var cartItemsJson = HttpContext.Session.GetString("CartItems");
            if (!string.IsNullOrEmpty(cartItemsJson))
            {
                CartItems = JsonSerializer.Deserialize<List<BookDTO>>(cartItemsJson);
            }
            else
            {
                CartItems = new List<BookDTO>();
            }
        }

        private void SaveCartItemsToSession()
        {
            var cartItemsJson = JsonSerializer.Serialize(CartItems);
            HttpContext.Session.SetString("CartItems", cartItemsJson);
        }
    }
}
