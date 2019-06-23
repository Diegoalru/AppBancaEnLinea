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

        private static Usuario usuario = new Usuario
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

        private void Login(object sender, EventArgs e)
        {
            string Username = Txt_Username.Text;
            string Password = Txt_Password.Text;
            bool ValidacionForm = !VerificaForm();

            if (ValidacionForm)
            {
                bool ValidacionUsername = VerificaCampo(Txt_Username.Text);
                bool ValidacionPassword = VerificaCampo(Txt_Password.Text);

                if (ValidacionUsername)
                {
                    DisplayAlert("Validación de ingreso.", "El campo de usuario no puede estar vacio.", "OK");
                }
                if (ValidacionPassword)
                {
                    DisplayAlert("Validación de ingreso.", "El campo de la contraseña no puede estar vacio.", "OK");
                }

                if((!ValidacionUsername && !ValidacionPassword) == true)
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
            else
            {
                DisplayAlert("Validación de ingreso.", "Debes proporcionar los datos solicitados.", "OK");
            }
        }

        private bool VerificaCredenciales(string user, string pass)
        {
            if (usuario.USU_USERNAME.Equals(user) && usuario.USU_PASSWORD.Equals(pass))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool VerificaForm()
        {
            string Username = Txt_Username.Text;
            string Password = Txt_Password.Text;

            if (Username.Equals("") && Password.Equals(""))
                return true;

            return false;
        }
        
        private bool VerificaCampo(string Campo)
        {
            if(Campo.Equals(null) || Campo.Equals(""))
                return true;

            return false;
        }
    }
}