using Newtonsoft.Json;

namespace CIIADHEL_CR.models
{
    public partial class Airport_Contact
    {
        [JsonProperty("ID_Contacto")]
        public long IdContacto { get; set; }

        [JsonProperty("ID_Aeropuerto")]
        public long IdAeropuerto { get; set; }

        [JsonProperty("Direccion_Exacta")]
        public string DireccionExacta { get; set; }

        [JsonProperty("Numero_Telefono1")]
        public string NumeroTelefono1 { get; set; }

        [JsonProperty("Numero_Telefono2")]
        public object NumeroTelefono2 { get; set; }

        [JsonProperty("Numero_Telefono3")]
        public object NumeroTelefono3 { get; set; }

        [JsonProperty("Horario")]
        public string Horario { get; set; }
    }
}
