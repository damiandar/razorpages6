using ProyHerramientas.Servicios;
using Microsoft.EntityFrameworkCore;
using ProyHerramientas.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<SchoolContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnectionString")));

/*services.AddRazorPages();
services.AddSingleton<Model.Profesor>();
//registrarse como un servicio Scoped para que cada usuario diferente reciba su propia instancia del servicio durante la duraci�n de su sesi�n. .
//Un Singleton permite compartir la misma instancia de una clase de servicio entre componentes. 
//Esto es bueno porque desea asegurarse de que todas sus p�ginas reciban la misma instancia de la clase de servicio Profesor. 
//Esto garantizar� que los datos (es decir, Legajo, en este ejemplo) se compartan correctamente entre las p�ginas a lo largo de la vida �til de la aplicaci�n.
services.AddScoped<Model.Profesor>();*/

builder.Services.AddSingleton<IProfesorServicio, ProfesorServicio>();
builder.Services.AddSingleton<IMateriaServicio, MateriaServicio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
