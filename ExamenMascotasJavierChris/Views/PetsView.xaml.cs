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
    public partial class PetsView : ContentPage
    {
        public PetsView()
        {
            InitializeComponent();
            BindingContext = new PetsViewModel();
        }
    }
}