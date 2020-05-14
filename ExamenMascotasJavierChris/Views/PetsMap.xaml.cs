using ExamenMascotasJavierChris.Models;
using ExamenMascotasJavierChris.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ExamenMascotasJavierChris.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PetsMap : ContentPage
    {
        public PetsMap(List<PetModel> Pets)
        {
            InitializeComponent();
            MapPet.Pet = null;
            MapPet.PetList = Pets;
            if (Pets.Count != 0)
            {
                double mediaLat = 0;
                double mediaLon = 0;
                foreach (var item in Pets)
                {
                    item.ImageBase64 = new ImageService().SaveImageFromBase64(item.ImageBase64, item.ID);
                    MapPet.Pins.Add(
                        new Pin()
                        {
                            Type = PinType.Place,
                            Label = item.Nombre,
                            Position = new Position(item.Latitud, item.Longitud)
                        });
                    mediaLat += item.Latitud;
                    mediaLon += item.Longitud;
                }

                mediaLat /= Pets.Count;
                mediaLon /= Pets.Count;

                MapPet.MoveToRegion(
                                MapSpan.FromCenterAndRadius(
                                    new Position(mediaLat, mediaLon), Distance.FromKilometers(10)
                            ));
            } else
            {
                MapPet.MoveToRegion(
                                MapSpan.FromCenterAndRadius(
                                    new Position(0, 0), Distance.FromKilometers(10)
                            ));
            }

        }

        public PetsMap(PetModel petSelected)
        {
            InitializeComponent();
            MapPet.PetList = null;
            petSelected.ImageBase64 = new ImageService().SaveImageFromBase64(petSelected.ImageBase64, petSelected.ID);
            MapPet.Pet = petSelected;

            MapPet.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(petSelected.Latitud, petSelected.Longitud), Distance.FromKilometers(10)
            ));

            MapPet.Pins.Add(
                new Pin
                {
                    Type = PinType.Place,
                    Label = petSelected.Nombre,
                    Position = new Position(petSelected.Latitud, petSelected.Longitud)
                }
            );
        }
    }
}