using ExamenMascotasJavierChris.Models;
using ExamenMascotasJavierChris.Services;
using ExamenMascotasJavierChris.Views;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExamenMascotasJavierChris.ViewModels
{
    public class PetDetailViewModel : BaseViewModel
    {
        Command saveCommand;
        public Command SaveCommand => saveCommand ?? (saveCommand = new Command(SaveAction));

        Command mapCommand;
        public Command MapCommand => mapCommand ?? (mapCommand = new Command(MapAction));

        Command _TakePictureCommand;
        public Command TakePictureCommand => _TakePictureCommand ?? (_TakePictureCommand = new Command(TakePictureAction));

        Command _SelectPictureCommand;
        public Command SelectPictureCommand => _SelectPictureCommand ?? (_SelectPictureCommand = new Command(SelectPictureAction));

        PetModel petSelected;
        public PetModel PetSelected
        {
            get => petSelected;
            set => SetProperty(ref petSelected, value);
        }

        public PetDetailViewModel()
        {
            PetSelected = new PetModel();
        }

        public PetDetailViewModel(PetModel petModel)
        {
            PetSelected = petModel;
        }

        private async void SaveAction()
        {
            var maxLat = 21.182691;
            var minLat = 21.038316;

            var maxLon = -101.574025;
            var minLon = -101.727908;

            PetSelected.Latitud = GetRandomCoord(minLat, maxLat);
            PetSelected.Longitud = GetRandomCoord(minLon, maxLon);
            await App.PetDatabase.SavePetAsync(PetSelected);
            App.hamMenu.Detail = new NavigationPage(new PetsView());
        }

        public double GetRandomCoord(double minCoord, double maxCoord)
        {
            return new Random().NextDouble() * (maxCoord - minCoord) + minCoord;
        }

        public void MapAction()
        {
            App.hamMenu.Detail.Navigation.PushAsync(new PetsMap(new PetModel
            {
                Nombre = PetSelected.Nombre,
                ImageBase64 = PetSelected.ImageBase64,
                Latitud = PetSelected.Latitud,
                Longitud = PetSelected.Longitud
            }));
        }

        private async void SelectPictureAction()
        {

            if (Device.RuntimePlatform == Device.UWP)
            {
                await CrossMedia.Current.Initialize();
            }

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("ERROR", ":( Seleccionar fotografia no esta disponible.", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });

            if (file == null)
                return;

            PetSelected.ImageBase64 = new ImageService().ConvertImageSourceToBase64(file.Path);

            //await Application.Current.MainPage.DisplayAlert("File Location", file.Path, "OK");
        }

        private async void TakePictureAction()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                await CrossMedia.Current.Initialize();
            }

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
            });

            if (file == null)
                return;

            PetSelected.ImageBase64 = new ImageService().ConvertImageSourceToBase64(file.Path);
          

            //await Application.Current.MainPage.DisplayAlert("File Location", file.Path, "OK");
        }
    }
}
