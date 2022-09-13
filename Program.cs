using ProyHerramientas.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

/*services.AddRazorPages();
services.AddSingleton<Model.Profesor>();
//registrarse como un servicio Scoped para que cada usuario diferente reciba su propia instancia del servicio durante la duración de su sesión. .
//Un Singleton permite compartir la misma instancia de una clase de servicio entre componentes. 
//Esto es bueno porque desea asegurarse de que todas sus páginas reciban la misma instancia de la clase de servicio Profesor. 
//Esto garantizará que los datos (es decir, Legajo, en este ejemplo) se compartan correctamente entre las páginas a lo largo de la vida útil de la aplicación.
services.AddScoped<Model.Profesor>();*/

builder.Services.AddSingleton<IProfesorServicio, ProfesorServicio>();
//builder.services.AddSingleton<IMateriaServicio, MateriaServicio>();

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
