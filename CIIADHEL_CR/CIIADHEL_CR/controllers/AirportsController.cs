using CIIADHEL_CR.models;
using CIIADHEL_CR.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
namespace CIIADHEL_CR.controllers
{
    public class AirportsController
    {
        #region Verificacion de aeropuerto ->verificationAirport(int id)
        public async Task<Airport_Detail> verificationAirport(int id)
        {
            try//validations
            {
                NetworkAccess currentNetwork = Connectivity.NetworkAccess;
                if (currentNetwork == NetworkAccess.Internet)// Connection
                {
                    if (await existsAirport(id) == false)// Exists in SQLite
                    {
                        if (await saveDataAirport(id, true) != 1)// Save a new airport data
                        {
                            throw new Exception("Error en el sqlLite");//display error on screen
                        }
                        // Get new airport data
                        return await getAnAirportOnline(id);
                    }
                    else
                    {
                        // if exists compare date if not equals date save data updated if equals get SQLite
                        string StrlastDateSQLite = App.SQLiteDBAirports.GetLastDateAirportsAsync(id);
                        bool isUpdate = await AirportServices.getisUpdate(id, StrlastDateSQLite);
                        if (isUpdate)// True: if need updated
                        {
                            if (await saveDataAirport(id) != 1)// Save a updated airport data
                            {
                                throw new Exception("Error en el sqlLite");//display error on screen
                            }
                        }
                        return await getAnAirportOnline(id); // Get new airport data
                    }
                }
                else
                {
                    if (await existsAirport(id) == false)// in case you dont exist so
                    {
                        throw new Exception("Aeropuerto no existe");//show error on screen
                    }
                    return await getAnAirportOnline(id);// Get new airport data
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);//display error on console
                throw new Exception("Error al cargar los aeropuertos");
            }
        }
        #endregion
        //************************************************************************
        #region Descarga de aeropuerto downloadAirport(int id)
        public async Task<bool> downloadAirport(int id)
        {
            try
            {
                NetworkAccess currentNetwork = Connectivity.NetworkAccess;
                if (currentNetwork != NetworkAccess.Internet)//internet
                {
                    throw new Exception("No conexion en Internet");// display error 
                }
                if (await existsAirport(id) == false)// if it doesn't exist the Airport
                {
                    if (await saveDataAirport(id, true) == 0) // Save new data
                    {
                        throw new Exception("Error al guardar un aeropuerto"); //display error on screen
                    }
                    return true;
                }
                else
                {
                    // Check last date update
                    string StrlastDateSQLite = App.SQLiteDBAirports.GetLastDateAirportsAsync(id);
                    bool isUpdate = await AirportServices.getisUpdate(id, StrlastDateSQLite);
                    if (isUpdate) //if update is true you will save data
                    {
                        if (await saveDataAirport(id) == 0)//await to save information
                        {
                            throw new Exception("Error al guardar un nuevo aeropuerto");// display error on screen
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        //************************************************************************
        #region Validacion de existe aeropuerto-->existsAirport(int id)
        private async Task<bool> existsAirport(int id)
        {
            try
            {
                if (await App.SQLiteDBAirports.GetExistsAirportsAsync(id) != 1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        //************************************************************************
        #region Obtiene aeropuerto -->getAnAirport(int id)
        private async Task<Airport_Detail> getAnAirport(int id)
        {
            try
            {
                // Get new airport data
                AirportLite airportLite = await App.SQLiteDBAirports.GetAnAirportByIDAsync(id);

                // Desearialize data
                return desealizeAirportSQLite(airportLite);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        //*************************************************************************
        #region Obtiene aeropuerto -->getAnAirportOnline(int id)
        public async Task<Airport_Detail> getAnAirportOnline(int id)
        {
            try
            {
                Airport_Detail airport_Detail = await AirportServices.getAnAirportById(id);

                return airport_Detail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        //*************************************************************************
        #region Guardar informacion del aeropuerto ->saveDataAirport(int id, bool isNewData = false)
        private async Task<int> saveDataAirport(int id, bool isNewData = false)
        {
            try
            {
                string frecuencias = "";
                string tipoFrecuencias = "";
                string notam = "";
                string fechaCreacion = "";
                string fechaVencimiento = "";
                string ultimaActualizacion = "";
                string usuarioCreacion = "";
                string usuarioActualizacion = "";
                Airport_Detail airport_Detail = await AirportServices.getAnAirportById(id);
                foreach (Airport_Frequencies fre in airport_Detail.Frecuencias)
                {
                    frecuencias += fre.FrecuenciaFrecuencia + ",";
                    tipoFrecuencias += fre.TipoFrecuencia + ",";
                }
                foreach (Airport_NOTAMS not in airport_Detail.NOTAMS)
                {
                    notam += not.NotamNotam + ",";
                    fechaCreacion += not.FechaCreacion + ",";
                    fechaVencimiento += not.FechaVencimiento + ",";
                    ultimaActualizacion += not.UltimaActualizacion + ",";
                    usuarioCreacion += not.UsuarioCreacion + ",";
                    usuarioActualizacion += not.UsuarioActualizacion + ",";
                }
                AirportLite airportLite = new AirportLite()
                {
                    ID_Aeropuerto = (int)airport_Detail.Aeropuerto.IdAeropuerto,
                    Nombre = airport_Detail.Aeropuerto.Nombre,
                    Nombre_OACI = airport_Detail.Aeropuerto.NombreOaci,
                    NombreICAO = airport_Detail.Aeropuerto.NombreIcao,
                    Estado_Aeropuerto = airport_Detail.Aeropuerto.EstadoAeropuerto,
                    Ultima_Actualizacion_Aeropuerto = airport_Detail.Aeropuerto.UltimaActualizacion,
                    Publico = airport_Detail.Caracteristicas_Especiales.Publico.ToString(),
                    Controlado = airport_Detail.Caracteristicas_Especiales.Controlado.ToString(),
                    Coordenada = airport_Detail.Caracteristicas_Especiales.Coordenada,
                    Info_Torre = airport_Detail.Caracteristicas_Especiales.InfoTorre,
                    Info_General = airport_Detail.Caracteristicas_Especiales.InfoGeneral,
                    Espacio_Aereo = airport_Detail.Caracteristicas_Especiales.EspacioAereo,
                    Combustible = airport_Detail.Caracteristicas_Especiales.Combustible,
                    Norma_General = airport_Detail.Caracteristicas_Especiales.NormaGeneral,
                    Norma_Particular = airport_Detail.Caracteristicas_Especiales.NormaParticular,
                    Frecuencia = frecuencias,
                    TipoFrecuencia = tipoFrecuencias,
                    Notam = notam,
                    Fecha_Creacion = fechaCreacion,
                    FechaVencimiento = fechaVencimiento,
                    Ultima_Actualizacion = ultimaActualizacion,
                    Usuario_Actualizacion = usuarioActualizacion,
                    Usuario_Creacion = usuarioCreacion,
                    Pista = airport_Detail.Pistas.Pista,
                    Elevacion = airport_Detail.Pistas.Elevacion,
                    Superficie_Pista = airport_Detail.Pistas.SuperficiePista,
                    ASDA_Rwy_1 = airport_Detail.Pistas.AsdaRwy1,
                    ASDA_Rwy_2 = airport_Detail.Pistas.AsdaRwy2,
                    TODA_Rwy_1 = airport_Detail.Pistas.TodaRwy1,
                    TODA_Rwy_2 = airport_Detail.Pistas.TodaRwy2,
                    TORA_Rwy_1 = airport_Detail.Pistas.ToraRwy1,
                    TORA_Rwy_2 = airport_Detail.Pistas.ToraRwy2,
                    LDA_Rwy_1 = airport_Detail.Pistas.LdaRwy1,
                    LDA_Rwy_2 = airport_Detail.Pistas.LdaRwy2,
                    Direccion_Exacta = airport_Detail.Contacto.DireccionExacta,
                    Numero_Telefono1 = airport_Detail.Contacto.NumeroTelefono1,
                    Numero_Telefono2 = (string)airport_Detail.Contacto.NumeroTelefono2,
                    Horario = airport_Detail.Contacto.Horario,
                };
                return await App.SQLiteDBAirports.SaveAirportAsync(airportLite, isNewData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        //*********************************************************************************
        #region Desearilizacion del aeropuerto desealizeAirportSQLite(AirportLite airportLite)
        private Airport_Detail desealizeAirportSQLite(AirportLite airportLite)
        {
            try
            {
                Airport airport = new Airport()
                {
                    IdAeropuerto = airportLite.ID_Aeropuerto,
                    Nombre = airportLite.Nombre,
                    NombreOaci = airportLite.Nombre_OACI,
                    NombreIcao = airportLite.NombreICAO,
                    EstadoAeropuerto = airportLite.Estado_Aeropuerto,
                    UltimaActualizacion = airportLite.Ultima_Actualizacion_Aeropuerto
                };

                Airport_Features features = new Airport_Features()
                {
                    Publico = Convert.ToInt64(airportLite.Publico),
                    Controlado = Convert.ToInt64(airportLite.Controlado),
                    Coordenada = airportLite.Coordenada,
                    InfoTorre = airportLite.Info_Torre,
                    InfoGeneral = airportLite.Info_General,
                    EspacioAereo = airportLite.Espacio_Aereo,
                    Combustible = airportLite.Combustible,
                    NormaGeneral = airportLite.Norma_General,
                    NormaParticular = airportLite.Norma_Particular
                };

                List<Airport_Frequencies> _Frequencies = new List<Airport_Frequencies>();
                string[] frequencies = airportLite.Frecuencia.Split(',');
                string[] typeFrequencies = airportLite.TipoFrecuencia.Split(',');

                for (int i = 0; i <= frequencies.Length - 2; i++)
                {
                        _Frequencies.Add(new Airport_Frequencies()
                        {
                        FrecuenciaFrecuencia = frequencies[i],
                        TipoFrecuencia = typeFrequencies[i]
                        });
                    }

                List<Airport_NOTAMS> _Notams = new List<Airport_NOTAMS>();
                string[] notam = airportLite.Notam.Split(',');
                string[] fechaCreacion = airportLite.Fecha_Creacion.Split(',');
                string[] fechaVencimiento = airportLite.FechaVencimiento.Split(',');
                string[] ultimaActualizacion = airportLite.Ultima_Actualizacion.Split(',');
                string[] usuarioCreacion = airportLite.Usuario_Creacion.Split(',');
                string[] usuarioActualizacion = airportLite.Usuario_Actualizacion.Split(',');

                for (int i = 0; i <= notam.Length - 2; i++)
                {
                    _Notams.Add(new Airport_NOTAMS()
                    {
                        NotamNotam = notam[i],
                        FechaCreacion = DateTimeOffset.Parse(fechaCreacion[i]),
                        FechaVencimiento = DateTimeOffset.Parse(fechaVencimiento[i]),
                        UltimaActualizacion = DateTimeOffset.Parse(ultimaActualizacion[i]),
                        UsuarioCreacion = Convert.ToInt64(usuarioCreacion[i]),
                        UsuarioActualizacion = Convert.ToInt64(usuarioActualizacion[i]),
                    });
                }

                Airport_Runways runways = new Airport_Runways()
                {
                    Pista = airportLite.Pista,
                    Elevacion = airportLite.Elevacion,
                    SuperficiePista = airportLite.Superficie_Pista,
                    AsdaRwy1 = airportLite.ASDA_Rwy_1,
                    AsdaRwy2 = airportLite.ASDA_Rwy_2,
                    TodaRwy1 = airportLite.TODA_Rwy_1,
                    TodaRwy2 = airportLite.TODA_Rwy_2,
                    ToraRwy1 = airportLite.TORA_Rwy_1,
                    ToraRwy2 = airportLite.TORA_Rwy_2,
                    LdaRwy1 = airportLite.LDA_Rwy_1,
                    LdaRwy2 = airportLite.LDA_Rwy_2,
                };

                Airport_Contact contact = new Airport_Contact()
                {
                    DireccionExacta = airportLite.Direccion_Exacta,
                    NumeroTelefono1 = airportLite.Numero_Telefono1,
                    NumeroTelefono2 = airportLite.Numero_Telefono2,
                    Horario = airportLite.Horario
                };

                return new Airport_Detail()
                {
                    Aeropuerto = airport,
                    Caracteristicas_Especiales = features,
                    Frecuencias = _Frequencies,
                    NOTAMS = _Notams,
                    Pistas = runways,
                    Contacto = contact
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
