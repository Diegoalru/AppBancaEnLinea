using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppBancaEnLinea.Data;
using AppBancaEnLinea.Models;

namespace AppBancaEnLinea.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PagoPageList : ContentPage
	{
        private ObservableCollection<Pago> listaPago = new ObservableCollection<Pago>();

        public PagoPageList ()
		{
			InitializeComponent ();
            CargaDatos();
		}

        private void CargaDatos()
        {
            //Obtenermos la lista de las cuentas del usuario.
            foreach (var cuenta in App.repositorioCuenta.ObtenerCuentas())
            {
                if(cuenta.USU_CODIGO == App.repositorioUsuario.GetUsuario().USU_CODIGO)
                {
                    //Obtenemos los pagos del usuario.
                    foreach (var pago in App.repositorioPago.ObtenerPagos())
                    {
                        if(pago.CUE_CODIGO == cuenta.CUE_CODIGO)
                        {
                            listaPago.Add(pago);
                        }
                    }
                }
            }
            PagoList.ItemsSource = listaPago;
        }

        private void Btn_Regresar_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}