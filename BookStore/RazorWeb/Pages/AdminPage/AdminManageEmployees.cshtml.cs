using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.AdminPage
{
    public class AdminManageEmployeesModel : PageModel
    {
        IUserService user;
        public AdminManageEmployeesModel(IUserService user)
        {
            this.user = user;
        }
        [BindProperty]
        public List<User> UserList { get; set; }
        [BindProperty]
        public User User { get; set; }
        public async Task OnGet()
        {
            var listUser = await user.GetAllUser();
            UserList=listUser.ToList();
        }
        public async Task<IActionResult> OnPost()
        {
          var m_update= await user.UpdateUser(User);
            if (m_update)
            {
                return RedirectToPage();
            }
            return RedirectToPage();
        }
    }
}
