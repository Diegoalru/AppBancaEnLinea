using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppBancaEnLinea.Data;
using AppBancaEnLinea.Models;

namespace AppBancaEnLinea.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PagoPage : ContentPage
	{
        #region Variables
        private List<Cuenta> cuentas = new List<Cuenta>();
        private List<Servicio> servicios = new List<Servicio>();
        #endregion

        public PagoPage()
        {
            InitializeComponent();
            Lbl_Date.Text = "Fecha Actual: " + DateTime.Now.Date.ToShortDateString();
            CargarDatos();
        }

        public void CargarDatos()
        {
            //Lista de los servicios.
            foreach (var item in App.repositorioServicio.ObtenerServicios())
            {
                servicios.Add(item);
            }
            Pkr_Servicios.ItemsSource = servicios;


            //Lista de las cuentas del usuario.
            foreach (var item in App.repositorioCuenta.ObtenerCuentas())
            {
                if (item.USU_CODIGO == App.repositorioUsuario.GetUsuario().USU_CODIGO)
                {
                    cuentas.Add(item);
                }
            }
            Pkr_Cuentas.ItemsSource = cuentas;
        }

        /// <summary>
        /// Verifica que el campo no este vacio o nulo.
        /// </summary>
        /// <param name="Campo">Cadena de texto que desea verificar.</param>
        /// <returns>True en caso de que este vacio o nulo. False en caso de tener información.</returns>
        private bool VerificaCampo(string Campo)
        {
            if (Campo == null || Campo.Equals(""))
                return true;

            return false;
        }

        private void Btn_Volver_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }

        private async void Btn_Pagar_Clicked(object sender, EventArgs e)
        {
            try
            {
                int indexCuenta = Pkr_Cuentas.SelectedIndex;
                int indexServicio = Pkr_Servicios.SelectedIndex;
                bool BCuenta = true;
                bool BServicio = true;

                if(indexCuenta == -1)
                {
                    BCuenta = false;
                    await DisplayAlert("Alerta", "Debes seleccionar una cuenta.", "OK");
                }

                if(indexServicio == -1)
                {
                    BServicio = false;
                    await DisplayAlert("Alerta", "Debes seleccionar un servicio.", "OK");
                }

                if (BCuenta && BServicio)
                {
                    if (!VerificaCampo(Txt_Monto.Text))
                    {
                        decimal monto = Convert.ToDecimal(Txt_Monto.Text);
                        decimal montoFinal = cuentas[indexCuenta].CUE_SALDO - monto;
                        if (montoFinal >= 0)
                        {
                            Pago pago = new Pago()
                            {
                                PAG_CODIGO = 1,
                                CUE_CODIGO = cuentas[indexCuenta].CUE_CODIGO,
                                SER_CODIGO = servicios[indexServicio].SER_CODIGO,
                                PAG_MONEDA = cuentas[indexCuenta].CUE_MONEDA,
                                PAG_MONTO = monto,
                                PAG_FECHA = DateTime.Now
                            };
                            string signo = cuentas[indexCuenta].CUE_MONEDA.Trim().Equals("DOL") ? "$" : cuentas[indexCuenta].CUE_MONEDA.Trim().Equals("COL") ? "₡" : "€";
                            string msj = string.Format("Comprobación:\nServicio: {0}\nCuenta: {1}\nMonto: {2}{3}\nSaldo despues del pago: {4}", servicios[indexServicio].SER_DESCRIPCION, pago.CUE_CODIGO, signo, pago.PAG_MONTO, montoFinal);

                            bool respuesta = await DisplayAlert("Comprobación:", msj, "OK", "Cancelar");
                            if (respuesta)
                            {
                                Cuenta cuenta = cuentas[indexCuenta];
                                cuenta.CUE_SALDO -= monto;
                                App.repositorioCuenta.ActualizaCuenta(cuenta);
                                App.repositorioPago.AgregarPago(pago);
                                await DisplayAlert("Pago", App.repositorioPago.StatusMessage, "OK");
                                Limpiar();
                            }
                        }
                        else
                        {
                            await DisplayAlert("Error", "Dinero insuficiente.\n", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Alerta", "Debes indicar el monto a pagar.", "OK");
                    }
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Error al crear pago.", "OK");
            }
        }

        private void Limpiar()
        {
            Pkr_Servicios.SelectedItem = 0;
            Pkr_Cuentas.SelectedItem = 0;
            Txt_Monto.Text = "";
        }
    }
}