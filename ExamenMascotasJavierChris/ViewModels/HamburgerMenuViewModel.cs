using ExamenMascotasJavierChris.Models;
using ExamenMascotasJavierChris.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamenMascotasJavierChris.ViewModels
{
    public class HamburgerMenuViewModel : BaseViewModel
    {
        MasterDetailPage masterDetailPage;

        Command selectedCommand;
        public Command SelectedCommand => selectedCommand ?? (selectedCommand = new Command(SelectedAction));

        private async void SelectedAction(object sender)
        {
            var frame = sender as Frame;
            await frame.FadeTo(0.5, 150);
            await frame.FadeTo(1, 50);
            string name = frame.ClassId;

            ContentPage nextpage;

            switch (name)
            {
                case "Pets":
                    nextpage = new PetsView();
                    break;
                case "New":
                    nextpage = new PetForm();
                    break;
                case "Map":
                    List<PetModel> pets = await App.PetDatabase.GetAllPetsAsync();
                    nextpage = new PetsMap(pets);
                    break;
                default:
                    nextpage = new PetForm();
                    break;
            }

            masterDetailPage.IsPresented = false;
            masterDetailPage.Detail = new NavigationPage(nextpage);
        }

        public HamburgerMenuViewModel(MasterDetailPage masterDetailPage)
        {
            this.masterDetailPage = masterDetailPage;
        }
    }
}
