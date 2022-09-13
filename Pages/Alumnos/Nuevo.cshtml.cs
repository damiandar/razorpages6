using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProyHerramientas.Pages.Alumnos
{
    public class NuevoModel : PageModel
    {
        public int TotalAlumnos=20;
        public string NombreProfesor {get;set;}
        public void OnGet()
        {
            ViewData["instituto"]="ISTEA"; 
            NombreProfesor="Laura Alonso";
        }
        public void OnPost(){
            NombreProfesor="Marina Lopez";
            //var formulario=Request.Form; 
            var numeroid= Request.Form["id"];
            var fecha=Request.Form["fechanacimiento"];
            var pass=Request.Form["clave"];
            var recibi=Request.Form["recibido"];
            var opcion=Request.Form["opcion"];
            var mail=Request.Form["mail"];
            var archivo=Request.Form["archivo"];
            var rango=Request.Form["rango"];

        }
        public void OnPostModificar(){
            NombreProfesor="Nicolas Fernandez";
        }

        public IActionResult OnPostVerificar(){
            return Page();
            //return RedirectToPage("Index");
        }
    }
}
