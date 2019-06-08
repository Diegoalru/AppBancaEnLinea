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
        }

        private void Login(object sender, EventArgs e)
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

            if(usuario.USU_USERNAME.Equals(Txt_Username.Text) && usuario.USU_PASSWORD.Equals(Txt_Password.Text))
            {
                Application.Current.MainPage = new MainPage();
            }
            else
            {
                DisplayAlert("Validación de ingreso.", "Credenciales Incorrectas.", "OK");
            }
        }
    }
}