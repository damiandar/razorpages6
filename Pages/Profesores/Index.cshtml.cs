using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyHerramientas.Models;
using ProyHerramientas.Servicios;

namespace ProyHerramientas.Pages.Profesores
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string MensajeExitoso{get;set;}
        [BindProperty]
        public List<Profesor> Profesores { get; set; }
        private IProfesorServicio _profServicio;
        public string LegajoOrden;
        public string CurrentSort { get; set; }
        public IndexModel(IProfesorServicio profServicio){
                _profServicio = profServicio;            
        } 
        public void OnGet(string CampoOrden,int LegajoFiltro,string ApellidoFiltro)
        {
            CurrentSort = CampoOrden;
            //LegajoOrden = String.IsNullOrEmpty(CampoOrden) ? "Cust_ID" : "";
            LegajoOrden = (CampoOrden == "Legajo_Asc_Sort") ? "Legajo_Desc_Sort" : "Legajo_Asc_Sort";
            Profesores = _profServicio.GetAll().ToList();
            if(LegajoFiltro !=null && LegajoFiltro>0)
                Profesores = Profesores.Where(x => x.Legajo == LegajoFiltro).ToList();
            if (ApellidoFiltro != null && ApellidoFiltro.Length > 0) {
                Profesores = Profesores.Where(x => x.Apellido.ToUpper().Contains(ApellidoFiltro.ToUpper())).ToList();
            }

            switch (CampoOrden)
            {
                case "Cust_ID":
                    Profesores = Profesores.OrderByDescending(s => s.Id).ToList();
                    break;
                case "Legajo_Asc_Sort":
                    Profesores = Profesores.OrderBy(s => s.Legajo).ToList();
                    break;
                case "Legajo_Desc_Sort":
                    Profesores = Profesores.OrderByDescending(s => s.Legajo).ToList();
                    break;
                case "OrdenPorApellidoAsc":
                    Profesores = Profesores.OrderBy(s => s.Apellido).ToList();
                    break;
                case "OrdenPorApellidoDesc":
                    Profesores = Profesores.OrderByDescending(s => s.Apellido).ToList();
                    break;
                default:
                    //cust = cust.OrderBy(s => s.CustomerId);
                    break;
            }
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
