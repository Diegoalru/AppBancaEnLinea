using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppBancaEnLinea.Models;
using AppBancaEnLinea.Data;

namespace AppBancaEnLinea.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CuentaPage : ContentPage
    {
        public CuentaPage()
        {
            InitializeComponent();
            Txt_Codigo.IsVisible = false;
            Btn_Modificar.IsVisible = false;
        }

        public CuentaPage(Cuenta cuenta)
        {
            InitializeComponent();
            Btn_Agregar.IsVisible = false;
            Txt_Codigo.IsReadOnly = true;
            Txt_Codigo.Text = cuenta.CUE_CODIGO.ToString();
            Txt_Descripcion.Text = cuenta.CUE_DESCRIPCION.ToString();
            Txt_Saldo.Text = cuenta.CUE_SALDO.ToString();
            Pkr_Estado.SelectedItem = (cuenta.CUE_ESTADO.Equals("A") ? "Activo" : "Inactivo");
            string moneda;
            switch (cuenta.CUE_MONEDA)
            {
                case "DOL":
                    moneda = "DOLARES";
                    break;
                case "COL":
                    moneda = "COLONES";
                    break;
                default:
                    moneda = "EUROS";
                    break;
            }
            Pkr_Moneda.SelectedItem = moneda;
        }

        private void AgregarTapped(Object sender, System.EventArgs e)
        {
            string moneda = string.Empty;
            switch(Pkr_Moneda.SelectedItem.ToString())
            {
                case "DOLARES":
                    moneda = "DOL";
                    break;
                case "COLONES":
                    moneda = "COL";
                    break;
                default:
                    moneda = "EUR";
                    break;
            }

            Cuenta cuenta = new Cuenta
            {
                USU_CODIGO = 1,
                CUE_CODIGO = 1,
                CUE_DESCRIPCION = Txt_Descripcion.Text,
                CUE_MONEDA = moneda,
                CUE_SALDO = Convert.ToDecimal(Txt_Saldo.Text),
                CUE_ESTADO = Pkr_Estado.SelectedItem.ToString().Substring(0, 1)
            };

            App.repositorioCuenta.AgregarCuenta(cuenta);
            DisplayAlert("SQLite", App.repositorioCuenta.StatusMessage, "OK", "Cancel");
        }

        private void ActualizarTapped(Object sender, System.EventArgs e)
        {
            string moneda = string.Empty;
            switch (Pkr_Moneda.SelectedItem.ToString())
            {
                case "DOLARES":
                    moneda = "DOL";
                    break;
                case "COLONES":
                    moneda = "COL";
                    break;
                default:
                    moneda = "EUR";
                    break;
            }

            Cuenta cuenta = new Cuenta
            {
                USU_CODIGO = 1,
                CUE_CODIGO = Convert.ToInt32(Txt_Codigo.Text),
                CUE_DESCRIPCION = Txt_Descripcion.Text,
                CUE_MONEDA = moneda,
                CUE_SALDO = Convert.ToDecimal(Txt_Saldo.Text),
                CUE_ESTADO = Pkr_Estado.SelectedItem.ToString().Substring(0, 1)
            };

            App.repositorioCuenta.ActualizaCuenta(cuenta);
            DisplayAlert("SQLite", App.repositorioCuenta.StatusMessage, "OK", "Cancel");
        }


        private void RegresarTapped(Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }


    }
}