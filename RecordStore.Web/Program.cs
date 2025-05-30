using Microsoft.EntityFrameworkCore;
using RecordStore.Core.Interfaces;
using RecordStore.Data.Repositories;
using RecordStore.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

// Get connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register repositories
builder.Services.AddScoped<RecordRepository>(sp => 
    new RecordRepository(connectionString ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// Register application services
builder.Services.AddScoped<CartService>();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
