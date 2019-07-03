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

        private void Btn_Volver_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();
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

        public bool ValidaMail(string Mail)
        {
            if (Mail.Contains("@") && Mail.Contains("."))
                return true;
            return false;
        }



        
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
                    if (item.USU_IDENTIFICACION == id)
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



        private void Btn_Register_Clicked(object sender, EventArgs e)
        {
            if(VerificaForm())
            {
                DisplayAlert("Advertencia", "Listo.", "Ok");
            }
            else
            {
                DisplayAlert("Advertencia", "Verifica que todos los datos hayan sido introducidos.", "Ok");
            }
        }
    }
}