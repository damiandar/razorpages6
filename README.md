# razorpages6 

echo "# razorpages6" >> README.md
git remote add origin https://github.com/damiandar/razorpages6.git

### CLI

dotnet tool list

dotnet list package

### Comandos

dotnet new webapp -o ProyRazorPages --no-https

dotnet new page -n PaginaPrueba -o ./Pages/

dotnet new page -n Listado -o Pages/Profesores

dotnet new page -n _FormProfesor -o ./Pages/Shared

## Seguridad Identity

dotnet add package Microsoft.AspNetCore.Identity.UI --version 5.0.12

dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 5.0.13

   > Microsoft.AspNetCore.Identity.EntityFrameworkCore      
   > Microsoft.AspNetCore.Identity.UI                   
   > Microsoft.EntityFrameworkCore.Design                  
   > Microsoft.EntityFrameworkCore.SqlServer               
   > Microsoft.EntityFrameworkCore.Tools                     
   > Microsoft.VisualStudio.Web.CodeGeneration.Design  
```html
dotnet-aspnet-codegenerator identity --dbContext AppDbContext --files "Account.Login;Account.Logout;Account.Register"
```
Si ejecuta el scaffolder de identidad sin especificar el indicador --files o el flag --useDefaultUI (-udui), todas las páginas de IU de identidad disponibles se crearán en su proyecto. Se puede abreviar --dbContext con -dc (Y puedo apuntarlo a mi context creado anteriormente).
### Paso 1
```html
dotnet-aspnet-codegenerator identity --files "Account.Login;Account.Logout;Account.Register"
```
1. Nos crea la carpeta Areas/Identity.
2. Dentro de esa carpeta tenemos las Pages.
3. Tambien nos crea un archivo IdentityHostingStartup.cs una especie de archivo startup apuntando a un **connection string** y a un **context**.
4. Un context en la carpeta data.
5. En el archivo appsetting agrega un connection string.

### Paso 2

Ejecuto la migración:
```html
dotnet ef migrations add CualquierNombre --context IdentityDbContext (nmbre del context creado en la carpeta data).
``` 
Dentro de la carpeta migrations crea un carpeta identitydb (Nombre del context) con las tablas a crear.

### Paso 3

Cambiar las configuraciones: En el startup de identity cambio el conection string por el que estaba usando en mi app. (voy y me fijo en el settings)
```html
dotnet ef database update --context IdentityDbContext
```
Luego vamos al sql y vemos las tablas creadas.

### Paso 4

Voy a startup y agrego
```html
app.UseAuthentication();
```

### Paso 5

Poner en el layout.cshtml un partial que llame a _LoginPartial (se creo cuando hice el code generator).

```html
<partial name="_LoginPartial" />
```
Utilizar identity en las páginas para mostrar o ocultar botones, etc.

```html
@using Microsoft.AspNetCore.Identity

@inject SignInManager<IdentityUser> SignInManager
@inject UserManager<IdentityUser> UserManager

@if (SignInManager.IsSignedIn(User))

@UserManager.GetUserName(User)

```

> Bloquear el acceso a paginas si no estoy logeado.
```html
services.AddRazorPages(o=> { o.Conventions.AuthorizePage("Index"); });
```

> Bloquear el acceso a carpetas si no estoy logeado.
```html
services.AddRazorPages(o=> { o.Conventions.AuthorizeFolder("/Profesores"); });
```

