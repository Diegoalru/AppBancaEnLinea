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
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// Este almacenara la lista de cuentas.
        /// Con este tipo de datos, no se tiene que refrescar la app para que aparezcan los datos.
        /// </summary>
        ObservableCollection<Cuenta> listaCuenta = new ObservableCollection<Cuenta>(App.repositorioCuenta.ObtenerCuentas());

        public MainPage()
        {
            InitializeComponent();
            CargarCuentas();
        }

        public void CargarCuentas()
        {
            Cuenta cuenta = new Cuenta
            {
                CUE_CODIGO = 1,
                USU_CODIGO = 1,
                CUE_DESCRIPCION = "AHORRO",
                CUE_MONEDA = "COL",
                CUE_ESTADO = "A",
                CUE_SALDO = 1000000
            };

            App.repositorioCuenta.AgregarCuenta(cuenta);
            DisplayAlert("SQLite", App.repositorioCuenta.StatusMessage, "OK", "Cancel");
            CuentasList.ItemsSource = App.repositorioCuenta.ObtenerCuentas();
        }
    }
}