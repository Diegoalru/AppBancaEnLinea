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
                CUE_DESCRIPCION = Txt_Descripcion.Text,
                CUE_MONEDA = moneda,
                CUE_SALDO = Convert.ToDecimal(Txt_Saldo.Text),
                CUE_ESTADO = Pkr_Estado.SelectedItem.ToString().Substring(0, 1)
            };

            App.repositorioCuenta.AgregarCuenta(cuenta);
            DisplayAlert("SQLite", App.repositorioCuenta.StatusMessage, "OK", "Cancel");
        }

        public void RegresarTapped(Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }


    }
}