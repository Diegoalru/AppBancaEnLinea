using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppBancaEnLinea.Models;
using AppBancaEnLinea.Views;

namespace AppBancaEnLinea
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Txt_Username.Text = "diegoalru";
            Txt_Password.Text = "qwerty";
            //Txt_Username.Text = "";
            //Txt_Password.Text = "";

        }

        //Metodo de Login, ¡¡FUNCIONA!! pero ya no entiendo nada xD 
        private void Login(object sender, EventArgs e)
        {
            //Guardamos los campos
            string Username = Txt_Username.Text.ToUpper();
            string Password = Txt_Password.Text;

            //Verificamos el Formulario.
            if (VerificaForm())
            {
                //Validamos que los datos esten completos.
                bool ValidacionUsername = VerificaCampo(Txt_Username.Text);
                bool ValidacionPassword = VerificaCampo(Txt_Password.Text);

                //Mostramos mensaje de error para el dato incompleto.
                if (ValidacionUsername)
                {
                    DisplayAlert("Validación de ingreso.", "El campo de usuario no puede estar vacio.", "OK");
                }
                if (ValidacionPassword)
                {
                    DisplayAlert("Validación de ingreso.", "El campo de la contraseña no puede estar vacio.", "OK");
                }
                if(!(ValidacionUsername && ValidacionPassword))
                {
                    if(VerificaCredenciales(Username, Password))
                    {
                        Application.Current.MainPage = new MainPage();
                    }
                    else
                    {
                        DisplayAlert("Validación de ingreso.", "Credenciales Incorrectas.", "OK");
                    }
                }
            }
            else // En caso de error, se muestra un mensaje.
            {
                DisplayAlert("Validación de ingreso.", "Debes proporcionar los datos solicitados.", "OK");
            }
        }
        
        private bool VerificaCredenciales(string user, string pass)
        {
            if (BuscarUsuario(user, pass))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool BuscarUsuario(string username, string password)
        {
            try
            {
                bool existe = false;
                List<Usuario> data = App.repositorioUsuario.ObtenerUsuarios();
                foreach (var item in data)
                {
                    if (item.USU_USERNAME.Equals(username) && item.USU_PASSWORD.Equals(password))
                    {
                        App.repositorioUsuario.SetUser(new Usuario(item.USU_CODIGO, item.USU_USERNAME));
                        return true;
                    }
                    else
                    {
                        existe = false;
                    }
                }
                return existe;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Verifica que se encuentren los datos necesarios para iniciar sesión.
        /// </summary>
        /// <returns>True en caso de que esten todos los datos. False en caso de que falte algun dato.</returns>
        private bool VerificaForm()
        {
            string Username = Txt_Username.Text;
            string Password = Txt_Password.Text;

            if (Username.Equals("") && Password.Equals(""))
                return false;

            return true;
        }
        
        /// <summary>
        /// Verifica que el campo no este vacio o nulo.
        /// </summary>
        /// <param name="Campo">Cadena de texto que desea verificar.</param>
        /// <returns>True en caso de que este vacio o nulo. False en caso de tener información.</returns>
        private bool VerificaCampo(string Campo)
        {
            if(Campo.Equals(null) || Campo.Equals(""))
                return true;

            return false;
        }

        /// <summary>
        /// Carga la pagina de registro.
        /// </summary>
        private void Btn_Register_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new RegisterPage();
        }

        #region Deprecated
        [System.Obsolete("Metodo Obsoleto. Usarlo solo para agregar una cuenta predeterminada.", true)]
        private void Btn_Agregar_Clicked(object sender, EventArgs e)
        {
            AgregarUsuarioDefault();
        }

        /// <summary>
        /// Agrega un usuario por defecto.
        /// </summary>
        [System.Obsolete("Metodo usado para insetar un usuario ya establecida", false)]
        private void AgregarUsuarioDefault()
        {
            Usuario usuario = new Usuario
            {
                USU_CODIGO = 1,
                USU_NOMBRE = "Diego",
                USU_USERNAME = "diegoalru",
                USU_EMAIL = "diegoalru@gmail.com",
                USU_IDENTIFICACION = "123456789",
                USU_ESTADO = "A",
                USU_PASSWORD = "qwerty",
                USU_FEC_NAC = DateTime.Now
            };
            App.repositorioUsuario.AgregarUsuario(usuario);
            DisplayAlert("SQLite", App.repositorioUsuario.StatusMessage, "OK");
        }
        #endregion
    }
}