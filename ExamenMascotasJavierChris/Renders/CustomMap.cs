using ExamenMascotasJavierChris.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace ExamenMascotasJavierChris.Renders
{
    public class CustomMap : Map
    {
        public PetModel Pet;
        public List<PetModel> PetList;
    }
}