using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyHerramientas.Models;
using ProyHerramientas.Servicios;

namespace ProyHerramientas.Pages.Profesores
{
    public class EditarModel : PageModel
    {
        private readonly IProfesorServicio _profService;
        //private readonly IMateriaServicio _mateService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public EditarModel(IProfesorServicio profServicio, IWebHostEnvironment hostingEnviroment)
        {
            //IMateriaServicio mateServicio, 
            _profService = profServicio;
            //_mateService = mateServicio;
            _hostingEnvironment = hostingEnviroment;
        }

        [BindProperty]
        public Profesor Profesor { get; set; }
        public void OnGet(int leg)
        {
            //var materias = _mateService.GetAll();
            //ViewData["Materias"] = new SelectList(materias, "Id", "Descripcion");
            //ViewData["MateriasDirecto"] = materias;


            Profesor = _profService.GetAll().Where(x => x.Legajo == leg).First();
        }

        public IActionResult OnPost()
        {
            /*
            if (profesor.Foto != null)
            {
                string carpetaFotos = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                string nombreArchivo = profesor.Foto.FileName;
                string rutaCompleta = Path.Combine(carpetaFotos, nombreArchivo);
                //subimos la imagen al servidor
                profesor.Foto.CopyTo(new FileStream(rutaCompleta, FileMode.Create));
                //guardar la ruta de la imagen en la base de datos
                profesor.FotoRuta = nombreArchivo;
            }*/
            _profService.Modificar(Profesor);
            //var profe2=profesor;
            return RedirectToPage("Index");
        }
    }
}
