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
using System.Windows.Input;
using CIIADHEL_CR.Popups;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Lottie.Forms;

namespace CIIADHEL_CR.pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhonePage : ContentPage
    {
       
        private Country selectedCountry;

        public PhonePage()
        {
            SelectedCountry = new Country("Costa Rica", "506", "XXXX-XXXX", "cr.png", "8312-3456");
            ShowPopupCommand = new Command(async _ => await ExecuteShowPopupCommand());
            CountrySelectedCommand = new Command(country => ExecuteCountrySelectedCommand(country as Country));
            BindingContext = this;
            InitializeComponent();

        }

        public Country SelectedCountry
        {
            get => selectedCountry;
           set => SetProperty(ref selectedCountry, value);
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
           [CallerMemberName] string propertyName = "",
           Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        private void ExecuteCountrySelectedCommand(Country country)
        {
            SelectedCountry = country;
        }

        public ICommand ShowPopupCommand { get; }
        public ICommand CountrySelectedCommand { get; }

        private Task ExecuteShowPopupCommand()
        {
            txtPhone.Text = "";

            var popup = new ChooseCountryPopup(SelectedCountry)
            {
                CountrySelectedCommand = CountrySelectedCommand
            };
            return Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(popup);
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
                    btn.IsVisible = false;
                    lottie.IsVisible = true;
                    lblError.Text = "No puede existir espacios vacíos";
                    //await Task.Delay(6000);
                    //lottie.IsVisible = false;
                    //btn.IsVisible = true;
                    

                }
                else
                {
                    string mask = SelectedCountry.CountryMask;
                    string pattern = mask
                        .Replace("(", "\\(")
                        .Replace(")", "\\)")
                        .Replace(" ", "\\s")
                        .Replace("X", "\\d");

                    if (!Regex.IsMatch(identifier, pattern))
                    {
                        btn.Source = "loadingPhoneCancel.png";
                        lblError.Text = "El número proporcionado no es válido";
                        await Task.Delay(500);
                        btn.Source = "log.png";
                    }                
                    //else if (!identifier.ToCharArray().All(Char.IsDigit))
                    //{

                    //    btn.Source = "loadingPhoneCancel.png";
                    //    lblError.Text = "El teléfono debe ser numérico";
                    //    await Task.Delay(500);
                    //    btn.Source = "log.png";

                    //}
                    else
                    {

                        //bool ValidateIdentifier = await NotificationsServices.existsIdentifier(identifier);

                        //if (ValidateIdentifier)
                        //{
                        //    lblError.Text = "";
                        //    btn.Source = "loadingPhone.png";
                        //    btn.IsEnabled = true;

                        //    var action = await DisplayAlert("Alerta", "Este numero de telefono ya ha sido registrado, los favoritos seran cargados automaticamente, ¿desea continuar?", "Si", "No");

                        //    if (action)
                        //    {
                        //        Identifier phone = new Identifier
                        //        {
                        //            Telephone_Number = txtPhone.Text,
                        //        };
                        //        await App.SQLiteDBIdentifier.SaveIdentifierAsync(phone);

                        //        await NotificationsServices.saveToken(phone.Telephone_Number, GToken.token);

                        //        Application.Current.MainPage = new MainPage();

                        //        btn.IsEnabled = false;
                        //    }

                        //    lblError.Text = "";
                        //    btn.Source = "log.png";

                        //}
                        //else
                        //{
                        //    btn.Source = "loadingPhone.png";
                        //    btn.IsEnabled = true;

                        //    Identifier phone = new Identifier
                        //    {
                        //        Telephone_Number = txtPhone.Text,

                        //    };
                        //    await App.SQLiteDBIdentifier.SaveIdentifierAsync(phone);

                        //    await NotificationsServices.saveToken(phone.Telephone_Number, GToken.token);

                        btn.IsEnabled = false;

                        Application.Current.MainPage = new MainPage();

                        //}
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            string identifier = txtPhone.Text;

            if (string.IsNullOrEmpty(identifier))
            {
                lottie.IsVisible = true;
               Phone.IsVisible = false;
            }
            else
            {
                lottie.IsVisible = false;
                Phone.IsVisible = true;
            }
        }



        protected override void OnAppearing()
    {
        base.OnAppearing();
            lottie.IsVisible = false;
            lottie.PlayAnimation();
            lottie.RepeatMode = RepeatMode.Infinite;
        }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        lottie.PauseAnimation();
        lottie = null; // Liberar la referencia al objeto
    }

}

}