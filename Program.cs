using Microsoft.Data.SqlClient;
using project1.Repositories;
using project1.Services;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// SQL ba?lant?s? için Connection String
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// IDbConnection'? DI container'a ekliyoruz
builder.Services.AddTransient<IDbConnection>(sp => new SqlConnection(connectionString));

// UserRepository'yi DI container'a ekliyoruz
builder.Services.AddScoped<UserRepository>();

// TaskRepository'yi DI container'a ekliyoruz
builder.Services.AddScoped<TaskRepository>(); // Burada TaskRepository'yi ekliyoruz

// UserService s?n?f?n? DI container'a ekliyoruz
builder.Services.AddScoped<UserService>();

// TaskService s?n?f?n? DI container'a ekliyoruz
builder.Services.AddScoped<TaskService>();

// MVC servisini ekliyoruz
builder.Services.AddControllersWithViews();

// Session hizmetini ekliyoruz
builder.Services.AddSession(); // Session middleware'i eklemek için
builder.Services.AddDistributedMemoryCache(); // Session için gerekli

var app = builder.Build();

// Enable middleware
app.UseSession(); // Session middleware'i ekliyoruz

app.UseRouting(); // Routing middleware'i

// Static file middleware'i (js, css, img dosyalar? için)
app.UseStaticFiles();

// Authorization middleware'i
app.UseAuthorization();

// Configuring endpoints
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.Run();
