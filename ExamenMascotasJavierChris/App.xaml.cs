using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExamenMascotasJavierChris.Views;
using ExamenMascotasJavierChris.Data;

namespace ExamenMascotasJavierChris
{
    public partial class App : Application
    {
        static PetDatabase petDatabase;
        public static PetDatabase PetDatabase
        {
            get
            {
                if (petDatabase == null) petDatabase = new PetDatabase();
                return petDatabase;
            }
        }

        public static MasterDetailPage hamMenu;

        public App()
        {
            InitializeComponent();
            hamMenu = new HamburgerMenu();
            MainPage = hamMenu;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
