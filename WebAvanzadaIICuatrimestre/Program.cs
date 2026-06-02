
using Microsoft.EntityFrameworkCore;
using WebAvanzadaIICuatrimestre.BLL;
using WebAvanzadaIICuatrimestre.BLL.Services.Carro;
using WebAvanzadaIICuatrimestre.BLL.Services.Duenno;
using WebAvanzadaIICuatrimestre.DAL.Data;
using WebAvanzadaIICuatrimestre.DAL.Repositorios.Carro;
using WebAvanzadaIICuatrimestre.DAL.Repositorios.Duenno;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register EF Core DbContext (SQLite). Update the connection string in appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


//Inyección de dependencias para repositorios, servicios, etc. Extraer a clase configuracion de servicios para mantener el Program.cs limpio y organizado. Se pueden crear clases estáticas para cada capa (Repositorios, Servicios, etc.) y llamar a sus métodos de configuración desde aquí para una mejor organización.
// Repositorios
builder.Services.AddScoped<ICarroRepositorio, CarroRepositorio>();
builder.Services.AddScoped<IDuennoRepositorio, DuennoRepositorio>();

//Servicios
builder.Services.AddScoped<ICarroServicio, CarroServicio>();
builder.Services.AddScoped<IDuennoServicio, DuennoServicio>();

// Servicios Terceros
builder.Services.AddAutoMapper(cfg => { }, typeof(MapeoClases)); // Directamente desde la documentación



















var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
    name: "Duenno",
    pattern: "{controller=Duenno}/{action=Index}/{id?}")
    .WithStaticAssets();

//FILTERS

//MIDDLEWARES

//INGRESO DE VARIASBLES DE ENTORNO AZURE KEYVAULTS





//NO SE PUEDAN REGLAS DE NEGOCIO


app.Run();
