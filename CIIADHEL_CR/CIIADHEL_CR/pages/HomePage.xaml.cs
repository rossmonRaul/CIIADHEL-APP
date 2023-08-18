using Acr.UserDialogs;
using CIIADHEL_CR.controllers;
using CIIADHEL_CR.helpers;
using CIIADHEL_CR.models;
using CIIADHEL_CR.services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CIIADHEL_CR.interfaces;
using Lottie.Forms;
using System.Linq.Expressions;
using System.Threading;
using System.Security.Cryptography;

namespace CIIADHEL_CR.pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private const string TooltipShownKey = "TooltipShown";
        private const int AnimationDuration = 800;
        private const int FadeOutDuration = 6000;
        private const int DelayDuration = 4000;
        public HomePage()
        {
            InitializeComponent();
            BindingContext = this;
            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetHasBackButton(this, false);
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            Application.Current.Properties["ultimaPantalla"] = "home";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try //validations 
            {
                NetworkAccess currentNetwork = Connectivity.NetworkAccess;
                if (currentNetwork == NetworkAccess.Internet)//if you have internet
                {
                    lottie.PlayAnimation();
                    lottie.RepeatMode = RepeatMode.Infinite;
                    lottie.Speed = 2.0f;
                    gridBuscarAeropuerto.IsVisible = false;
                    if (await updateAirports() != false)
                    {
                        lstAirposts.ItemsSource = null;
                    }
                    AirportServices airportServices = new AirportServices();
                    List<Airport_Principal> airport_Principals = await App.SQLiteDB.GetAllAirportAsync();
                    //method to get whole airports
                    List<Identifier> identifiers = await App.SQLiteDBIdentifier.getIdentifier();// mehtod to get Identifier code
                    string id = identifiers[0].Telephone_Number;
                    List<Airport_Favorite> Recuperados = await AirportServices.getFavoritebyIdentificador(id);
                    foreach (var airport in Recuperados)
                    {
                        Airport_Principal aux = airport_Principals.Where(a => a.ID_Aeropuerto == airport.ID_Aeropuerto).FirstOrDefault();
                        if (aux != null)
                        {
                            aux.Favorito = true;
                            await App.SQLiteDB.UpdateAirportAsync(aux);
                        }
                    }
                    if (airport_Principals.Count == 0)
                    {
                        // Show message error in screen
                        lstAirposts.ItemsSource = null;
                        return;
                    }

                    gridContainer.IsVisible = false;
                    lstAirposts.ItemsSource = airport_Principals;

                    if (!Application.Current.Properties.ContainsKey(TooltipShownKey))
                    {
                        Application.Current.Properties[TooltipShownKey] = true;

                        if (Parent != null)
                        {
                            tooltipFrame.Scale = 2;
                            tooltipFrame.Opacity = 1;
                            tooltipFrame.IsVisible = true;

                            var animationIn = new Animation(v => tooltipFrame.Scale = v, 0, 1, Easing.SpringOut);

                            animationIn.Commit(this, "TooltipAnimationIn", length: AnimationDuration, finished: (d, b) => tooltipFrame.Scale = 1);

                            var fadeOutTimer = new Timer(FadeOutTimerCallback, null, DelayDuration, Timeout.Infinite);
                        }
                    }

                    if (GNotifications.isOpenNotification)
                    {
                        GNotifications.isOpenNotification = false;
                        await Application.Current.MainPage.Navigation.PushModalAsync(new AirportPage(GNotifications.airportNotification));
                    }
                }
                else
                {
                    lottie.PlayAnimation();
                    lottie.RepeatMode = RepeatMode.Infinite;
                    lottie.Speed = 2.0f;
                    gridBuscarAeropuerto.IsVisible = false;
                    await Task.Delay(3000);
                    gridContainer.IsVisible = false;
                    
                    List<Airport_Principal> airport_Principals = await App.SQLiteDB.GetAllAirportAsync();
                    if (airport_Principals.Count == 0)
                    {
                        await DialogService.ShowErrorAsync("Alerta", "No hay conexion a internet", "OK");//display error on screen
                    }
                    lstAirposts.ItemsSource = airport_Principals;
                }
            }
            catch (Exception ex)
            {
                throw (ex);//display error on console
                //await DialogService.ShowErrorAsync("Error", ex.Message, "OK");//show error on screen
            }
        }
 
        private async void lstAirposts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Airport_Principal airports = e.SelectedItem as Airport_Principal;//loading list
            await Navigation.PushAsync(new AirportPage(airports));
        }
        //*******************************************************************************************************
        #region metodo para actualizar aeropuertos -->updateAirports()
        private async Task<bool> updateAirports()
        {
            try
            {
                bool error = false;
                NetworkAccess currentNetwork = Connectivity.NetworkAccess;
                int airportsLenght = await AirportServices.getSizeAirports();
                int airportsLenghtSQLite = await App.SQLiteDB.GetLengthAirportsAsync();
                if (airportsLenghtSQLite == 0 || airportsLenghtSQLite == airportsLenght)//if airport is empty or different of lentgh so
                {
                    List<Airport_Principal> airports = await AirportServices.getAllAirports();//get list airports
                    if (await App.SQLiteDB.SaveAllAirports(airports, (airportsLenghtSQLite == 0) ? true : false) == 0)
                    {
                        error = true;
                    }
                }
                return error;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        //*******************************************************************************************************
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                NetworkAccess currentNetwork = Connectivity.NetworkAccess;
                var b = sender as ImageButton;// Get the data of the selected airport in the button
                var ob = b.CommandParameter as Airport_Principal;
                int id = ob.ID_Aeropuerto;
                if (currentNetwork == NetworkAccess.Internet)// Validation Network Access  
                {
                    AirportsController airportsController = new AirportsController();
                    bool downloadOK = await airportsController.downloadAirport(id);
                    if (downloadOK)// Valid if download is true
                    {
                        ImageButton btn = (ImageButton)sender;
                        btn.Source = "downloading2"; //load with source downloading 
                        btn.IsEnabled = true;
                        #region Clase Aeropuerto -->Airport_Principal airport = new Airport_Principal()
                        Airport_Principal airport = new Airport_Principal()
                        {
                            ID_Aeropuerto = id,
                            Nombre = ob.Nombre,
                            Estado_Aeropuerto = ob.Estado_Aeropuerto,
                            Usuario_Creacion = ob.Usuario_Creacion,
                            Nombre_OACI = ob.Nombre_OACI,
                            NombreICAO = ob.NombreICAO,
                            Direccion_Exacta = ob.Direccion_Exacta,
                            Coordenada = ob.Coordenada,
                            Elevacion = ob.Elevacion,
                            Espacio_Aereo = ob.Espacio_Aereo,
                            Horario = ob.Horario,
                            Numero_Telefono1 = ob.Numero_Telefono1,
                            Favorito = ob.Favorito,
                            Descargado = true,
                        };
                        #endregion
                        await App.SQLiteDB.UpdateAirportAsync(airport);// Save status of download 
                        List<Airport_Principal> airport_Principals = await App.SQLiteDB.GetAllAirportAsync();// Refresh the screen to see the changes
                        lstAirposts.ItemsSource = airport_Principals; //display list
                        btn.IsEnabled = false;
                    }
                }
                else
                {
                    await DialogService.ShowErrorAsync("Alerta", "No hay conexion a internet", "OK");//display an error connection
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);//manage error on console
                await DialogService.ShowErrorAsync("Error al descargar", ex.Message, "OK");
            }
        }
        //*******************************************************************************************************
        private async void Favorito_Clicked(object sender, EventArgs e)
        {
            try //validation
            {
                NetworkAccess currentNetwork = Connectivity.NetworkAccess;
                var b = sender as ImageButton;                 // Get the data of the selected airport in the button
                var ob = b.CommandParameter as Airport_Principal;
                AirportServices airportServices = new AirportServices();// connection with class on file models
                string id = "";
                List<Identifier> identifiers = await App.SQLiteDBIdentifier.getIdentifier();// Get the identifier
                if (identifiers.Count != 0) //if identifier of cellphone is different of zero
                {
                    id = identifiers[0].Telephone_Number;
                }
                //******************************************************
                if (currentNetwork == NetworkAccess.Internet)
                {
                    bool Validate = await AirportServices.getValidateExist(ob.ID_Aeropuerto,id);//method to validate if exists 
                    if (Validate)
                    {
                        ImageButton btn = (ImageButton)sender;
                        btn.Source = "change_delete.png";
                        btn.IsEnabled = true;
                        #region Clase Aeropuertos -->Airport_Principal airport = new Airport_Principal()
                        Airport_Principal airport = new Airport_Principal()
                        {
                            ID_Aeropuerto = ob.ID_Aeropuerto,
                            Nombre = ob.Nombre,
                            Estado_Aeropuerto = ob.Estado_Aeropuerto,
                            Usuario_Creacion = ob.Usuario_Creacion,
                            Nombre_OACI = ob.Nombre_OACI,
                            NombreICAO = ob.NombreICAO,
                            Direccion_Exacta = ob.Direccion_Exacta,
                            Coordenada = ob.Coordenada,
                            Elevacion = ob.Elevacion,
                            Espacio_Aereo = ob.Espacio_Aereo,
                            Horario = ob.Horario,
                            Numero_Telefono1 = ob.Numero_Telefono1,
                            Favorito = false,
                            Descargado = ob.Descargado,
                            Valor1 = ob.Valor1,
                            Valor2 = ob.Valor2,
                        };
                        #endregion
                        await App.SQLiteDB.UpdateAirportAsync(airport);  // Save status of favorite 
                        List<Airport_Principal> airport_Principals = await App.SQLiteDB.GetAllAirportAsync();//get whole airports sql lite
                        lstAirposts.ItemsSource = airport_Principals; //display list 
                        await AirportServices.deleteFavoriteAirports(ob.ID_Aeropuerto, id); // clear data
                        btn.IsEnabled = false;
                    }
                    else
                    {
                        ImageButton btn = (ImageButton)sender;
                        btn.Source = "change.png";
                        btn.IsEnabled = true;
                        #region Clase Aeropuerto principal --> Airport_Principal airports = new Airport_Principal()
                        Airport_Principal airports = new Airport_Principal()
                        {
                            ID_Aeropuerto = ob.ID_Aeropuerto,
                            Nombre = ob.Nombre,
                            Estado_Aeropuerto = ob.Estado_Aeropuerto,
                            Usuario_Creacion = ob.Usuario_Creacion,
                            Nombre_OACI = ob.Nombre_OACI,
                            NombreICAO = ob.NombreICAO,
                            Direccion_Exacta = ob.Direccion_Exacta,
                            Coordenada = ob.Coordenada,
                            Elevacion = ob.Elevacion,
                            Espacio_Aereo = ob.Espacio_Aereo,
                            Horario = ob.Horario,
                            Numero_Telefono1 = ob.Numero_Telefono1,
                            Favorito = true,
                            Descargado = ob.Descargado,
                            Valor1 = ob.Valor1,
                            Valor2 = ob.Valor2,
                        };
                        #endregion
                        await App.SQLiteDB.UpdateAirportAsync(airports); // Save status of favorite 
                        List<Airport_Principal> airport_Principals = await App.SQLiteDB.GetAllAirportAsync();//get all airports
                        lstAirposts.ItemsSource = airport_Principals;//display list
                        #region Aeropuerto favorito -->Airport_Favorite airport = new Airport_Favorite()
                        Airport_Favorite airport = new Airport_Favorite()
                        {
                            ID_Aeropuerto = ob.ID_Aeropuerto,
                            Identificador = id,
                            Nombre = ob.Nombre,
                            Nombre_OACI = ob.Nombre_OACI,
                            NombreICAO = ob.NombreICAO,
                            Usuario_Creacion = ob.Usuario_Creacion,
                        };
                        #endregion region
                        await AirportServices.postFavoriteAirports(airport);
                        btn.IsEnabled = false;
                    }
                }
                else
                {
                    await DisplayAlert("Alerta", "No hay conexion a internet", "OK"); //diaplay an error connection
                }
            }       
            catch (Exception ex)
            {
                Console.WriteLine(ex);//show error on console
                await DialogService.ShowErrorAsync("Error", ex.Message, "OK"); //display error on screen
            }
        }
        //*******************************************************************************************************
        private async void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtBuscar = (SearchBar)sender;
            NetworkAccess currentNetwork = Connectivity.NetworkAccess;
            try
            {
                if (currentNetwork == NetworkAccess.Internet)
                {
                    if (string.IsNullOrEmpty(txtBuscar.Text) || txtBuscar.Text == "")
                    {
                        lstAirposts.ItemsSource = await App.SQLiteDB.GetAllAirportAsync();
                        gridBuscarAeropuerto.IsVisible = false;
                        tooltipFrame.IsVisible = false;
                    }
                    else
                    {
                        var searchResults = await App.SQLiteDB.GetSearchAsync(e.NewTextValue);
                        lstAirposts.ItemsSource = searchResults;

                        if (searchResults == null || searchResults.Count == 0)
                        {
                            gridBuscarAeropuerto.IsVisible = true;
                        }
                        else
                        {
                            gridBuscarAeropuerto.IsVisible = false;
                            tooltipFrame.IsVisible = false;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtBuscar.Text) || txtBuscar.Text == "")
                    {
                        lstAirposts.ItemsSource = await App.SQLiteDB.GetAllAirportAsync();
                        gridBuscarAeropuerto.IsVisible = false;
                    }
                    else
                    {
                        var searchResults = await App.SQLiteDB.GetSearchAsync(e.NewTextValue);
                        lstAirposts.ItemsSource = searchResults;

                        if (searchResults == null || searchResults.Count == 0)
                        {
                            gridBuscarAeropuerto.IsVisible = true;
                        }
                        else
                        {
                            gridBuscarAeropuerto.IsVisible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await DialogService.ShowErrorAsync("Error", "Error en la búsqueda de datos", "Ok");
            }
        }
        //*******************************************************************************************************
        #region metodo para desvanecer el tooltip
        private void FadeOutTimerCallback(object state)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var animationOut = new Animation(v => tooltipFrame.Opacity = v, 1, 0, Easing.Linear);

                animationOut.Commit(this, "TooltipAnimationOut", length: FadeOutDuration, finished: (d, b) =>
                {
                    tooltipFrame.Opacity = 0;
                    tooltipFrame.IsVisible = false;
                });
            });
        }
        #endregion
        //*******************************************************************************************************
    }
}
