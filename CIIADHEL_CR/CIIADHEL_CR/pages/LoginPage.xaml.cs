using Acr.UserDialogs;
using CIIADHEL_CR.models;
using CIIADHEL_CR.services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace CIIADHEL_CR.pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        ILoginManager iml = null;
        //connection to internet
        NetworkAccess currentNetwork = Connectivity.NetworkAccess;
        Login log;
        String sCedula = "";
        String sPassword = "";
        String sIdAirport = "";
        public LoginPage()
        {
            InitializeComponent();
        }
        public LoginPage(ILoginManager ilm)
        {
            InitializeComponent();
            iml = ilm;//connecttion with interface
        }
        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            /// email=andreyszcr@gmail.com
            sCedula = txtID.Text;//var to connect on Api
            sPassword = txtPassword.Text;
            log = new Login(sCedula, sPassword, sIdAirport);
            //connection with services files
            UserServices userServices = new UserServices();
            log.Cedula = sCedula;
            log.Contrasena = sPassword;
            if (currentNetwork == NetworkAccess.Internet)
            {
                //conddition in case the textbox are empty
                if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtPassword.Text))
                {
                    lblResultado.Text = "Usuario o Contraseña no pueden estar vacios";
                    UserDialogs.Instance.HideLoading();
                }
                //method bool in case the username or pasword does not exist 
                else if (userServices.ValidationLogin(log) == true)
                {
                    lblResultado.Text = ""; //clear message
                    string[] session = userServices.PostLOGIN(log);//session var y method post
                    log.Cedula = session[0]; 
                    log.IdAeropuerto = session[1];
                    App.Current.Properties["Cedula"] = log.Cedula;
                    App.Current.Properties["Contraseña"] = log.Contrasena;
                    App.Current.Properties["ID_Aeropuerto"] = log.IdAeropuerto;
                    App.Current.Properties["IsLoggedIn"] = true;
                    await this.Navigation.PushModalAsync(new UpdateAirportPage(log.Cedula, log.Contrasena, log.IdAeropuerto));//pass to another form
                }
                else
                {
                    txtID.Text = "";
                    txtPassword.Text = "";
                    lblResultado.Text = "Usuario o Contraseña incorrectos!"; //display error on screen
                }
            }
            else
            {
                await DisplayAlert("Advertencia", "No tienes conexion a internet.", "OK");// error on screen sin internet
            }
        }
    }
}