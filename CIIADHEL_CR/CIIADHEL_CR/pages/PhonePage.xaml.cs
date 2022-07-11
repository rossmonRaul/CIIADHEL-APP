using System;
using CIIADHEL_CR.controllers;
using CIIADHEL_CR.helpers;
using CIIADHEL_CR.models;
using CIIADHEL_CR.services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIIADHEL_CR.pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhonePage : ContentPage
    {
        public PhonePage()
        {
            InitializeComponent();
        }

        private async void Phone_Clicked(object sender, EventArgs e)
        {
            try
            {
                NotificationsServices notificationServices = new NotificationsServices();

                ImageButton btn = (ImageButton)sender;


                string identifier = txtPhone.Text;

                if (identifier == null || identifier.Length == 0)
                {
                    btn.Source = "loadingPhoneCancel.png";
                    lblError.Text = "No puede existir espacios vacíos";
                    await Task.Delay(500);
                    btn.Source = "log.png";

                }
                else if (identifier.Length < 8)
                {

                    btn.Source = "loadingPhoneCancel.png";
                    lblError.Text = "Debe contener al menos 8 caracteres";
                    await Task.Delay(500);
                    btn.Source = "log.png";


                }
                else if (!identifier.ToCharArray().All(Char.IsDigit))
                {

                    btn.Source = "loadingPhoneCancel.png";
                    lblError.Text = "El teléfono debe ser numérico";
                    await Task.Delay(500);
                    btn.Source = "log.png";

                }
                else
                {

                    bool ValidateIdentifier = await NotificationsServices.existsIdentifier(identifier);

                    if (ValidateIdentifier)
                    {
                        lblError.Text = "";
                        btn.Source = "loadingPhone.png";
                        btn.IsEnabled = true;

                        var action = await DisplayAlert("Alerta", "Este numero de telefono ya ha sido registrado, los favoritos seran cargados automaticamente, ¿desea continuar?", "Si", "No");

                        if (action)
                        {
                            Identifier phone = new Identifier
                            {
                                Telephone_Number = txtPhone.Text,
                            };
                            await App.SQLiteDBIdentifier.SaveIdentifierAsync(phone);

                            //await NotificationsServices.saveToken(phone.Telephone_Number, GToken.token);

                            Application.Current.MainPage = new MainPage();

                            btn.IsEnabled = false;
                        }

                        lblError.Text = "";
                        btn.Source = "log.png";

                    }
                    else
                    {
                        btn.Source = "loadingPhone.png";
                        btn.IsEnabled = true;

                        Identifier phone = new Identifier
                        {
                            Telephone_Number = txtPhone.Text,

                        };
                        await App.SQLiteDBIdentifier.SaveIdentifierAsync(phone);

                        //await NotificationsServices.saveToken(phone.Telephone_Number, GToken.token);

                        btn.IsEnabled = false;

                        Application.Current.MainPage = new MainPage();

                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}