using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace CIIADHEL_CR.services
{
    public class NotificationsServices
    {
        private static string _url = @"https://site--nuvian-api--lzg9n5zsl8j8.code.run/api/notifications"; //url on heroku
        #region Guardar el token -->saveToken(string identifier, string token)
        public async static Task<bool> saveToken(string identifier, string token)
        {
            try
            {
                var JsonDate = new { identifier = identifier, token = token };//json var
                string json = JsonConvert.SerializeObject(JsonDate);
                HttpClient client = new HttpClient();  // Call a end-point for get all airports
                HttpResponseMessage response = await client.PostAsync(_url + "/tokens", new StringContent(
                json, Encoding.UTF8, "application/json"));
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();// Save a new token with mac address
                    JObject parseResponse = JObject.Parse(responseBody);
                    return Convert.ToBoolean(parseResponse["ok"].ToString());
                }
                else
                {
                    throw new Exception("Error al guardar el token");//display error 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);//error on console
                throw new Exception("Error en el Token");
            }
        }
        #endregion
        //****************************************************************************************
        #region Existe Identificador -->existsIdentifier(string identifier)
        public async static Task<bool> existsIdentifier(string identifier)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(_url + "/identifier/" + identifier);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();// Save a new token with mac address
                    JObject parseResponse = JObject.Parse(responseBody);
                    return Convert.ToBoolean(parseResponse["exists"].ToString());
                }
                else
                {
                    throw new Exception("Error en la validacion del identifcador");//display error
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);//error on console
                throw new Exception("Error en la validacion del identificador");
            }
        }
        #endregion
        //****************************************************************************************
        #region Enviar Notificacion -->sendNotification(int idAirport, string nameAirport)
        public async static void sendNotification(int idAirport, string nameAirport)
        {
            try
            {
                var JsonDate = new
                {
                    idAirport = idAirport,
                    title = (nameAirport.Contains("Aeropuerto"))
                            ? nameAirport
                            : "Aeropuerto " + nameAirport,
                    body = (nameAirport.Contains("Aeropuerto"))
                            ? "Nuevas modificaciones en el " + nameAirport + " míralas aquí!"
                            : "Nuevas modificaciones en el Aeropuerto " + nameAirport + " míralas aquí!",
                };
                string json = JsonConvert.SerializeObject(JsonDate);
                HttpClient client = new HttpClient();// Call a end-point for send notifications
                HttpResponseMessage response = await client.PostAsync(_url, new StringContent(
                   json, Encoding.UTF8, "application/json"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
