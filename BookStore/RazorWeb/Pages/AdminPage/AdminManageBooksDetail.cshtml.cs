using AutoMapper;
using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Service.IService;

namespace RazorWeb.Pages.AdminPage
{
    public class AdminManageBooksDetailModel : PageModel
    {
        private IBookService _book;
        private IMapper _map;
        public AdminManageBooksDetailModel(IBookService book,IMapper mapper)
        {
            _book = book;
            _map = mapper;
        }
        [BindProperty]
        public BookDetailDTO bookDetail { get; set; }
        [BindProperty(SupportsGet = true)]
        public Guid Book_Id { get; set; }
        public async Task OnGet()
        {
            bookDetail = await _book.GetBookById(Book_Id);
        }
    }
}
