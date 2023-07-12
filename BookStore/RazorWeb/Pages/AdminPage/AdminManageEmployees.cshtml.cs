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
        [BindProperty]
        public int m_Message { get; set; } = 0;
        public async Task OnGet()
        {
            var listUser = await user.GetAllUser();
            UserList=listUser.ToList();
        }
        public async Task<IActionResult> OnPost()
        {
          var _update= await user.UpdateUser(User);
            if (_update)
            {
                m_Message = 1;
                await OnGet();
                return Page();
            }
            m_Message = 2;
            return Page();
        }
    }
}