> Permitir el acceso anonimo a cierta página (antes bloqueda por AuthorizeFolder.
```html
services.AddRazorPages(o=> { o.Conventions.AllowAnonymousToPage("/Profesores/Listado"); });
```

> Permitir el acceso anonimo a cierta carpeta (antes bloqueda por AuthorizeFolder.
```html
services.AddRazorPages(o=> { o.Conventions.AllowAnonymousToFolder("/Profesores/Presenciales"); });
```

> Combinar 
```html
services.AddRazorPages(o=> { o.Conventions.AuthorizeFolder("/Profesores").AllowAnonymousToPage("/Profesores/Listado").AllowAnonymousToPage("/Profesores/Nuevo"); });
```
### Configurar el identity
```html
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});
```

## Sintaxis RAZOR
```html
<!-- Single statement block -->
@{ var myMessage = "Hello World"; }

<!-- Inline expression or variable -->
<p>The value of myMessage is: @myMessage</p>

<!-- Multi-statement block -->
@{
var greeting = "Welcome to our site!";
var weekDay = DateTime.Now.DayOfWeek;
var greetingMessage = greeting + " Here in Huston it is: " + weekDay;
}
<p>The greeting is: @greetingMessage</p>
```
## MVVM

No tenemos una carpeta llamada controllers, cada vista tiene una clase C# para procesar las peticiones http.
Tampoco tenemos views, si una carpeta pages.

Cada Page es un 99% parecida MVC, por ejemplo tenemos un @Page en la parte superior para indicar que es una razor pages

Index tiene una indexmodel que hereda de PageModel. 

<strong>OnGet</strong> procesa la peticion http get de nuestra pagina, puedo procesar o devolver una pagina. No hace falta indicarle que tiene que devolver la misma pagina, se sobre entiende.

<strong>OnPost</strong>
Vamos a enviar un parametro hasta nuestro modelo.

```html
<input name="valor1" type="text"/>
<input type="submit" />
```

```
OnPost(string valor1){

}
```
## Forms

Hay varias formas de utilizar formularios.
- Request Form
- Pasar por parametros
- Model Binding (Enlace de modelos)

### Request Form

En este enfoque tenemos los input html con un type determinado (text,number, etc) y cada uno de ellos tiene un <b>name</b>. Cuando hacemos el post recibimos la colección de elementos en Request.Form.
``` html
<form method="post">
    <input type="email" name="emailaddress"> 
    <input type="submit">
</form>
```
Puede acceder al valor en el método OnPost del controlador de la siguiente manera:
``` html
public void OnPost()
{
    var emailAddress = Request.Form["emailaddress"];
    // do something with emailAddress
}
``` 
### Pasar por parametros

Podemos pasar a un método onpost los distintos names de los controles. La limitación que tenemos es que el enlace se hace solo en el método onpost. 
Si queremos acceder a esos parametros los deberemos cargar en el nuevo método.

``` html
OnPost(int id, string clave)
```
### Binding properties

Podemos crear las distintas propiedades y utilizamos el atributo [BindProperty]. Si no queremos poner en cada prop un atributo, arriba de una clase podemos poner [BindProperties] 

``` html
[BindProperty]
public int id {get;set;}
[BindProperty]
public string clave {get;set;}
``` 
Esto nos permite hacer referencia a los names del formulario en cualquier parte y no hace falta pasarlo por parametros.
``` html
OnPost(){
 var numero=id;
 var pass=clave;
}
``` 

Si queremos pasar un objeto, en mvc hariamos
``` html
OnPost(Persona persona)
```
En razor pages no es necesario, tenemos que crear el metodo
 
```
OnPost( )

[BindProperty]
public Persona Persona {get;set;}
```

### Tag Helper
Los elementos form, input y son objetivos de los asistentes de selectetiquetas , componentes que amplían el elemento HTML para proporcionar atributos personalizados que se utilizan para controlar la generación de HTML.textarea

El atributo más importante es el atributo <b>asp-for</b> que toma el nombre de una propiedad de PageModel. Esto da como resultado que se genere el atributo correcto para que los valores del formulario se vinculen correctamente al modelo cuando el formulario se devuelva al servidor.

Etiquete de la siguiente manera:
```html
<input asp-for="EmailAddress" />
```
El HTML resultante es el siguiente:
```html
<input type="text" id="EmailAddress" name="EmailAddress" value="" />
```

Cambiamos los controles html comunes con *name* por los asp-for

Cuando el asp-for detecta que la bind property es string el type del input lo convierte en text. Si detecta que es un int lo convierte en type="number"

| .NET type | Input Type |
| --- | --- |
| Bool	| type="checkbox"| 
| String	| type="text"| 
| DateTime | 	type="datetime-local"| 
| Byte	| type="number"| 
| Int	| type="number"| 
| Single, Double	| type="number"| 
|IFormFile	| type="file"|

Si queremos customizar un poco el control debemos usar data annotations, por ej el para crear un tag de tipo password.

| .NET type | Input Type |
| --- | --- | 
|[EmailAddress] |	type="email"|
|[Url] |	type="url"|
|[HiddenInput] |	type="hidden"|
|[Phone] |	type="tel"|
|[DataType(DataType.Password)] |	type="password"|
|[DataType(DataType.Date)] |	type="date"|
|[DataType(DataType.Time)] |	type="time"|

## Handlers

Podemos tener varios OnPost() dentro del mismo modelo, por ejemplo OnPostAlumnoFormulario()

```html
<input name="valor1" type="text"/>
<input type="submit"  asp-page-handler="AlumnoFormulario" />
```

## Binding

Intercambio entre el modelo y la pagina.

Tambien tenemos un return RedirectToPage("Contact")

## ViewData

En el método OnGet:
```
ViewData["Materias"]
```
Como atributo:
```
[ViewData]
public string Materias { get; set; }
```
En razor:
```html
@page
@model IndexModel
@{
    ViewBag.Title = "My Home Page";
}
```
## Funciones
```html
    @{await MostrarImagen(item.MatrizId.ToString());}

@functions{


    async System.Threading.Tasks.Task MostrarImagen(string rutafoto) {
        if (String.IsNullOrEmpty(rutafoto))
        {
                <img src="~/images/icono-persona.png" class="rounded-circle" style="width:75px;height:75px;" />
        }
        else { 
                <img src="@Url.Content("~/images/"+ rutafoto)"  class="rounded-circle" style="width:75px;height:75px;" />
        }

    }

    void MostrarActivo(bool activo){
        if(activo){
            <span>SI</span>;
        }
        else{
            <span>NO</span>;
        }
      
    }
 

    void MostrarFechaNacimiento(DateTime dt){
        <span class="date">@dt.ToString("dd/MM/yyyy")   </span>;
    }
    void MostrarEstudioTipo(int tipo){
        var tipoestudio="";
        if(tipo==1){
            tipoestudio="Secundario";
        }
        else if(tipo==2){
            tipoestudio="Terciario";
        }
        else{
            tipoestudio="Universitario";
        }
        <span>@tipoestudio</span>;
    }
      void MostrarEstudioTipo2(int tipo){
         
        if(tipo==1){
            <span class="badge badge-danger">Secundario</span>;
        }
        else if(tipo==2){
                <span class="badge badge-warning">Terciario</span>;
        }
        else{
                <span class="badge badge-success">Universitario</span>;
        }
         
    }
}
```

## Validaciones

```html
using System.ComponentModel.DataAnnotations;

[Required]
[Range(1111111, 9999999)] 
[Required,MaxLength(20)] 
[Required]
[MaxLength(20)] 
[Required(ErrorMessage ="Debe ingresar la fecha de nacimiento")] 
[Range(1,3)]


@section scripts{
   <partial name="_ValidationScriptsPartial" />
}
<span asp-validation-for="Profesor.Legajo" class="badge bg-danger"></span>

 public IActionResult OnPost(){
      if (ModelState.IsValid)
      {
      }
 }


```

## Ruteo

La estructura de archivos determina la url que hay que poner para llegar a nuestra pagina.
En startup

Services.AddMvc().AddRazorPagesOptions(o=> o.Conventions.AddPageRoute("/About","acerca-de"));

Entonces http://localhost:5000/acerca-de  nos lleva a about

## Pasar una variable de una pagina a otra utilizando TempData, info primitiva (int, bool, string)

En el Index
```
[TempData]
public string Nombre {get;set;}

OnPost(string param1){
    Nombre=param1;
    return RedirectToPage("Contact")
}
```

En Contact tambien:
```
[TempData]
public string Nombre {get;set;}
```

```html
<span>@Model.Nombre</span>
```


## Pasar una variable de una pagina a otra por querystring

En contact
``` 
 @Page "{id:int}" o   @Page "{id:int?}"
<span>@Model.Mensaje</span>

 public void OnGet(int id){
     Mensaje="Mi mensaje es del id " + id.ToString();
 }
```

## Recibir mas parametros
``` 
 @Page "{id:int}/{pg:int}" 
<span>@Model.Mensaje</span>

 public void OnGet(int id,int pg){
     Mensaje="Mi mensaje es del id " + id.ToString();
     NroPagina=pg;
 }
```

## Ordenamiento

```html
    <a asp-page="./Index" asp-route-sortOrder="@Model.DateSort">
        @Html.DisplayNameFor(model => model.Students[0].EnrollmentDate)
    </a>
```

Y en el método recibe ese sortOrder

```
public OnGet(string sortOrder)
{

}
```


```html
    <a asp-page="./Index" asp-route-CampoOrden="@Model.LegajoOrden">
        @Html.DisplayNameFor(model => model.Profesores[0].Legajo)
    </a>

        public string CurrentSort { get; set; }

        public void OnGet(string CampoOrden)
        {
            CurrentSort = CampoOrden;
            //LegajoOrden = String.IsNullOrEmpty(CampoOrden) ? "Cust_ID" : "";
            LegajoOrden = CampoOrden == "Legajo_Asc_Sort" ? " Legajo_Desc_Sort" : "Legajo_Asc_Sort";
            Profesores = _profServicio.GetAll().ToList();
            switch (CampoOrden)
            {
                case "Cust_ID":
                    Profesores = Profesores.OrderByDescending(s => s.Id).ToList();
                    break;
                case " Legajo_Asc_Sort":
                    Profesores = Profesores.OrderBy(s => s.Legajo).ToList();
                    break;
                case " Legajo_Desc_Sort":
                    Profesores = Profesores.OrderByDescending(s => s.Legajo).ToList();
                    break;
                default:
                    //cust = cust.OrderBy(s => s.CustomerId);
                    break;
            }
        }
```


## Exportar sql

dotnet ef migrations script --output "script.sql" --context AppDbContext



## Inyeccion de dependencias

<ul>
<li>
 <strong>Singleton: </strong> significa que solo se creará una única instancia. Esa instancia se comparte entre todos los componentes que la requieren. Por lo tanto, se utiliza siempre la misma instancia.
</li>
<li>
 <strong>Scoped (ambito): </strong> significa que se crea una instancia una vez por ámbito. Se crea un alcance en cada solicitud a la aplicación, por lo que cualquier componente registrado como Con alcance se creará una vez por solicitud.
 </li>
 <li>
 <strong>Transient (transitorio):  </strong>Los servicios creados utilizando el tiempo de vida transitorio se crearán cada vez que se soliciten. Esta vida útil funciona mejor para servicios ligeros.
 </li>
</ul>
    
    > services.AddSingleton<Model.Profesor>();
    
    1. Registrarse como un servicio Scoped para que cada usuario diferente reciba su propia instancia del servicio durante la duración de su sesión. .
    2. Un Singleton permite compartir la misma instancia de una clase de servicio entre componentes. 
    3. Esto es bueno porque desea asegurarse de que todas sus páginas reciban la misma instancia de la clase de servicio Profesor. 
    4. Esto garantizará que los datos (es decir, Legajo, en este ejemplo) se compartan correctamente entre las páginas a lo largo de la vida útil de la aplicación.
    

    > services.AddScoped<Model.Profesor>();

<table>
<tr>
    <td>Service Type</td>
    <td>In the scope of same Http request	Different Http request
        Singleton	The same Instance of repository served	The same Instance of repository served</td>
</tr>
<tr>
    <td>Scoped	</td>
    <td>Same Instance	Different Instance</td>
</tr>
<tr>
    <td>Transient</td>
    <td>Every time New Instance	Every time New Instance</td>
</tr>
</table>

## Agregar iconos

```html
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.8.1/font/bootstrap-icons.css">
<i class="bi-github" role="img" aria-label="GitHub"></i>
```


# Links

### Environment 
https://www.learnrazorpages.com/razor-pages/tag-helpers/environment-tag-helper


https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/sort-filter-page?view=aspnetcore-6.0


https://www.learnrazorpages.com/razor-pages/routing


https://docs.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-6.0&tabs=visual-studio

https://www.mikesdotnetting.com/article/308/razor-pages-understanding-handler-methods

https://www.aspsnippets.com/Articles/Using-jQuery-AJAX-in-ASPNet-Core-Razor-Pages.aspx

https://www.aspsnippets.com/Articles/ASPNet-Core-Razor-Pages-Load-Partial-View-using-jQuery-AJAX.aspx

https://www.mikesdotnetting.com/article/349/razor-pages-and-bootstrap-modal-master-details



