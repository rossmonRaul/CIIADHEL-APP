using CIIADHEL_CR.helpers;
using CIIADHEL_CR.models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace CIIADHEL_CR.services
{
    public class AirportServices
    {
        DialogService dialog = new DialogService();
        // Official  Endpoint
        private static string _url = @"https://nuvian-api.herokuapp.com/api/airports";
        // changes made by andreyszcr@gmail.com
        private static string urlfind = "https://nuvian-api.herokuapp.com/api/airports/search";
        #region obtiene el identificador -->getFavoritebyIdentificador(string Identificador)
        public async static Task<List<Airport_Favorite>> getFavoritebyIdentificador(string Identificador)
        {
            try
            {
                // Call a end-point for get all airports
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(_url + "/favorito/" + Identificador);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Get data and create airports lists.
                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject parseResponse = JObject.Parse(responseBody);
                    return JsonConvert.DeserializeObject<List<Airport_Favorite>>(parseResponse["Recuperados"].ToString());
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new List<Airport_Favorite>();
                }
                else
                {
                    throw new Exception("Error al cargar favoritos");//display error
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); //error con soles
                throw new Exception("Error al obtener los id por medio del identificador");
            }
        }
        #endregion
        //*****************************************************************************************
        #region Postea los aeropuertos favoritos ->postFavoriteAirports
        public async static Task<bool> postFavoriteAirports(Airport_Favorite airport)
        {
            try
            {
                string json = JsonConvert.SerializeObject(airport);
                // Call a end-point 
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(_url + "/favorito/", new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    return true;
                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }
                else
                {
                    throw new Exception("Error");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);//error on console
                throw new Exception("Error al añadir a favoritos");
            }
        }
        #endregion
        //*****************************************************************************************
        #region Borra favoritos del aeropuerto ->deleteFavoriteAirports
        public async static Task<bool> deleteFavoriteAirports(int ID_Aeropuerto, string Identificador)
        {
            try
            {
                // Call a end-point 
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(_url + "/favorito/" + ID_Aeropuerto + "/" + Identificador);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Error delete a Airport");
                }
                else
                {
                    throw new Exception("Error");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);//error console
                throw new Exception("Error al quitar favoritos");
            }
        }
        #endregion
        //*****************************************************************************************
        #region obtiene validacion si existe ->getValidateExist
        public async static Task<bool> getValidateExist(int ID_Aeropuerto, string Identificador)
        {
            try
            {
                // Call a end-point 
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(_url + "/favorito/" + ID_Aeropuerto + "/" + Identificador);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }
                else
                {
                    throw new Exception("Error ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);// error en console
                throw new Exception("Error al validar el aeropuerto");
            }
        }
        #endregion
        //*****************************************************************************************
        #region Obtiene todos los aeropuertos ->getAllAirports()
        public async static Task<List<Airport_Principal>> getAllAirports()
        {
            try
            {
                // Call a end-point for get all airports
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(_url);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Get data and create airports lists.
                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject parseResponse = JObject.Parse(responseBody);
                    return JsonConvert.DeserializeObject<List<Airport_Principal>>(parseResponse["airports"].ToString());
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Aeropuerto no encontrados");//display error
                }
                else
                {
                    throw new Exception("Error al cargar aeropuertos");//display error
                }
            }
            catch (Exception ex)
            {
                // changes made by olman Sanchez Zuniga
                Console.WriteLine(ex);
                throw new Exception("Error al cargar todos los aeropuerto");
            }
        }
        #endregion
        //*****************************************************************************************
        #region Obtner el tamaño de aeropuertos ->getSizeAirports
        public async static Task<int> getSizeAirports()
        {
            try
            {
                // Call a end-point for get all airports
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(_url + "/size");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Get data and create airports lists.
                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject parseResponse = JObject.Parse(responseBody);
                    return JsonConvert.DeserializeObject<int>(parseResponse["size"].ToString());
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Aeropuertos no encontrados");//display error
                }
                else
                {
                    throw new Exception("Error al cargar el aeropuerto");//display error
                }
            }
            catch (Exception ex)
            {
                // changes made by olman Sanchez Zuniga
                Console.WriteLine(ex);
                throw new Exception("Error al cargar los aeropuertos");
            }
        }
        #endregion
        //*****************************************************************************************
        #region Actualizar ->getisUpdate
        public async static Task<bool> getisUpdate(int id, string lastDate)
        {
            try
            {
                var JsonDate = new
                {
                    lastDate = lastDate
                };
                string json = JsonConvert.SerializeObject(JsonDate);
                // Call a end-point for get all airports
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(_url + "/lastDate/" + id,
                new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Get data and create airports lists.
                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject parseResponse = JObject.Parse(responseBody);
                    return Convert.ToBoolean(parseResponse["update"].ToString());
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Aeropuerto no encontrado");//display error
                }
                else
                {
                    throw new Exception("Erro al tener la ultima actualizacion del aeropuerto");//display error
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);//error on console
                throw new Exception("Error al cargar el aeropuerto");
            }
        }
        #endregion
        //*****************************************************************************************
        #region Obtener el aeropuerto por nombre ->getAllAirportsByName
        public async static Task<List<Airport_Principal>> getAllAirportsByName(string name)
        {
            try
            {
                // Call a end-point for get all airports
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(urlfind + "/name/" + name);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Get data and create airports lists.
                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject parseResponse = JObject.Parse(responseBody);
                    return JsonConvert.DeserializeObject<List<Airport_Principal>>(parseResponse["airports"].ToString());
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("No se encontro el aeropuerto llamado:" + name);//display error
                }
                else
                {
                    throw new Exception("Error al cargar el aeropuerto");//display error
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);//error console
                throw new Exception("Error al cargar el aeropuerto");
            }
        }
        #endregion
        //*****************************************************************************************
        #region Obtener aeropuerto por codigo aeropuerto -->getAnAirportById
        public async static Task<Airport_Detail> getAnAirportById(int id)
        {
            try
            {
                // Call a end-point for get all airports
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(_url + "/id/" + id);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Get data and create airports lists.
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return Airport_Detail.FromJson(@responseBody);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("No se encontro el codigo del aeropuerto:" + id);//display error
                }
                else
                {
                    throw new Exception("Error al cargar el aeropuerto");// display error
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);//error on console
                throw new Exception("Error al cargar el aeropuerto");
            }
        }
        #endregion
        //*****************************************************************************************
        #region Obtner el aeropuerto por nombre -> getAirportsByName
        public async static Task<List<Airport_Principal>> getAirportsByName(string name)
        {
            try
            {
                HttpClient client = new HttpClient();// Call a end-point for get all airports
                HttpResponseMessage response = await client.GetAsync(_url + "/name/" + name);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Get data and create airports lists.
                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject parseResponse = JObject.Parse(responseBody);
                    return JsonConvert.DeserializeObject<List<Airport_Principal>>(parseResponse["airports"].ToString());
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("No se encuentra el aeropuerto llamado:" + name);// display error
                }
                else
                {
                    throw new Exception("Error al cargar el aeropuerto");//display error
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);// error on console
                throw new Exception("Error al cargar el aeropuerto");
            }
        }
        #endregion
        //*****************************************************************************************
        /// made by andreyszcr@gmail.com
        #region  Obtener el aeropuerto por busqueda  ->GetAirportbySearch
        public async static Task<List<Airport_Principal>> getAllAirportsBySearch(string name)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(_url + "/name/" + name);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();// Get data and create airports lists.
                    JObject parseResponse = JObject.Parse(responseBody);
                    return JsonConvert.DeserializeObject<List<Airport_Principal>>(parseResponse["airports"].ToString());
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("No se ecnontro el aeropuerto llamado" + name);//display message
                }
                else
                {
                    throw new Exception("Error al cargar el aeropuerto");//display message
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);//console error
                throw new Exception("Error, cargado el aeropuerto");// display message
            }
        }
        #endregion
        //*****************************************************************************************
        #region Insertar aeropuerto ->PutAirportAsync
        public async static Task<int> PutAirportAsync(string Ejecutables, int ID_Aeropuerto, Airport_Update airport_Data)
        {
            try
            {
                Uri RequestUri = new Uri(_url + "/update/" + Ejecutables + "/" + ID_Aeropuerto);
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(airport_Data);
                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(RequestUri, contentJson);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return 1;
                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Error updating Airport");
                }
                else
                {
                    throw new Exception("Error ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("Error updating Airports");
            }
        }
        #endregion
    }
}
