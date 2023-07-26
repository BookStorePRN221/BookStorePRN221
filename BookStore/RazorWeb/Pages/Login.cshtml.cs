using BookStoreAPI.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Service.Service.IService;

namespace RazorWeb.Pages
{
    public class LoginModel : PageModel
    {
        IUserService _user;
        private readonly IHttpContextAccessor _context;
        public LoginModel(IUserService user, IHttpContextAccessor httpContextAccessor)
        {
            _user = user;
            _context = httpContextAccessor;
        }
        [BindProperty]
        public LoginDTO login { get; set; }
       
        public async Task<IActionResult> OnPost()
        {
            var check = await _user.CheckLogin(login);
            if(check != null)
            {
                var loginJson = JsonConvert.SerializeObject(check);
                _context.HttpContext.Session.SetString("UserLogin", loginJson);
                switch (check.Role_Id)
                {
                    case 1:
                        return RedirectToPage("/AdminPage/AdminOverView");
                    case 2:
                        return RedirectToPage("/StaffPage/StaffManageNodifycshtml");
                    case 3:
                        return RedirectToPage("/SellerPage/SellerManageNodify");
                }
            }
            else
            {
                ViewData["Message"] = "Wrong Account Or Password, Please Login Again!";
                return Page();
            }
            return Page();
        }

    }
}
