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
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage ()
		{
			InitializeComponent ();
		}

        #region Valida Form
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

        /// <summary>
        /// Valida que el email, contenga los simbolos '@' y '.'
        /// </summary>
        /// <param name="Mail">Email del usuario.</param>
        /// <returns>True en caso que contenga los simbolos "@" y "." .</returns>
        public bool ValidaMail(string Mail)
        {
            if (Mail.Contains("@") && Mail.Contains("."))
                return true;
            return false;
        }
        
        /// <summary>
        /// Verifica que el formulario este completo.
        /// </summary>
        /// <returns>Retorna true en caso de que el formulario este completo.</returns>
        public bool VerificaForm()
        {
            bool completo = true;
            var datos = new List<string>();
            datos.Add(Txt_Identificacion.Text);
            datos.Add(Txt_Nombre.Text);
            datos.Add(Txt_Username.Text);
            datos.Add(Txt_Password.Text);
            datos.Add(Txt_PasswordConfirm.Text);
            datos.Add(Txt_Email.Text);

            for (int i = 0; i <= (datos.Count - 1); i++)
            {
                if (VerificaCampo(datos[i]))
                    completo = false;
            }
            return completo;
        }
        #endregion

        #region Metodos para verificar que no exista el usuario o sus datos.
        public int ExisteUsername(string username)
        {
            int result = 0;
            try
            {
                foreach (var item in App.repositorioUsuario.ObtenerUsuarios())
                {
                    if (item.USU_USERNAME.Equals(username))
                    {
                        result = 1;
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public int ExisteEmail(string email)
        {
            int result = 0;
            try
            {
                foreach (var item in App.repositorioUsuario.ObtenerUsuarios())
                {
                    if (item.USU_EMAIL.Equals(email))
                    {
                        result = 1;
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public int ExisteID(string id)
        {
            int result = 0;
            try
            {
                foreach (var item in App.repositorioUsuario.ObtenerUsuarios())
                {
                    if (item.USU_IDENTIFICACION.Equals(id))
                    {
                        result = 1;
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        #endregion

        #region Botones
        /// <summary>
        /// Botón para registrar el usuario.
        /// </summary>
        private async void Btn_Register_Clicked(object sender, EventArgs e)
        {
            if (VerificaForm())
            {
                string Pass1 = Txt_Password.Text;
                string Pass2 = Txt_PasswordConfirm.Text;
                if (Pass1.Equals(Pass2))
                {
                    string id = Txt_Identificacion.Text;
                    if (id.Length == 9)
                    {
                        string mail = Txt_Email.Text;
                        string username = Txt_Username.Text.ToUpper();
                        if (ValidaMail(mail))
                        {
                            int IID = ExisteID(id);
                            int IUser = ExisteUsername(username);
                            int IMail = ExisteEmail(mail);
                            if (IID != 0)
                            {
                                await DisplayAlert("Advertencia", "La cedula " + id + " ya existe.", "Ok");
                            }
                            if (IUser != 0)
                            {
                                await DisplayAlert("Advertencia", "El usuario " + username + " ya existe.", "Ok");
                            }
                            if (IMail != 0)
                            {
                                await DisplayAlert("Advertencia", "El correo " + mail + " ya existe.", "Ok");
                            }
                            if ((IID == 0) && (IUser == 0) && (IMail == 0))
                            {
                                Usuario usuario = new Usuario()
                                {
                                    USU_CODIGO = 1,
                                    USU_IDENTIFICACION = id,
                                    USU_NOMBRE = Txt_Nombre.Text,
                                    USU_EMAIL = mail,
                                    USU_ESTADO = "A",
                                    USU_PASSWORD = Pass1,
                                    USU_USERNAME = username,
                                    USU_FEC_NAC = Pkr_Date.Date
                                };
                                bool respuesta = await DisplayAlert("Comprobación:", usuario.ToString(), "OK", "Cancelar");
                                if (respuesta)
                                {
                                    App.repositorioUsuario.AgregarUsuario(usuario);
                                    await DisplayAlert("Registrado", App.repositorioUsuario.StatusMessage, "OK");
                                    Limpiar();
                                }
                            }
                        }
                        else
                        {
                            await DisplayAlert("Advertencia", "Verifica el formato del correo.", "Ok");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Advertencia", "La cedula no puede tener menos de 9 digitos.", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("Advertencia", "Las contraseñas no coinciden.", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Advertencia", "Verifica que todos los datos hayan sido introducidos.", "Ok");
            }
        }

        private void Limpiar()
        {
            Txt_Identificacion.Text = "";
            Txt_Nombre.Text = "";
            Txt_Username.Text = "";
            Txt_Password.Text = "";
            Txt_PasswordConfirm.Text = "";
            Txt_Email.Text = "";
            Pkr_Date.Date = DateTime.Now.Date;
        }

        /// <summary>
        /// Botón de volver al Login.
        /// </summary>
        private void Btn_Volver_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();
        }
        #endregion
    }
}