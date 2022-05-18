using Newtonsoft.Json;
using Xamarin.Forms;
namespace CIIADHEL_CR.models
{
    public class Login
    {
        /// <summary>
        /// made by Olman Sanchez Zuniga
        /// email=andreyszcr@gmail.com
        /// </summary>
        /// Json var and public var
        [JsonProperty("Cedula")]
        public string Cedula { get; set; }
        [JsonProperty("Contraseña")]
        public string Contrasena { get; set; }
        [JsonProperty("ID_Aeropuerto")]
        public string IdAeropuerto { get; set; }
        //method cosntructor
        public Login(string _Cedula, string _Contrasena, string _IdAirport)
        {

            if (Application.Current.Properties.ContainsKey("Cedula")
                || Application.Current.Properties.ContainsKey("Contraseña")
                || Application.Current.Properties.ContainsKey("ID_Aeropuerto"))
            {


                var id = Application.Current.Properties["Cedula"];
                Cedula = id.ToString();

                var password = Application.Current.Properties["Contraseña"];
                Contrasena = password.ToString();

                var IdAirport = Application.Current.Properties["ID_Aeropuerto"];
                IdAeropuerto = IdAirport.ToString();

                _Cedula = Cedula;
                _Contrasena = Contrasena;
                _IdAirport = IdAeropuerto;
            }
        }

        //method to use on API
        public string toJsonString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        public override string ToString()
        {
            return toJsonString();
        }

        //method string to return values
        public string ID()
        {
            return Cedula;
        }

        public string Password()
        {
            return Contrasena;
        }
        public string IDAirport()
        {
            return IdAeropuerto;
        }
    }
}
