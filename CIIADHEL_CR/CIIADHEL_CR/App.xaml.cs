using CIIADHEL_CR.data;
using CIIADHEL_CR.helpers;
using CIIADHEL_CR.interfaces;
using CIIADHEL_CR.models;
using CIIADHEL_CR.pages;
using CIIADHEL_CR.services;
using Plugin.FirebasePushNotification;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIIADHEL_CR
{
    public partial class App : Application, ILoginManager
    {
        //made by andreyszcr@gmail.com
        public static int val;
        public static string[] var = new string[3];
        static SQLiteAirportsPrincipal db;
        static SQLiteAirports dbAirports;
        static SQLiteIdentifier dbIdentifier;
        public App()
        {
            InitializeComponent();
            Current = this;
            //this.td();
            //MainPage = new MainPage();
            #region Notificaciones -->CrossFirebasePushNotification
            // Comentar estas lineas de codigo cuando se ejecuta la aplicacion en un embulador 41-60
            CrossFirebasePushNotification.Current.Subscribe("all");
            CrossFirebasePushNotification.Current.OnTokenRefresh += async (s, p) =>
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
                    GToken.token = p.Token;
                }
                catch(Exception ex)
                {
                    await DialogService.ShowErrorAsync("Error", ex.Message, "OK");//display error on screen
                }
            };
            //CrossFirebasePushNotification.Current.OnNotificationOpened += async (s, p) =>
            //{
            //    try
            //    {
            //        string idAirport = "";
            //        foreach (var data in p.Data)
            //        {
            //            if (data.Key.Equals("idAirport"))
            //            {
            //                idAirport = data.Value.ToString();
            //                break;
            //            }
            //        }
            //        GNotifications.airportNotification = await App.SQLiteDB.GetAirportByIdAsync(Convert.ToInt32(idAirport));
            //        GNotifications.isOpenNotification = true;
            //    }
            //    catch (Exception ex)
            //    {
            //        await DialogService.ShowErrorAsync("Error", ex.Message, "OK");//display error on screen
            //    }
            //};
            #endregion
        }
        //****************************************************************
        #region metodo de td public void td()
        public void td()
        {
            Device.BeginInvokeOnMainThread(async () =>{ 
                if (await SQLiteDBIdentifier.existIdentifier() == false)
                {
                    Application.Current.MainPage = new PhonePage();
                }
                else
                {
                    MainPage = new NavigationPage(new MainPage());
                }
            });
            
        }
        #endregion
        //****************************************************************
        //conexion de base dedatos
        #region Conexion de SQLiteAirportsPrincipal SQLiteDB
        public static SQLiteAirportsPrincipal SQLiteDB
        {
            // Check if there is any database connection
            // If it is null it create the db
            get
            {
                if (db == null)
                {
                    db = new SQLiteAirportsPrincipal(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "AirportGeneral.db"));
                }
                return db;
            }
        }
        #endregion
        //****************************************************************
        #region Aeropuerto con bd de SQLLite SQLiteDBAirports
        public static SQLiteAirports SQLiteDBAirports
        {
            // Check if there is any database connection
            // If it is null it create the db
            get
            {
                if (dbAirports == null)
                {
                    dbAirports = new SQLiteAirports(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "Airports.db"));

                }
                return dbAirports;
            }
        }
        #endregion
        //*****************************************************************
        #region Identificador de sqllite -->SQLiteDBIdentifier
        public static SQLiteIdentifier SQLiteDBIdentifier
        {
            // Check if there is any database connection
            // If it is null it create the db
            get
            {
                if (dbIdentifier == null)
                {
                    dbIdentifier = new SQLiteIdentifier(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "Identifier.db"));

                }
                return dbIdentifier;
            }
        }
        #endregion
        //*****************************************************************
        public void ShowMainPage()//method to use main page
        {
            MainPage = new MainPage();
        }
        //*****************************************************************
        public void Logout()//in case YOU CAN USE LOG OUT
        {
            Properties["IsLoggedIn"] = false;
        }
        //*****************************************************************
        protected override async void OnStart()
        {
            try
            {
                base.OnStart();
                if (await SQLiteDBIdentifier.existIdentifier() == false)
                {
                    MainPage = new NavigationPage(new PhonePage());
                }
                else
                {
                    MainPage = new NavigationPage(new MainPage());
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw ex;
            }

        }
        //*******************************************************************
        protected override void OnSleep()
        {
            base.OnSleep();
            // Save session var 
            if (Application.Current.Properties.ContainsKey("Cedula")
                && Application.Current.Properties.ContainsKey("ID_Aeropuerto"))
            {
                string cedula = (string)Application.Current.Properties["Cedula"];
                Application.Current.Properties["Cedula"] = cedula;

                string idAeropuerto = (string)Application.Current.Properties["ID_Aeropuerto"];
                Application.Current.Properties["ID_Aeropuerto"] = idAeropuerto;
            }
            if (Application.Current.Properties.ContainsKey("ultimaPantalla"))
            {
                string lastPage = (string)Application.Current.Properties["ultimaPantalla"];
                Application.Current.Properties["ultimaPantalla"] = lastPage;
            }
        }
        //*****************************************************************
        protected override void OnResume()
        {
            base.OnResume();
            if (Application.Current.Properties.ContainsKey("ultimaPantalla"))//if Exists var
            {
                string lastPage = (string)Application.Current.Properties["ultimaPantalla"];
                if (lastPage.Equals("update"))
                {
                    if (!Application.Current.Properties.ContainsKey("Cedula")
                        || !Application.Current.Properties.ContainsKey("ID_Aeropuerto"))
                    {
                        MainPage = new NavigationPage(new HomePage());
                    }
                }
            }
        }
        //*****************************************************************
    }
}
