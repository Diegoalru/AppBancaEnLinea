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
        /// </summary>
        private ObservableCollection<Cuenta> listaCuenta = new ObservableCollection<Cuenta>(App.repositorioCuenta.ObtenerCuentas());
        
        public MainPage()
        {
            InitializeComponent();
            RefrescarPantalla();//Muestra los datos en la lista
            CargaUser();//Muestra en un label el Usuario.
            //CrearServicios();
        }

        public void CargaUser()
        {
            string user = App.repositorioUsuario.GetUsuario().USU_USERNAME;
            Lbl_Username.Text = "Usuario: " + user;
        }

        /// <summary>
        /// Actualiza los datos de la lista que se muestra en la pantalla.
        /// </summary>
        private void RefrescarPantalla()
        {
            try
            {
                List<Cuenta> dataList = new List<Cuenta>();
                foreach (var item in App.repositorioCuenta.ObtenerCuentas())
                {
                    if (item.USU_CODIGO == App.repositorioUsuario.GetUsuario().USU_CODIGO)
                    {
                        dataList.Add(item);
                    }
                }
                CuentasList.ItemsSource = dataList;
            }
            catch (Exception)
            {
                DisplayAlert("Cuenta", "Tu usuario no posee cuentas", "OK");
            }
        }

        #region Cuentas

        /// <summary>
        /// Metodo usado por el botón "Actualizar", que actualiza los datos de la lista que se muestra en la pantalla.
        /// </summary>
        private void Btn_RefrescarPantalla(object sender, EventArgs e)
        {
            RefrescarPantalla();
        }

        /// <summary>
        /// Metodo que nos lleva a otro formulario, donde podremos crear una nueva cuenta.
        /// </summary>
        public void AgregarTapped(object sender, EventArgs e)
        {
            Application.Current.MainPage = new CuentaPage();
        }

        /// <summary>
        /// Cierra la sesión actual.
        /// </summary>
        public async void CerrarSesionTapped(object sender, EventArgs e)
        {
            try
            {
                bool respuesta = await DisplayAlert("Cerrar Sesión", "¿Desea cerrar la sesión?", "OK", "Cancelar");
                if(respuesta)
                {
                    Application.Current.MainPage = new LoginPage();
                    App.repositorioUsuario.SetUser(null);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Elimina la cuenta seleccionada.
        /// </summary>
        public async void EliminarTapped(object sender, EventArgs e)
        {
            try
            {
                Cuenta cuenta = (Cuenta) CuentasList.SelectedItem;
                bool respuesta = await DisplayAlert("Advertencia", "Desea eliminar la cuenta " + cuenta.CUE_CODIGO + ".", "OK", "Cancelar");
                if (respuesta)
                {
                    App.repositorioCuenta.EliminarCuenta(cuenta);
                    await DisplayAlert("SQLite", App.repositorioCuenta.StatusMessage, "OK");
                    RefrescarPantalla();
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Advertencia", "Si desea eliminar una cuenta, debes seleccionarla primero.", "OK");
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void ModificarTapped(object sender, EventArgs e)
        {
            Cuenta cuenta = (Cuenta) CuentasList.SelectedItem;
            if(cuenta != null)
                Application.Current.MainPage = new CuentaPage(cuenta);
            else
                DisplayAlert("Advertencia", "Si desea modificar una cuenta, debes seleccionarla primero.", "OK");
        }
        #endregion

        #region Pagos

        /// <summary>
        /// Metodo que nos lleva a otro formulario, donde podremos pagar los servicios.
        /// </summary>
        public void PagarTapped(object sender, EventArgs e)
        {
            Application.Current.MainPage = new PagoPage();
        }
        #endregion

        #region Deprecated
        /// <summary>
        /// Agrega una cuenta por defecto.
        /// </summary>
        [System.Obsolete("Metodo usado para insetar una cuenta ya establecida", false)]
        private void Btn_AgregarCuentaDefault(object sender, EventArgs e)
        {
            Cuenta cuenta = new Cuenta
            {
                CUE_CODIGO = 1,
                USU_CODIGO = App.repositorioUsuario.GetUsuario().USU_CODIGO,
                CUE_DESCRIPCION = "AHORRO",
                CUE_MONEDA = "COL",
                CUE_ESTADO = "A",
                CUE_SALDO = 1000000
            };

            App.repositorioCuenta.AgregarCuenta(cuenta);
            DisplayAlert("SQLite", App.repositorioCuenta.StatusMessage, "OK");
            RefrescarPantalla();
        }

        /// <summary>
        /// Agrega varios servicios.
        /// </summary> 
        [System.Obsolete("Metodo usado para agregar servicios predeterminados.", false)]
        public void CrearServicios()
        {
            Servicio servicioAYA = new Servicio()
            {
                SER_DESCRIPCION = "AyA",
                SER_ESTADO = "A",
            };
            Servicio servicioCNFL = new Servicio()
            {
                SER_DESCRIPCION = "CNFL",
                SER_ESTADO = "A",
            };
            Servicio servicioNetflix = new Servicio()
            {
                SER_DESCRIPCION = "Netflix",
                SER_ESTADO = "A",
            };

            App.repositorioServicio.AgregarServicio(servicioAYA);
            App.repositorioServicio.AgregarServicio(servicioCNFL);
            App.repositorioServicio.AgregarServicio(servicioNetflix);
        }

        #endregion

        private void Btn_PagoList_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new PagoPageList();
        }
    }
}

