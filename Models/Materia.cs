using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyHerramientas.Models
{
  
    public class Materia
    {
        public Materia(){

        }        
        public Materia(int Id,string Descripcion){
            this.Id=Id;
            this.Descripcion=Descripcion;
        }
        public int Id {get;set;}
        public string Descripcion{get;set;}
    } 
}