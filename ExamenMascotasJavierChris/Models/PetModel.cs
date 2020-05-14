using ExamenMascotasJavierChris.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamenMascotasJavierChris.Models
{
    public class PetModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string ImageBase64 { get; set; }
        public DateTime FechaNac { get; set; }
        public string Genero { get; set; }
        public string Raza { get; set; }
        public float Peso { get; set; }
        public string Comentarios { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        [Ignore]
        public PetsViewModel parent { get; set; }
        [Ignore]
        public static PetsViewModel staticParent {get; set;}
        public PetModel() {
            parent = staticParent;
        }

    }
}
