using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyHerramientas.Models;
using ProyHerramientas.Servicios;

namespace ProyHerramientas.Pages.Profesores
{
    public class EditarModel : PageModel
    {
        public List<SelectListItem> MateriasLista { get; set; }
        private readonly IProfesorServicio _profService;
        private readonly IMateriaServicio _mateService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public EditarModel(IProfesorServicio profServicio,IMateriaServicio mateServicio, IWebHostEnvironment hostingEnviroment)
        {
            _profService = profServicio;
            _mateService = mateServicio;
            _hostingEnvironment = hostingEnviroment;
        }

        [BindProperty]
        public Profesor Profesor { get; set; }
        public void OnGet(int leg)
        {
            MateriasLista= _mateService.GetAll().Select(a => 
                                  new SelectListItem 
                                  {
                                      Value = a.Id.ToString(),
                                      Text =  a.Descripcion
                                  }).ToList();

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
            var materia=_mateService.GetAll().Where(x=>x.Id==Profesor.MateriaDictadaId).First();
            Profesor.MateriaDictada=materia;
            _profService.Modificar(Profesor);
            //var profe2=profesor;
            return RedirectToPage("Index");
        }
    }
}
