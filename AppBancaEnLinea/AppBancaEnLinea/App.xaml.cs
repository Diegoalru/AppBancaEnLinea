using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppBancaEnLinea.Data;

namespace AppBancaEnLinea
{
    public partial class App : Application
    {
        public static RepositorioCuenta repositorioCuenta { get; private set; }
        public static RepositorioPago repositorioPago { get; private set; }
        public static RepositorioServicio repositorioServicio { get; private set; }
        public static RepositorioUsuario repositorioUsuario { get; private set; }


        public App()
        {
            InitializeComponent();
        }

        public App(string ruta)
        {
            InitializeComponent();

            repositorioCuenta = new RepositorioCuenta(ruta);
            repositorioPago = new RepositorioPago(ruta);
            repositorioServicio = new RepositorioServicio(ruta);
            repositorioUsuario = new RepositorioUsuario(ruta);

        }

        protected override void OnStart()
        {
            // Handle when your app starts
            MainPage = new LoginPage();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// Metodo que devuelve una pantalla, al volver a iniciar el programa.
        /// </summary>
        protected override void OnResume()
        {
            MainPage = new Views.TestPage();
        }
    }
}
