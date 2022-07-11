using CIIADHEL_CR.models;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace CIIADHEL_CR.services
{
    public class UserServices
    {
        //Url on Heroku
        private static string UrlLog = "https://nuvian-api.herokuapp.com/api/users/login";
        //Url used on apui rest on node.js
        //private static string UrlLog = "http://localhost:3033/api/users/login/";
        /// made by andreyszcr@gmail.com 
        #region Var to Log in 
        ///local var to connect with textbox.
        String cedula;
        String IDAeropuerto;
        public string msj2 { get; set; }
        #endregion
        //*************************************************************************************************
        #region Method post 
        public string[] PostLOGIN(Login login)
        {
            string json = login.toJsonString();// var connected by class User
            using (var client = new System.Net.Http.HttpClient())
            {
                var task = Task.Run(async () =>{
                    return await client.PostAsync(UrlLog + "/" + login.Cedula + "/" + login.Contrasena,
                    new StringContent(json, Encoding.UTF8, "application/json"));//paramereters Post , to login. 
                });
                HttpResponseMessage msj = task.Result;
                //if post is correct 
                if (msj.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //as soon as the post is correct
                    var task2 = Task<string>.Run(async () =>{
                        return await msj.Content.ReadAsStringAsync();// get back the content  
                    });
                    string resultstr = task2.Result;
                    JObject parseResponse = JObject.Parse(resultstr);//get data from request
                    cedula = parseResponse["cedula"].ToString();// get ID
                    JObject airport = JObject.Parse(parseResponse["airport"].ToString());// get IdAirport
                    IDAeropuerto = airport["ID_Aeropuerto"].ToString();
                    string[] session = new string[2];
                    session[0] = cedula;
                    session[1] = IDAeropuerto;
                    return session;
                }
                else if (msj.StatusCode == HttpStatusCode.NotFound)//if id is not found 
                {
                    throw new Exception("no esta registrado el usuario:" + login.Cedula);//error management
                }
                else
                {
                    throw new Exception("Error mientras se logueaba");//error management
                }
            }
        }
        #endregion
        //*************************************************************************
        #region Validations Login
        public bool ValidationLogin(Login login)
        {
            string json = login.toJsonString();// data to get data from class login 
            using (var client = new System.Net.Http.HttpClient())
            {
                var task = Task.Run(async () =>{
                    //paramereters Post , to login.   
                    return await client.PostAsync(UrlLog + "/" + login.Cedula + "/" + login.Contrasena,
                    new StringContent(json, Encoding.UTF8, "application/json"));
                });
                HttpResponseMessage msj = task.Result;
                // in case username and password is correctly 
                if (msj.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var task2 = Task<string>.Run(async () =>{
                       return await msj.Content.ReadAsStringAsync();
                    });
                    string resultstr = task2.Result;//result of api 
                    JObject parseResponse = JObject.Parse(resultstr);
                    JObject airport = JObject.Parse(parseResponse["airport"].ToString());//IdAirport
                    IDAeropuerto = Convert.ToString(airport);
                    if (IDAeropuerto == "{}") // IdAirport is more than 0 
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {return false;}
            }
        }
        #endregion
    }
}
