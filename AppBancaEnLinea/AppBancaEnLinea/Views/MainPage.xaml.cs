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
        private ObservableCollection<Cuenta> listaCuenta = new ObservableCollection<Cuenta>(App.repositorioCuenta.ObtenerCuentas());
        
        public MainPage()
        {
            InitializeComponent();
            RefrescarPantalla();
        }

        private void RefrescarPantalla()
        {
            CuentasList.ItemsSource = App.repositorioCuenta.ObtenerCuentas();
        }

        private void Btn_RefrescarPantalla(object sender, EventArgs e)
        {
            CuentasList.ItemsSource = App.repositorioCuenta.ObtenerCuentas();
        }

        public void AgregarTapped(object sender, EventArgs e)
        {
            Application.Current.MainPage = new CuentaPage();
        }

        public void CerrarTapped(object sender, EventArgs e)
        {
            DisplayAlert("Cerrar Sesión", "Cerrando sesión.", "OK");
            Application.Current.MainPage = new LoginPage();
        }

        #region Deprecated
        [System.Obsolete("Metodo usado para insetar una cuenta ya establecida", false)]
        private void Btn_AgregarCuentaDefault(object sender, EventArgs e)
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
        #endregion
    }
}

