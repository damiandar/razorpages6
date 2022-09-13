using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyHerramientas.Models;
using ProyHerramientas.Servicios;

namespace ProyHerramientas.Pages.Profesores
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Profesor> Profesores { get; set; }
        private IProfesorServicio _profServicio;

        public IndexModel(IProfesorServicio profServicio){
                _profServicio = profServicio;            
        } 
        public void OnGet()
        {
            Profesores = _profServicio.GetAll().ToList();
        }

        public void OnGetDelete(int legajoborrar)
        {
            var ProfesorBuscado = _profServicio.GetAll().Where(x => x.Legajo == legajoborrar).First();
            _profServicio.Eliminar(ProfesorBuscado);
            Profesores = _profServicio.GetAll().ToList();
        }

        public void OnPostFiltrar(int LegajoFiltro)
        {
            Profesores = _profServicio.GetAll().Where(x => x.Legajo == LegajoFiltro).ToList();
        }
    }
}
