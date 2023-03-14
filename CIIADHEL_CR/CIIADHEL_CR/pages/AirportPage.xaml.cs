using Acr.UserDialogs;
using CIIADHEL_CR.controllers;
using CIIADHEL_CR.helpers;
using CIIADHEL_CR.models;
using CIIADHEL_CR.services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Essentials;
using System.Collections.Generic;
using System.IO;
using Plugin.XamarinFormsSaveOpenPDFPackage;
using System.Net.Http;


namespace CIIADHEL_CR
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AirportPage : ContentPage
    {
        public Airport_Principal airport_Principal;
        public AirportPage(Airport_Principal airport_Principal)
        {
            this.airport_Principal = airport_Principal;
            InitializeComponent();
            Application.Current.Properties["ultimaPantalla"] = "airport";
            Application.Current.Properties["ultimoIDAirport"] = airport_Principal.ID_Aeropuerto.ToString();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //made by andreyszcr@gmail.com
            try
            {
                //if texbox are empty or clean so 
                if (string.IsNullOrEmpty(lblName.Text) || lblName.Text == ""
                || string.IsNullOrEmpty(lblName_OACI.Text) || lblName_OACI.Text == ""
                || string.IsNullOrEmpty(lblName_ICAO.Text) || lblName_ICAO.Text == ""
                || string.IsNullOrEmpty(lblState_Airport.Text) || lblState_Airport.Text == ""
                || string.IsNullOrEmpty(lblLanding.Text) || lblLanding.Text == ""
                || string.IsNullOrEmpty(lblLandingLanding.Text) || lblLandingLanding.Text == ""
                || string.IsNullOrEmpty(lblElevation.Text) || lblElevation.Text == ""
                || string.IsNullOrEmpty(lblSurface.Text) || lblSurface.Text == ""
                || string.IsNullOrEmpty(lblAsdaRwy1.Text) || lblAsdaRwy1.Text == ""
                || string.IsNullOrEmpty(lblAsdaRwy2.Text) || lblAsdaRwy2.Text == ""
                || string.IsNullOrEmpty(lblTodaRwy1.Text) || lblTodaRwy1.Text == ""
                || string.IsNullOrEmpty(lblTodaRwy2.Text) || lblTodaRwy2.Text == ""
                || string.IsNullOrEmpty(lblToraRwy1.Text) || lblToraRwy1.Text == ""
                || string.IsNullOrEmpty(lblToraRwy2.Text) || lblToraRwy2.Text == ""
                || string.IsNullOrEmpty(lblLdaRwy1.Text) || lblLdaRwy1.Text == ""
                || string.IsNullOrEmpty(lblLdaRwy2.Text) || lblLdaRwy2.Text == ""
                || string.IsNullOrEmpty(lblFrequency.Text) || lblFrequency.Text == ""
                || string.IsNullOrEmpty(lblFrequencyFrequency.Text) || lblFrequencyFrequency.Text == ""
                || string.IsNullOrEmpty(lblNotam.Text) || lblNotam.Text == ""
                || string.IsNullOrEmpty(lblAddress.Text) || lblAddress.Text == ""
                || string.IsNullOrEmpty(lblPhoneF.Text) || lblPhoneF.Text == ""
                || string.IsNullOrEmpty(lblPhoneS.Text) || lblPhoneS.Text == ""
                || string.IsNullOrEmpty(lblSchedule.Text) || lblSchedule.Text == ""
                || string.IsNullOrEmpty(lblPublic.Text) || lblPublic.Text == ""
                || string.IsNullOrEmpty(lblControl.Text) || lblControl.Text == ""
                || string.IsNullOrEmpty(lblGeoCoordinates.Text) || lblGeoCoordinates.Text == ""
                || string.IsNullOrEmpty(lblInfoTower.Text) || lblInfoTower.Text == ""
                || string.IsNullOrEmpty(lblInfoGeneral.Text) || lblInfoGeneral.Text == ""
                || string.IsNullOrEmpty(lblAirSpace.Text) || lblAirSpace.Text == ""
                || string.IsNullOrEmpty(lblFuel.Text) || lblFuel.Text == ""
                || string.IsNullOrEmpty(lblRuleGeneral.Text) || lblRuleGeneral.Text == ""
                || string.IsNullOrEmpty(lblRuleParticular.Text) || lblRuleParticular.Text == "")
                {
                    using (UserDialogs.Instance.Loading("Cargando", null, null, true, MaskType.Black))
                    {
                        await Task.Delay(4000);
                        AirportsController airportsController = new AirportsController();
                        Airport_Detail airportId = await airportsController.verificationAirport(this.airport_Principal.ID_Aeropuerto);
                        airport_Principal.Descargado = true;
                        await App.SQLiteDB.UpdateAirportAsync(airport_Principal);
                        #region Detalle del aeropuerto ->Airport
                        lblName.Text = airportId.Aeropuerto.Nombre;
                        lblName_OACI.Text = airportId.Aeropuerto.NombreOaci;
                        lblName_ICAO.Text = airportId.Aeropuerto.NombreIcao;
                        lblState_Airport.Text = airportId.Aeropuerto.EstadoAeropuerto;
                        lblLanding.Text = airportId.Pistas.Pista.Replace("|", "\n");
                        lblLandingLanding.Text = airportId.Pistas.Pista;
                        lblElevation.Text = airportId.Pistas.Elevacion;
                        lblSurface.Text = airportId.Pistas.SuperficiePista;
                        lblAsdaRwy1.Text = airportId.Pistas.AsdaRwy1.ToString();
                        lblAsdaRwy2.Text = airportId.Pistas.AsdaRwy2.ToString();
                        lblTodaRwy1.Text = airportId.Pistas.TodaRwy1.ToString();
                        lblTodaRwy2.Text = airportId.Pistas.TodaRwy2.ToString();
                        lblToraRwy1.Text = airportId.Pistas.ToraRwy1.ToString();
                        lblToraRwy2.Text = airportId.Pistas.ToraRwy2.ToString();
                        lblLdaRwy1.Text = airportId.Pistas.LdaRwy1.ToString();
                        lblLdaRwy2.Text = airportId.Pistas.LdaRwy2.ToString();
                        NetworkAccess currentNetwork = Connectivity.NetworkAccess;
                        if (currentNetwork == NetworkAccess.Internet)//if you have internet
                        {
                           
                            var metar = new HtmlWebViewSource();
                            var ruta = "'https://metar-taf.com/es/embed-js/" + airportId.Aeropuerto.NombreOaci + "?target=TItlKBUP'";
                            metar.Html = @"<html><head></head><body><div class='row justify-content-center align-items-center'><a id='metartaf-TItlKBUP' style='font-size:18px; font-weight:500; color:#000; width:300px; height:435px; display:block'>METAR </a></div>
                                  <script async defer crossorigin='anonymous' src=" + ruta + "></script></body></html>";
                            frame.Source = metar;
                            no_internet.SetValue(IsVisibleProperty, false);

                       
                        }
                        else
                        {
                            frame.SetValue(IsVisibleProperty, false);
                        }
                        #endregion
                        #region Frecuencias del aeropuerto ->Airport_Frequencie
                        foreach (Airport_Frequencies fre in airportId.Frecuencias)
                        {
                            lblFrequency.Text = string.Join("\n", airportId.Frecuencias.Select(c => c.FrecuenciaFrecuencia));
                        }
                        foreach (Airport_Frequencies fre in airportId.Frecuencias)
                        {
                            lblFrequencyFrequency.Text = string.Join("\n", airportId.Frecuencias.Select(c => c.TipoFrecuencia));
                        }
                        #endregion
                        // Valid if the value is empty
                        #region Notams del aeropuerto -> Notams
                        if ((airportId.NOTAMS != null) && airportId.NOTAMS.Any())
                        {
                            // Same Method Join to concatenates the elements
                            foreach (Airport_Frequencies fre in airportId.Frecuencias)
                            {
                                lblNotam.Text = string.Join("\n", airportId.NOTAMS.Select(c => c.NotamNotam + "\nFecha Creacion: " + c.FechaCreacion
                                 + "\nFecha Vencimiento: " + c.FechaVencimiento + "\nUltima Actualizacion: " + c.UltimaActualizacion + "\nUsuario Creacion: " + c.UsuarioCreacion + "\nUsuario Actualizacion: " + c.UsuarioActualizacion + "\n"));
                            }
                        }
                        else
                        {
                            lblNotam.Text = "No disponible";
                        }
                        #endregion
                        #region Contacto ->Contact
                        lblAddress.Text = airportId.Contacto.DireccionExacta;
                        lblPhoneF.Text = airportId.Contacto.NumeroTelefono1;
                        lblPhoneS.Text = (string)airportId.Contacto.NumeroTelefono2;
                        lblSchedule.Text = airportId.Contacto.Horario;
                        #endregion
                        #region Carcateristicas ->Features
                        lblPublic.Text = airportId.Caracteristicas_Especiales.Publico.ToString() == "1" ? "Si" : "No";
                        lblControl.Text = airportId.Caracteristicas_Especiales.Controlado.ToString() == "1" ? "Si" : "No";
                        lblGeoCoordinates.Text = airportId.Caracteristicas_Especiales.Coordenada;
                        lblInfoTower.Text = airportId.Caracteristicas_Especiales.InfoTorre;
                        lblInfoGeneral.Text = airportId.Caracteristicas_Especiales.InfoGeneral;
                        lblAirSpace.Text = airportId.Caracteristicas_Especiales.EspacioAereo;
                        lblFuel.Text = airportId.Caracteristicas_Especiales.Combustible;
                        lblRuleGeneral.Text = airportId.Caracteristicas_Especiales.NormaGeneral;
                        lblRuleParticular.Text = airportId.Caracteristicas_Especiales.NormaParticular;
                        #endregion
                    }
                }
                else
                {
                    UserDialogs.Instance.HideLoading();//hide loader
                }
            }
            catch (Exception ex)
            {
                await DialogService.ShowErrorAsync("Error", ex.Message, "Ok");//display error on screen 
            }
        }
        //**********************************************************************************************************************
        // made by andreyszcr@gmail.com
        private async void txtDocumento_Clicked(object sender, EventArgs e)
        {
            try //validation 
            {
                if (this.airport_Principal.ID_Aeropuerto == 23)//if aiport is sjo/Juan SantaMaria
                {
                    NetworkAccess currentNetwork = Connectivity.NetworkAccess;
                    if (currentNetwork == NetworkAccess.Internet) //if you have internet
                    {
                        #region Archivos con internet
                        var client = new HttpClient();
                        var Stream = await client.GetStreamAsync("http://www.uvairlines.com/admin/resources/mroc.pdf");//url with http
                        using (var memory = new MemoryStream()){
                            await Stream.CopyToAsync(memory);
                            await CrossXamarinFormsSaveOpenPDFPackage.Current.SaveAndView("MROC.pdf", "application/pdf",
                            memory, PDFOpenContext.InApp);// use to open files
                        }
                        #endregion
                    }
                    else 
                    {
                        //recordatorio 
                        // pruebe el archivo con internet y lo descarga pior favor y luego usar sin internet!!!
                        #region Abrir archivos sin conexion a Internet
                        var costumeFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>{
                            //format on iphone 
                            {DevicePlatform.iOS, new[] {"com.adobe.pdf"}},
                            //format on android
                            {DevicePlatform.Android, new[] {"application/pdf"}},
                            // format on windows
                            {DevicePlatform.UWP, new[] {".pdf"}},
                            //format on tizen
                            {DevicePlatform.Tizen, new[] {"*/*"}},
                            //format on macbook 
                            {DevicePlatform.macOS, new[] {"pdf"}}
                        });
                        var pickfile = await FilePicker.PickAsync(new PickOptions()// use to open files
                        {
                            FileTypes = costumeFileType,
                            PickerTitle = "Pick an PDF"
                        });
                        if (pickfile != null)
                        {
                            var stream = await pickfile.OpenReadAsync();
                            using (var memorysteam = new MemoryStream())
                            {
                                await stream.CopyToAsync(memorysteam);
                                await CrossXamarinFormsSaveOpenPDFPackage.Current.SaveAndView("MROC.pdf", "application/pdf", memorysteam,
                                PDFOpenContext.InApp);//use a nugget to open pdf files
                            } 
                        }
                        else
                        {
                            await DialogService.ShowErrorAsync("Error", "No se encuentra el archivo correspondiente", "Ok");//display error
                        }
                        #endregion
                    }
                }// if airport is not Juan SantaMaria
                else if (this.airport_Principal.ID_Aeropuerto!= 23)
                {
                    await DialogService.ShowErrorAsync("Error", "No se encuentra el archivo", "OK");//display error
                }
                else
                {
                    await DialogService.ShowErrorAsync("Error", "No se encuentra los archivos", "OK");//display error
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);//error on console
                await DialogService.ShowErrorAsync("Error","Error en los archivos","Ok");
            }
        }
    }
}