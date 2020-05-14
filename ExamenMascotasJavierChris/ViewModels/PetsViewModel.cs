using ExamenMascotasJavierChris.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ExamenMascotasJavierChris.Data;
using System.Text;
using Xamarin.Forms;
using ExamenMascotasJavierChris.Views;
using System.Threading.Tasks;

namespace ExamenMascotasJavierChris.ViewModels
{
    public class PetsViewModel : BaseViewModel
    {
        Command editCommand;
        public Command EditCommand => editCommand ?? (editCommand = new Command(EditAction));
        Command deletePetCommand;
        public Command DeleteCommand => deletePetCommand ?? (deletePetCommand = new Command(DeletePet));
        Command petTappedCommand;
        public Command PetTappedCommand => petTappedCommand ?? (petTappedCommand = new Command(petTappedAction));
        List<PetModel> pets;
        public List<PetModel> Pets
        {
            get => pets;
            set => SetProperty(ref pets, value);
        }


        public PetsViewModel()
        {
            PetModel.staticParent = this;


            getAllPets();
        }

        public async void getAllPets()
        {
            Pets = await App.PetDatabase.GetAllPetsAsync();
        }

        private async void petTappedAction(object obj)
        {
            var itemsFrame = obj as Frame;
            var myLabel = itemsFrame.FindByName<Label>("ageLabel");
            var buttons = itemsFrame.FindByName<FlexLayout>("buttonStack");

            await Task.WhenAll(
                itemsFrame.FadeTo(0.5, 100),
                itemsFrame.FadeTo(1, 100),
                myLabel.FadeTo(0, 200),
                buttons.FadeTo(0, 200)
                );

            myLabel.IsVisible = !myLabel.IsVisible;
            buttons.IsVisible = !buttons.IsVisible;
            await myLabel.FadeTo(1, 100);
            await buttons.FadeTo(1, 100);


            //await Application.Current.MainPage.DisplayAlert("Debug", itemsFrame.ClassId, "OK");
            // Reemplazar por get element by ID
            //PetModel pet = Pets[int.Parse(item.ClassId) - 1];

            //App.hamMenu.Detail = new NavigationPage(new PetForm(pet));
        }

        public async void DeletePet(object obj)
        {
            int.TryParse(obj.ToString(), out int idValue);
            bool proceed = await Application.Current.MainPage.DisplayAlert("Borrar", "¿Desea borrar a la mascota?", "Si", "No");
            if (proceed)
            {
                List<PetModel> petList = await App.PetDatabase.GetPetByID(idValue);
                PetModel pet = petList[0];
                await App.PetDatabase.DeletePetAsync(pet);
                getAllPets();
            }
        }

        public async void EditAction(object obj)
        {
            int.TryParse(obj.ToString(), out int idValue);
            List<PetModel> petList = await App.PetDatabase.GetPetByID(idValue);
            PetModel pet = petList[0];

            App.hamMenu.Detail = new NavigationPage(new PetForm(pet));
        }
    }
}
