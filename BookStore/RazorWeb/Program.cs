using BookStoreAPI.Infracstructure.Helper;
using BookStoreAPI.Infracstructure.ServiceExtension;
using Service.Service.IService;
using Service.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri("https://book0209.azurewebsites.net/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Add services to the container.
builder.Services.AddRazorPages(option =>
{
    option.Conventions.AddPageRoute("/AdminPage/{*path}", "/AdminPage");
    option.Conventions.AddPageRoute("/StaffPage/{*path}", "/StaffPage");
    option.Conventions.AddPageRoute("/SellerPage/{*path}", "/SellerPage");
    option.Conventions.AddPageRoute("/StaffManageRequest", "/StaffManageRequest/SaveOptions");
});

builder.Services.AddSession();

builder.Services.AddDIServices(builder.Configuration);
//Scoped
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IImportationService, ImportationService>();
builder.Services.AddScoped<IImportationDetailService, ImportationDetailService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian tồn tại của session (30 phút trong ví dụ này)
    options.Cookie.HttpOnly = true; // Chỉ sử dụng cookie từ phía máy chủ, không cho phép JavaScript truy cập
    options.Cookie.IsEssential = true; // Đánh dấu session cookie là bắt buộc cho ứng dụng
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapRazorPages("/AdminPage", "/AdminPage");
//});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.UseSession();

app.Run();
