using BookStoreAPI.Infracstructure.ServiceExtension;
using Service.Service.IService;
using Service.Service;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(option =>
{
    option.Conventions.AddPageRoute("/AdminPage/{*path}", "/AdminPage");
});
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

app.Run();
