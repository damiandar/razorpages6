using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations; 

namespace ProyHerramientas.Pages
{
    [BindProperties]
    public class EditarModel : PageModel
    {
        
        public int id {get;set;}
        [DataType("Password")]
        public string clave{get;set;}
        public bool recibido {get;set;}
        [DataType("EmailAddress")]
        public string mail {get;set;}
        public IFormFile archivo {get;set;}
        public void OnGet()
        {
            id=9999;
        }

        public void OnPost(){
            var numeroid=id;
            var pass=clave;
        }
    }
}
