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
    //changes made by Olman Sanchez Zuniga
    public class FileServices
    {
        public static string url = "https://ciiadhelapi.herokuapp.com/api/files";//url on heroku
        #region Get File Service
        public string[] GetFileAsync(string  Airportname)
        {
            //validation made by Olman Sanchez zuniga
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    var task = Task.Run(async () =>{
                        return await client.GetAsync(url + "/file/" + Airportname);
                    });
                    HttpResponseMessage msj = task.Result;
                    if (msj.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        //as soon as the post is correct
                        var task2 = Task<string>.Run(async () =>{
                            // get back the content  
                            return await msj.Content.ReadAsStringAsync();
                        });
                        string resultstr = task2.Result;
                        JObject parseResponse = JObject.Parse(resultstr);
                        JObject airport = JObject.Parse(parseResponse["airports"].ToString());
                        String IDAeropuerto = airport["ID_Aeropuerto"].ToString();
                        String NombreAeropuerto = airport["Nombre"].ToString();
                        String NombreICAO = airport["Nombre_OACI"].ToString();
                        String NombreOACI = airport["NombreICAO"].ToString();
                        string[] session = new string[4];
                        session[0] = IDAeropuerto;
                        session[1] = NombreAeropuerto;
                        session[2] = NombreICAO;
                        session[3] = NombreOACI;
                        return session;
                    }
                    else if (msj.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception("No se encuentra el archivo en el aeropuerto" + Airportname);// Manage the error
                    }
                    else
                    {
                        throw new Exception("No se encuentra el archivo que estas buscando");// Manage the error
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("Error al buscar los archivos del aeropuerto");
            }  
        }
        #endregion
    }
}
