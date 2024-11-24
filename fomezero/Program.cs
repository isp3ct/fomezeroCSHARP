using fomezero.Models; // Certifique-se de usar o namespace correto para seu contexto
using Microsoft.EntityFrameworkCore; // Necessário para AddDbContext

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Adicionando o FomezeroContext ao container de serviços
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
    pattern: "usuario/{action=Index}/{id?}",
    defaults: new { controller = "Usuarios" });

app.MapControllerRoute(
    name: "doacoes",
    pattern: "doacoes/{action=Index}/{id?}",
    defaults: new { controller = "Doacoes" });

app.MapControllerRoute(
    name: "retirada_doações",
    pattern: "RetiradaDoações/{action=Index}/{id?}",
    defaults: new { controller = "RetiradaDoacoes" });

app.MapControllerRoute(
    name: "instituicoes",
    pattern: "instituicoes/{action=Index}/{id?}",
    defaults: new { controller = "Instituicoes" });


app.Run();
