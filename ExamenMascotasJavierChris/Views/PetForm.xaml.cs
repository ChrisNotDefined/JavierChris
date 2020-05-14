using ExamenMascotasJavierChris.Models;
using ExamenMascotasJavierChris.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamenMascotasJavierChris.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PetForm : ContentPage
    {
        // 
        public PetForm()
        {
            InitializeComponent();
            BindingContext = new PetDetailViewModel();
        }

        public PetForm(PetModel petModel)
        {
            InitializeComponent();
            BindingContext = new PetDetailViewModel(petModel);
        }
    }
}