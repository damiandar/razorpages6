using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyHerramientas.Models;
 

namespace ProyHerramientas.Servicios
{
    public interface IMateriaServicio
    {
  
        public IEnumerable<Materia> GetAll();   
        public void Add(Materia materia);
        void Eliminar(Materia materia);
        void Modificar(Materia materia);
    }
    public class MateriaServicio : IMateriaServicio
    {
        private List<Materia> Materias;
        public MateriaServicio(){
          Materias=new List<Materia>(){
                new Materia(){ Id=1,Descripcion="POO"},
                new Materia(){ Id=2,Descripcion="WEB"},
                new Materia(){ Id=3,Descripcion="Herramientas"},
                new Materia(){ Id=4,Descripcion="BD"},
            };
        }
        public IEnumerable<Materia> GetAll(){
            return Materias;
        }  
        public void Add(Materia materia){
             //mobile.id = _mobileList.Max(e => e.id) + 1;
            //_mobileList.Add(mobile);
            //return mobile;
            Materias.Add(materia);
        }

        public void Eliminar(Materia materia){
            var materiaanterior=Materias.Where(x=> x.Id==materia.Id).First();
            Materias.Remove(materiaanterior); 
        }
         public void Modificar(Materia materia){
            var materiaanterior=Materias.Where(x=> x.Id==materia.Id).First();
            Materias.Remove(materiaanterior);
            Materias.Add(materia);
        }
    }
}
