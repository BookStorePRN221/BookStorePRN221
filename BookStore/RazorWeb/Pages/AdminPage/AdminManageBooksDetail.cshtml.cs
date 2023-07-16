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
        private ICategoryService _category;
        private IMapper _map;
        private IInventoryService _inventory;
        public AdminManageBooksDetailModel(IBookService book,IMapper mapper, ICategoryService category, IInventoryService inventory)
        {
            _book = book;
            _map = mapper;
            _category = category;
            _inventory = inventory;
        }
        [BindProperty]
        public BookDetailDTO bookDetail { get; set; }
        [BindProperty]
        public List<Category> listCate { get; set; }
        [BindProperty(SupportsGet = true)]
        public Guid Book_Id { get; set; }
        [BindProperty]
        public int m_Message { get; set; } = 0;
        public async Task OnGet()
        {
            bookDetail = await _book.GetBookById(Book_Id);
            var list = await _category.GetAllCategory();
            listCate=list.ToList(); 
        }
        public async Task<IActionResult> OnPost()
        {
            var book= _map.Map<Book>(bookDetail);
            var _update = await _book.UpdateBook(book);
            if (_update)
            {
                m_Message = 1;
                return Page();
            }
            m_Message = 2;
            return Page();
        }
        public async Task<IActionResult> OnPostAddInventory()
        {
            return Page();
        }
    }
}
