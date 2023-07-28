using AutoMapper;
using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Service.Service.IService;

namespace RazorWeb.Pages.StaffPage
{
    public class StaffManageBooksDetailModel : PageModel
    {
        private IBookService _book;
        private ICategoryService _category;
        private IMapper _map;
        private IInventoryService _inventory;
        private readonly IHttpContextAccessor _context;
        public StaffManageBooksDetailModel(IBookService book, IMapper mapper, ICategoryService category, IInventoryService inventory, IHttpContextAccessor context)
        {
            _book = book;
            _map = mapper;
            _category = category;
            _inventory = inventory;
            _context = context;
        }
        [BindProperty]
        public BookDetailDTO bookDetail { get; set; }
        [BindProperty]
        public List<Category> listCate { get; set; }
        [BindProperty(SupportsGet = true)]
        public Guid Book_Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public InventoryDTO Inventorydto { get; set; }
        [BindProperty]
        public int m_Message { get; set; } = -1;
        public async Task OnGet()
        {
            bookDetail = await _book.GetBookById(Book_Id);
            var list = await _category.GetAllCategory();
            listCate = list.ToList();
        }
        public async Task<IActionResult> OnPost()
        {
            var book = _map.Map<Book>(bookDetail);
            var _update = await _book.UpdateBook(book);
            if (_update)
            {
                m_Message = 1;
                return Page();
            }
            m_Message = 0;
            return Page();
        }
        public async Task<IActionResult> OnPostAddInventory()
        {
            Book_Id = Inventorydto.Book_Id;
            bookDetail = await _book.GetBookById(Book_Id);
            if (Inventorydto.Inventory_Quantity > bookDetail.Book_Quantity)
            {

                var list = await _category.GetAllCategory();
                listCate = list.ToList();
                m_Message = 3;
                return Page();
            }
            var data = _context.HttpContext.Session.GetString("UserLogin");
            var user = JsonConvert.DeserializeObject<User>(data);
            if (user == null) { }
            var inventory = _map.Map<Inventory>(Inventorydto);
            inventory.User_Id = user.User_Id;
            var IsCreate = await _inventory.CreateInventory(inventory);
            if (IsCreate)
            {
                m_Message = 2;
                return RedirectToPage("/AdminPage/AdminManageInventory");
            }
            m_Message = 3;
            return Page();
        }
    }
}
