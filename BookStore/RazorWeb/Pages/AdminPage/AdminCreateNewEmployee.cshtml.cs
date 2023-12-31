using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.AdminPage
{
    public class AdminCreateNewEmployeeModel : PageModel
    {
        public IUserService _user;
        public AdminCreateNewEmployeeModel(IUserService user)
        {
            _user = user;
        }
        [BindProperty]
        public User User { get; set; }
        public bool IsAccount { get; set; } = true;
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            var userList = await _user.GetAllUser();
            var checkAccount= (from u in userList where u.User_Account==User.User_Account select u).FirstOrDefault();
            if(checkAccount == null)
            {
                var result = await _user.CreateUserFE(User);
                if (result)
                    ViewData["Message"] = "Create Account Success!";
                else
                {
                    ViewData["Message"] = "Create Account Fail!";
                    IsAccount = false;
                }
                return Page();
            }
            IsAccount = false;
            return Page();
        }
    }
}
