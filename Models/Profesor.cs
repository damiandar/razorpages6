using System.ComponentModel.DataAnnotations;

namespace ProyHerramientas.Models
{
    public class Profesor
    {
        public int Id { get; set; }
        public int Legajo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public bool ADistancia {get; set; }

        public int DNI {get;set;}

        public Pais PaisNacimiento {get; set; }
        public DateTime FechaNacimiento { get; set; }
        [Range(1, 3)]
        public int EstudioTipo { get; set; }

    }
    public class Pais { 
        public int Id  {get; set;}
        public string Descripcion { get; set; } 
    }


}
