using fomezero.Models; // Certifique-se de usar o namespace correto para seu contexto
using Microsoft.EntityFrameworkCore; // Necessário para AddDbContext

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Habilitar Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de vida da sessão
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
app.UseStaticFiles(); // Certifique-se de incluir isso para servir arquivos estáticos

app.UseRouting();
app.UseSession(); // Middleware para habilitar o uso de sessão
app.UseAuthorization();

// Middleware para mapear rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "usuarios",
    pattern: "usuarios/{action=Index}/{id?}",
    defaults: new { controller = "Usuarios" });

app.MapControllerRoute(
    name: "Doacoes",
    pattern: "Doacoes/{action=Index}/{id?}",
    defaults: new { controller = "Doacoes" });

app.MapControllerRoute(
    name: "retirada_Doacoes",
    pattern: "retiradas-Doacoes/{action=Index}/{id?}",
    defaults: new { controller = "RetiradaDoacoes" });

app.MapControllerRoute(
    name: "instituicoes",
    pattern: "instituicoes/{action=Index}/{id?}",
    defaults: new { controller = "Instituicoes" });

app.MapControllerRoute(
    name: "Doacoes_instituicoes",
    pattern: "Doacoes-instituicoes/{action=Index}/{id?}",
    defaults: new { controller = "DoacoesInstituicoes" });

app.MapControllerRoute(
    name: "tipo_Doacoes",
    pattern: "tipos-Doacoes/{action=Index}/{id?}",
    defaults: new { controller = "TipoDoacaos" });

app.MapControllerRoute(
    name: "locais_retirada",
    pattern: "locais-retirada/{action=Index}/{id?}",
    defaults: new { controller = "LocaisRetiradums" });

app.Run();
