using Microsoft.EntityFrameworkCore;
using RecordStore.Core.Interfaces;
using RecordStore.Core.Services;
using RecordStore.Data.Repositories;
using RecordStore.Data.Services;
using RecordStore.Web.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddScoped<IUserRepository>(provider =>
    new UserRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IRecordRepository>(sp => new RecordRepository(connectionString));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICartRepository, CookieCartRepository>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<RecordService>();
builder.Services.AddScoped<IAdminRepository, AdminService>();




builder.Services.AddRazorPages();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
