using fomezero.Models; // Certifique-se de usar o namespace correto para seu contexto
using Microsoft.EntityFrameworkCore; // Necess�rio para AddDbContext

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Adicionando o FomezeroContext ao container de servi�os
builder.Services.AddDbContext<FomezeroContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "usuario",
    pattern: "Usuario/{action=Index}/{id?}",
    defaults: new { controller = "Usuario" });

app.MapControllerRoute(
    name: "doacoes",
    pattern: "doacoes/{action=Index}/{id?}",
    defaults: new { controller = "Doacoes" });

app.Run();
