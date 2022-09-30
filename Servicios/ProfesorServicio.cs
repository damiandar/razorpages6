using ProyHerramientas.Models;

namespace ProyHerramientas.Servicios
{
    public interface IProfesorServicio
    {

        IEnumerable<Profesor> GetAll();
        void Add(Profesor profesor);

        void Eliminar(Profesor profesor);
        void Modificar(Profesor profesor);
    }
    public class ProfesorServicio : IProfesorServicio
    {
        private List<Profesor> Profesores;
        public ProfesorServicio()
        {
            Profesores = new List<Profesor>(){
                new Profesor(){ Legajo=1111111, Nombre="Damian", Apellido="Rosso",   FechaNacimiento=new DateTime(1979,02,25),EstudioTipo=1,ADistancia=true,MateriaDictadaId=1,MateriaDictada=new Materia(1,"POO")},
                new Profesor(){ Legajo=2222222, Nombre="Florencia", Apellido="Diaz" ,FechaNacimiento=new DateTime(1979,02,25),EstudioTipo=2,ADistancia=false,MateriaDictadaId=1,MateriaDictada=new Materia(1,"POO")},
                new Profesor(){ Legajo=3333333, Nombre="Celeste", Apellido="Lopez",  FechaNacimiento=new DateTime(1984,04,05),EstudioTipo=1,ADistancia=true,MateriaDictadaId=2,MateriaDictada=new Materia(2,"WEB")},
                new Profesor(){ Legajo=4444444, Nombre="Laura", Apellido="Alonso",   FechaNacimiento=new DateTime(1986,10,16),EstudioTipo=3,ADistancia=false,MateriaDictadaId=3,MateriaDictada=new Materia(3,"Herramientas")},
                new Profesor(){ Legajo=5555555, Nombre="Lucas", Apellido="Castro",   FechaNacimiento=new DateTime(1979,02,25),EstudioTipo=1,ADistancia=true,MateriaDictadaId=4,MateriaDictada=new Materia(4,"BD")},
                new Profesor(){ Legajo=6666666, Nombre="Analia", Apellido="Martinez",FechaNacimiento=new DateTime(1979,02,25),EstudioTipo=2,ADistancia=true,MateriaDictadaId=4,MateriaDictada=new Materia(4,"BD")}
            };
        }
        public IEnumerable<Profesor> GetAll()
        {
            return Profesores;
        }
        public void Add(Profesor profesor)
        {
            //mobile.id = _mobileList.Max(e => e.id) + 1;
            //_mobileList.Add(mobile);
            //return mobile;
            Profesores.Add(profesor);
        }

        public void Eliminar(Profesor profesor)
        {
            var profesoranterior = Profesores.Where(x => x.Legajo == profesor.Legajo).First();
            Profesores.Remove(profesoranterior);
        }
        public void Modificar(Profesor profesor)
        {
            var profesoranterior = Profesores.Where(x => x.Legajo == profesor.Legajo).First();
            Profesores.Remove(profesoranterior);
            Profesores.Add(profesor);
        }
    }
}
