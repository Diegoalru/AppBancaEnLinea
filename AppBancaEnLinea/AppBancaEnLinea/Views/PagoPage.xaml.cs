using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppBancaEnLinea.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PagoPage : ContentPage
	{
        public PagoPage()
        {
            InitializeComponent();

        }

        public void CargarDatos()
        {
            Pkr_Cuentas.ItemsSource = App.repositorioCuenta.ObtenerCuentas();
            Pkr_Servicios.ItemsSource = App.repositorioServicio.ObtenerServicios();
        }

        private void Btn_Volver_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}