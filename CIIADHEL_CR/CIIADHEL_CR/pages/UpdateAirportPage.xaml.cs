using Acr.UserDialogs;
using CIIADHEL_CR.helpers;
using CIIADHEL_CR.models;
using CIIADHEL_CR.services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;

namespace CIIADHEL_CR.pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateAirportPage : ContentPage
    {
        public static string _cedula;
        public static string _password;
        public static string _idAirport;
        private int tapCount;

        public UpdateAirportPage(string cedula, string contrasena, string idAirport)
        {
            InitializeComponent();
            _cedula = cedula;
            _idAirport = idAirport;
            _password = contrasena;
            btnGuardar.Clicked += BtnGuardar_Clicked;
            Login log = new Login(cedula, contrasena, idAirport);
            _idAirport = log.IDAirport();

            var publics = GetPublic();
            pickerPublic.ItemsSource = publics;

            var controls = GetControl();
            pickerControl.ItemsSource = controls;

            Application.Current.Properties["ultimaPantalla"] = "update";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                ///validation if textbox are empty
                #region #region "Validations"
                if (string.IsNullOrEmpty(txtNombre_aeropuerto.Text) || txtNombre_aeropuerto.Text == ""
                   || string.IsNullOrEmpty(txtNombre_OACI.Text) || txtNombre_OACI.Text == ""
                   || string.IsNullOrEmpty(txtNombre_ICAO.Text) || txtNombre_ICAO.Text == ""
                   || string.IsNullOrEmpty(txtPistas.Text) || txtPistas.Text == ""
                   || string.IsNullOrEmpty(txtElevación.Text) || txtElevación.Text == ""
                   || string.IsNullOrEmpty(txtSuperficie_Pista.Text) || txtSuperficie_Pista.Text == ""
                   || string.IsNullOrEmpty(txtASDA_RWAY_1.Text) || txtASDA_RWAY_1.Text == ""
                   || string.IsNullOrEmpty(txtASDA_RWAY_2.Text) || txtASDA_RWAY_2.Text == ""
                   || string.IsNullOrEmpty(txtTODA_RWAY_1.Text) || txtTODA_RWAY_1.Text == ""
                   || string.IsNullOrEmpty(txtTODA_RWAY_2.Text) || txtTODA_RWAY_2.Text == ""
                   || string.IsNullOrEmpty(txtTORA_RWAY_1.Text) || txtTORA_RWAY_1.Text == ""
                   || string.IsNullOrEmpty(txtTORA_RWAY_2.Text) || txtTORA_RWAY_2.Text == ""
                   || string.IsNullOrEmpty(txtLDA_RWAY_1.Text) || txtLDA_RWAY_1.Text == ""
                   || string.IsNullOrEmpty(txtLDA_RWAY_2.Text) || txtLDA_RWAY_2.Text == ""
                   || string.IsNullOrEmpty(txtfrecuencia_TWR.Text) || txtfrecuencia_TWR.Text == ""
                   || string.IsNullOrEmpty(txtfrecuencia_ATIS.Text) || txtfrecuencia_ATIS.Text == ""
                   || string.IsNullOrEmpty(txtfrecuencia_GRND.Text) || txtfrecuencia_GRND.Text == ""
                   || string.IsNullOrEmpty(txtfrecuencia_EMERGENCY.Text) || txtfrecuencia_EMERGENCY.Text == ""
                   || string.IsNullOrEmpty(txtfrecuencia_Otras.Text) || txtfrecuencia_Otras.Text == ""
                   || string.IsNullOrEmpty(txtUbicacion.Text) || txtUbicacion.Text == ""
                   || string.IsNullOrEmpty(txtTelefono1.Text) || txtTelefono1.Text == ""
                   || string.IsNullOrEmpty(txtTelefono2.Text) || txtTelefono2.Text == ""
                   || string.IsNullOrEmpty(txtHorario.Text) || txtHorario.Text == ""
                   || string.IsNullOrEmpty(txtCoordenadas.Text) || txtCoordenadas.Text == ""
                   || string.IsNullOrEmpty(txtinfo_torre.Text) || txtinfo_torre.Text == ""
                   || string.IsNullOrEmpty(txtinfo_general.Text) || txtinfo_general.Text == ""
                   || string.IsNullOrEmpty(txtEspacio_Aereo.Text) || txtEspacio_Aereo.Text == ""
                   || string.IsNullOrEmpty(txtCombustible.Text) || txtCombustible.Text == ""
                   || string.IsNullOrEmpty(txtNormas_Generales.Text) || txtNormas_Generales.Text == ""
                   || string.IsNullOrEmpty(txtNormas_Particulares.Text) || txtNormas_Particulares.Text == ""
                   || string.IsNullOrEmpty(txtEstado_Aeropuerto.Text) || txtEstado_Aeropuerto.Text == ""
                   || string.IsNullOrEmpty(txtNOTAMS.Text) || txtNOTAMS.Text == "")
                {
                    #endregion
                    using (UserDialogs.Instance.Loading("Cargando, espere", null, null, true, MaskType.Black))
                    {
                        await Task.Delay(3000);
                        Airport_Detail airportId = await AirportServices.getAnAirportById(Convert.ToInt32(_idAirport));

                        txtNombre_aeropuerto.Text = airportId.Aeropuerto.Nombre;
                        txtNombre_OACI.Text = airportId.Aeropuerto.NombreOaci;
                        txtNombre_ICAO.Text = airportId.Aeropuerto.NombreIcao;
                        txtPistas.Text = airportId.Pistas.Pista;
                        txtElevación.Text = airportId.Pistas.Elevacion;
                        txtSuperficie_Pista.Text = airportId.Pistas.SuperficiePista;
                        txtASDA_RWAY_1.Text = airportId.Pistas.AsdaRwy1.ToString();
                        txtASDA_RWAY_2.Text = airportId.Pistas.AsdaRwy2.ToString();
                        txtTODA_RWAY_1.Text = airportId.Pistas.TodaRwy1.ToString();
                        txtTODA_RWAY_2.Text = airportId.Pistas.TodaRwy2.ToString();
                        txtTORA_RWAY_1.Text = airportId.Pistas.ToraRwy1.ToString();
                        txtTORA_RWAY_2.Text = airportId.Pistas.ToraRwy2.ToString();
                        txtLDA_RWAY_1.Text = airportId.Pistas.LdaRwy1.ToString();
                        txtLDA_RWAY_2.Text = airportId.Pistas.LdaRwy2.ToString();

                        //storage the list of frequencies and the list of type_frequencies in two array
                        string[] Data_Frec = string.Join("\n", airportId.Frecuencias.Select(c => c.FrecuenciaFrecuencia)).Split(Convert.ToChar("\n"));
                        string[] Data_Frec2 = string.Join("\n", airportId.Frecuencias.Select(c => c.TipoFrecuencia)).Split(Convert.ToChar("\n"));

                        int Frec = Data_Frec.Length;
                        int TipFrec = Data_Frec2.Length;

                        pickerControl.SelectedIndex = (int)airportId.Caracteristicas_Especiales.Controlado;
                        pickerPublic.SelectedIndex = (int)airportId.Caracteristicas_Especiales.Publico;

                        #region "validations list frequencies"//Verification with 5 data from the list frequencies
                        if ((Frec == 5) && (TipFrec == 5)) 
                        {
                            if (Data_Frec2[0] == "TWR")
                                txtfrecuencia_TWR.Text = Data_Frec[0].ToString();

                            if (Data_Frec2[0] == "ATIS")
                                txtfrecuencia_ATIS.Text = Data_Frec[0].ToString();

                            if (Data_Frec2[0] == "GRND")
                                txtfrecuencia_GRND.Text = Data_Frec[0].ToString();

                            if (Data_Frec2[0] == "EMERGENCY")
                                txtfrecuencia_EMERGENCY.Text = Data_Frec[0].ToString();

                            if (Data_Frec2[0] == "Otras")
                                txtfrecuencia_Otras.Text = Data_Frec[0].ToString();

                            if (Data_Frec2[1] == "TWR")
                                txtfrecuencia_TWR.Text = Data_Frec[1].ToString();

                            if (Data_Frec2[1] == "ATIS")
                                txtfrecuencia_ATIS.Text = Data_Frec[1].ToString();

                            if (Data_Frec2[1] == "GRND")
                                txtfrecuencia_GRND.Text = Data_Frec[1].ToString();

                            if (Data_Frec2[1] == "EMERGENCY")
                                txtfrecuencia_EMERGENCY.Text = Data_Frec[1].ToString();

                            if (Data_Frec2[1] == "Otras")
                                txtfrecuencia_Otras.Text = Data_Frec[1].ToString();

                            if (Data_Frec2[2] == "TWR")
                                txtfrecuencia_TWR.Text = Data_Frec[2].ToString();

                            if (Data_Frec2[2] == "ATIS")
                                txtfrecuencia_ATIS.Text = Data_Frec[2].ToString();

                            if (Data_Frec2[2] == "GRND")
                                txtfrecuencia_GRND.Text = Data_Frec[2].ToString();

                            if (Data_Frec2[2] == "EMERGENCY")
                                txtfrecuencia_EMERGENCY.Text = Data_Frec[2].ToString();

                            if (Data_Frec2[2] == "Otras")
                                txtfrecuencia_Otras.Text = Data_Frec[2].ToString();

                            if (Data_Frec2[3] == "TWR")
                                txtfrecuencia_TWR.Text = Data_Frec[3].ToString();

                            if (Data_Frec2[3] == "ATIS")
                                txtfrecuencia_ATIS.Text = Data_Frec[3].ToString();

                            if (Data_Frec2[3] == "GRND")
                                txtfrecuencia_GRND.Text = Data_Frec[3].ToString();

                            if (Data_Frec2[3] == "EMERGENCY")
                                txtfrecuencia_EMERGENCY.Text = Data_Frec[3].ToString();

                            if (Data_Frec2[3] == "Otras")
                                txtfrecuencia_Otras.Text = Data_Frec[3].ToString();

                            if (Data_Frec2[4] == "TWR")
                                txtfrecuencia_TWR.Text = Data_Frec[4].ToString();

                            if (Data_Frec2[4] == "ATIS")
                                txtfrecuencia_ATIS.Text = Data_Frec[4].ToString();

                            if (Data_Frec2[4] == "GRND")
                                txtfrecuencia_GRND.Text = Data_Frec[4].ToString();

                            if (Data_Frec2[4] == "EMERGENCY")
                                txtfrecuencia_EMERGENCY.Text = Data_Frec[4].ToString();

                            if (Data_Frec2[4] == "Otras")
                                txtfrecuencia_Otras.Text = Data_Frec[4].ToString();
                        }
                        #endregion
                        txtUbicacion.Text = airportId.Contacto.DireccionExacta;
                        txtTelefono1.Text = airportId.Contacto.NumeroTelefono1;
                        txtTelefono2.Text = (string)airportId.Contacto.NumeroTelefono2;
                        txtHorario.Text = airportId.Contacto.Horario;
                        txtCoordenadas.Text = airportId.Caracteristicas_Especiales.Coordenada.ToString();
                        txtinfo_torre.Text = airportId.Caracteristicas_Especiales.InfoTorre;
                        txtinfo_general.Text = airportId.Caracteristicas_Especiales.InfoGeneral;
                        txtEspacio_Aereo.Text = airportId.Caracteristicas_Especiales.EspacioAereo;
                        txtCombustible.Text = airportId.Caracteristicas_Especiales.Combustible;
                        txtNormas_Generales.Text = airportId.Caracteristicas_Especiales.NormaGeneral;
                        txtNormas_Particulares.Text = airportId.Caracteristicas_Especiales.NormaParticular;
                        txtEstado_Aeropuerto.Text = airportId.Aeropuerto.EstadoAeropuerto;
                        if ((airportId.NOTAMS != null) && airportId.NOTAMS.Any())
                        {
                            foreach (Airport_Frequencies fre in airportId.Frecuencias)
                            {
                                txtNOTAMS.Text = string.Join("\n", airportId.NOTAMS.Select(c => c.NotamNotam));
                            }
                        }
                        else
                        {
                            txtNOTAMS.Text = "No disponible";
                        }
                    }
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                await DialogService.ShowErrorAsync("Error", ex.Message, "OK");
            }
        }
        private void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Notificación", "¿Realmente Desea guardar los datos?", "Si", "No");
                if (result)
                {
                    try
                    {
                        #region "Validations"
                        if (String.IsNullOrWhiteSpace(txtNombre_aeropuerto.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Nombre Aeropuerto es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtNombre_OACI.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Nombre OACI es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtNombre_ICAO.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Nombre ICAO es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtPistas.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Pistas es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtCoordenadas.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Coordenadas es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtUbicacion.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Ubicación es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtElevación.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Elevación es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtASDA_RWAY_1.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo ASDA RWAY 1 es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtASDA_RWAY_2.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo ASDA RWAY 2 es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtTODA_RWAY_1.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo TODA RWAY 1 es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtTODA_RWAY_2.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo TODA RWAY 2 es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtTORA_RWAY_1.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo TORA RWAY 1 es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtTORA_RWAY_2.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo TORA RWAY 2 es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtLDA_RWAY_1.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo LDA RWAY 1 es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtLDA_RWAY_2.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo LDA RWAY 2 es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtinfo_torre.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Información Torre es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtinfo_general.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Información General es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtTelefono1.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Telefono 1 es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtTelefono2.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Telefono 2 es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtHorario.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Horario es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtNormas_Generales.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Normas Generales es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtNormas_Particulares.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Normas Particulares es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtEspacio_Aereo.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Espacio Aéreo es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtCombustible.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Combustible es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtNOTAMS.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo NOTAMS es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtSuperficie_Pista.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Superficie Pista es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtEstado_Aeropuerto.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Estado Aeropuerto es obligatorio", "OK");
                        }
                        else
                        {
                            #endregion
                            Airport_Detail airportId = await AirportServices.getAnAirportById(Convert.ToInt32(_idAirport));
                            Airport_Update airportX = new Airport_Update
                            {
                                ID_Aeropuerto = Convert.ToInt32(_idAirport),
                                Nombre = txtNombre_aeropuerto.Text,
                                Nombre_OACI = txtNombre_OACI.Text,
                                NombreICAO = txtNombre_ICAO.Text,
                                Usario = _cedula,
                                Espacio_Aereo = txtEspacio_Aereo.Text,
                                Estado_Aeropuerto = txtEstado_Aeropuerto.Text,
                                Notam = txtNOTAMS.Text,
                                Publico = pickerPublic.SelectedIndex.ToString(),
                                Controlado = pickerControl.SelectedIndex.ToString(),
                                Info_Torre = txtinfo_torre.Text,
                                Info_General = txtinfo_general.Text,
                                Combustible = txtCombustible.Text,
                                Norma_General = txtNormas_Generales.Text,
                                Norma_Particular = txtNormas_Particulares.Text,
                                Coordenada = txtCoordenadas.Text,
                                Direccion_Exacta = txtUbicacion.Text,
                                Numero_Telefono1 = txtTelefono1.Text,
                                Numero_Telefono2 = txtTelefono2.Text,
                                Horario = txtHorario.Text,
                                ATIS = txtfrecuencia_ATIS.Text,
                                TWR = txtfrecuencia_TWR.Text,
                                GRND = txtfrecuencia_GRND.Text,
                                EMERGENCY = txtfrecuencia_EMERGENCY.Text,
                                Otras = txtfrecuencia_Otras.Text,
                                Pista = txtPistas.Text,
                                Elevacion = txtElevación.Text,
                                Superficie_Pista = txtSuperficie_Pista.Text,
                                ASDA_Rwy_1 = Convert.ToInt16(txtASDA_RWAY_1.Text),
                                ASDA_Rwy_2 = Convert.ToInt16(txtASDA_RWAY_2.Text),
                                TODA_Rwy_1 = Convert.ToInt16(txtTODA_RWAY_1.Text),
                                TODA_Rwy_2 = Convert.ToInt16(txtTODA_RWAY_2.Text),
                                TORA_Rwy_1 = Convert.ToInt16(txtTORA_RWAY_1.Text),
                                TORA_Rwy_2 = Convert.ToInt16(txtTORA_RWAY_2.Text),
                                LDA_Rwy_1 = Convert.ToInt16(txtLDA_RWAY_1.Text),
                                LDA_Rwy_2 = Convert.ToInt16(txtLDA_RWAY_2.Text)
                            };
                            #region "validations"
                            if (airportX.ATIS == "No disponible" || airportX.ATIS == "")
                            {
                                airportX.ATIS = "0.00";
                            }
                            if (airportX.TWR == "No disponible" || airportX.TWR == "")
                            {
                                airportX.TWR = "0.00";
                            }
                            if (airportX.GRND == "No disponible" || airportX.GRND == "")
                            {
                                airportX.GRND = "0.00";
                            }
                            if (airportX.EMERGENCY == "No disponible" || airportX.EMERGENCY == "")
                            {
                                airportX.EMERGENCY = "0.00";
                            }
                            if (airportX.Otras == "No disponible" || airportX.Otras == "")
                            {
                                airportX.Otras = "0.00";
                            }
                            #endregion
                            //storage the list of frequencies and the list of type_frequencies in two array
                            string[] Data_FrecPut = string.Join("\n", airportId.Frecuencias.Select(c => c.FrecuenciaFrecuencia)).Split(Convert.ToChar("\n"));
                            string[] Data_FrecPut2 = string.Join("\n", airportId.Frecuencias.Select(c => c.TipoFrecuencia)).Split(Convert.ToChar("\n"));
                            string[] Ejecutables = new string[6];
                            #region "Data validation"

                            if ((airportX.Nombre_OACI) != (airportId.Aeropuerto.NombreOaci) || (airportX.NombreICAO) != (airportId.Aeropuerto.NombreIcao) ||
                                (airportX.Estado_Aeropuerto) != (airportId.Aeropuerto.EstadoAeropuerto))
                            {
                                Ejecutables[0] = "1";
                            }
                            else
                            {
                                Ejecutables[0] = "0";
                            }

                            if ((airportX.Notam.ToString()) != string.Join("\n", airportId.NOTAMS.Select(c => c.NotamNotam))) 
                            {
                                Ejecutables[1] = "2";
                            }
                            else
                            {
                                Ejecutables[1] = "0";
                            }

                            if ((airportX.Publico) != (airportId.Caracteristicas_Especiales.Publico.ToString()) || (airportX.Controlado) != (airportId.Caracteristicas_Especiales.Controlado.ToString()) ||
                                (airportX.Coordenada) != (airportId.Caracteristicas_Especiales.Coordenada) || (airportX.Info_Torre) != (airportId.Caracteristicas_Especiales.InfoTorre) ||
                                (airportX.Info_General) != (airportId.Caracteristicas_Especiales.InfoGeneral) || (airportX.Espacio_Aereo) != (airportId.Caracteristicas_Especiales.EspacioAereo) ||
                                (airportX.Combustible) != (airportId.Caracteristicas_Especiales.Combustible) || (airportX.Norma_General) != (airportId.Caracteristicas_Especiales.NormaGeneral) ||
                                (airportX.Norma_Particular) != (airportId.Caracteristicas_Especiales.NormaParticular))
                            {
                                Ejecutables[2] = "3";
                            }
                            else
                            {
                                Ejecutables[2] = "0";
                            }

                            if ((airportX.Direccion_Exacta) != (airportId.Contacto.DireccionExacta) ||
                                (airportX.Numero_Telefono1) != (airportId.Contacto.NumeroTelefono1) ||
                                (airportX.Numero_Telefono2) != (string)(airportId.Contacto.NumeroTelefono2) ||
                                (airportX.Horario.ToString()) != (airportId.Contacto.Horario))
                            {
                                Ejecutables[3] = "4";
                            }
                            else
                            {
                                Ejecutables[3] = "0";
                            }

                            if ((Data_FrecPut.Length == 5) && (Data_FrecPut2.Length == 5))
                            {
                                if (Data_FrecPut2[0] == "TWR")
                                {
                                    if (airportX.TWR != Data_FrecPut[0])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[0] == "ATIS")
                                {
                                    if (airportX.ATIS != Data_FrecPut[0])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[0] == "GRND")
                                {
                                    if (airportX.GRND != Data_FrecPut[0])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[0] == "EMERGENCY")
                                {
                                    if (airportX.EMERGENCY != Data_FrecPut[0])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[0] == "Otras")
                                {
                                    if (airportX.Otras != Data_FrecPut[0])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[1] == "TWR")
                                {
                                    if (airportX.TWR != Data_FrecPut[1])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[1] == "ATIS")
                                {
                                    if (airportX.ATIS != Data_FrecPut[1])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[1] == "GRND")
                                {
                                    if (airportX.GRND != Data_FrecPut[1])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[1] == "EMERGENCY")
                                {
                                    if (airportX.EMERGENCY != Data_FrecPut[1])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[1] == "Otras")
                                {
                                    if (airportX.Otras != Data_FrecPut[1])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[2] == "TWR")
                                {
                                    if (airportX.TWR != Data_FrecPut[2])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[2] == "ATIS")
                                {
                                    if (airportX.ATIS != Data_FrecPut[2])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[2] == "GRND")
                                {
                                    if (airportX.GRND != Data_FrecPut[2])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[2] == "EMERGENCY")
                                {
                                    if (airportX.EMERGENCY != Data_FrecPut[2])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[2] == "Otras")
                                {
                                    if (airportX.Otras != Data_FrecPut[2])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[3] == "TWR")
                                {
                                    if (airportX.TWR != Data_FrecPut[3])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[3] == "ATIS")
                                {
                                    if (airportX.ATIS != Data_FrecPut[3])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[3] == "GRND")
                                {
                                    if (airportX.GRND != Data_FrecPut[3])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[3] == "EMERGENCY")
                                {
                                    if (airportX.EMERGENCY != Data_FrecPut[3])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[3] == "Otras")
                                {
                                    if (airportX.Otras != Data_FrecPut[3])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[4] == "TWR")
                                {
                                    if (airportX.TWR != Data_FrecPut[4])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[4] == "ATIS")
                                {
                                    if (airportX.ATIS != Data_FrecPut[4])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[4] == "GRND")
                                {
                                    if (airportX.GRND != Data_FrecPut[4])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[4] == "EMERGENCY")
                                {
                                    if (airportX.EMERGENCY != Data_FrecPut[4])
                                        Ejecutables[4] = "5";
                                }

                                if (Data_FrecPut2[4] == "Otras")
                                {
                                    if (airportX.Otras != Data_FrecPut[4])
                                        Ejecutables[4] = "5";
                                }
                            }
                            else
                            {
                                Ejecutables[4] = "0";
                            }

                            if ((airportX.Pista) != (airportId.Pistas.Pista) || (airportX.Elevacion.ToString()) != (airportId.Pistas.Elevacion) ||
                                (airportX.Superficie_Pista.ToString()) != (airportId.Pistas.SuperficiePista) || (airportX.ASDA_Rwy_1.ToString()) != (airportId.Pistas.AsdaRwy1.ToString()) ||
                                (airportX.ASDA_Rwy_2.ToString()) != (airportId.Pistas.AsdaRwy2.ToString()) || (airportX.TODA_Rwy_1.ToString()) != (airportId.Pistas.TodaRwy1.ToString()) ||
                                (airportX.TODA_Rwy_2.ToString()) != (airportId.Pistas.TodaRwy2.ToString()) || (airportX.TORA_Rwy_1.ToString()) != (airportId.Pistas.ToraRwy1.ToString()) ||
                                (airportX.TORA_Rwy_2.ToString()) != (airportId.Pistas.ToraRwy2.ToString()) || (airportX.LDA_Rwy_1.ToString()) != (airportId.Pistas.LdaRwy1.ToString()) ||
                                (airportX.LDA_Rwy_2.ToString()) != (airportId.Pistas.LdaRwy2.ToString()))
                            {
                                Ejecutables[5] = "6";
                            }
                            else
                            {
                                Ejecutables[5] = "0";
                            }
                            #endregion
                            if (await AirportServices.PutAirportAsync(string.Join("", Ejecutables), airportX.ID_Aeropuerto, airportX) == 1)
                            {
                                NotificationsServices.sendNotification(Convert.ToInt32(airportId.Aeropuerto.IdAeropuerto), airportId.Aeropuerto.Nombre);
                                await DisplayAlert("Notificación", "Los datos se han modificado con éxito", "OK");
                                Application.Current.MainPage = new MainPage();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await DialogService.ShowErrorAsync("Notificacion", ex.Message, "OK");
                        await this.Navigation.PopModalAsync();
                    }
                }
                else
                {
                    OnAppearing();
                }
            });
        }
        // this code validates the functionality of the hardware backbutton 
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Notificación", "¿Realmente Desea guardar los datos?", "Si", "No");
                if (result)
                {
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    Application.Current.MainPage = new MainPage();
                }
            });
            return true;
        }

        void OnTapGestureRecognizerTappedPublic(object sender, EventArgs args)
        {
            tapCount++;
            var imageSender = (Image)sender;

            if (tapCount % 2 == 0)
            {
                pickerPublic.Focus();
            }
            else
            {
                pickerPublic.Focus();
            }
        }

        void OnTapGestureRecognizerTappedControl(object sender, EventArgs args)
        {
            tapCount++;
            var imageSender = (Image)sender;

            if (tapCount % 2 == 0)
            {
                pickerControl.Focus();
            }
            else
            {
                pickerControl.Focus();
            }
        }

        private void btnlogout_Clicked(object sender, EventArgs e)
        {
            //App.Current.Logout();
            Application.Current.MainPage = new MainPage();
        }
        private List<Airport_Update> GetPublic()
        {
            return new List<Airport_Update>
            {
                new Airport_Update{IdP=0,NameP="NO"},
                new Airport_Update{IdP=1,NameP="SI"},
            };
        }
        private List<Airport_Update> GetControl()
        {
            return new List<Airport_Update>
            {
                new Airport_Update{IdC=0,NameC="NO"},
                new Airport_Update{IdC=1,NameC="SI"},
            };
        }
    }
}
