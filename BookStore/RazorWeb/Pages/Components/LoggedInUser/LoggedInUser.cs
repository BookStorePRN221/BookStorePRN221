using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RazorWeb.Pages.Components.LoggedInUser
{
    public class LoggedInUserViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(DefaultModel model)
        {
            // Lấy thông tin đăng nhập từ session
            var loginJson = HttpContext.Session.GetString("UserLogin");
            if (loginJson != null)
            {
                // Chuyển đổi chuỗi JSON thành đối tượng LoginDTO
                var login = JsonConvert.DeserializeObject<User>(loginJson);
                // Lưu thông tin đăng nhập vào HttpContext.Items để sử dụng trong layout
                ViewBag.LoggedInUser = login;
            }

            // Trả về một ViewComponentResult (có thể là một partial view)
            return View(model);
        }
    }
}
