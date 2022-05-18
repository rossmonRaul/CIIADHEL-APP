using Newtonsoft.Json;

namespace CIIADHEL_CR.models
{
    public partial class Airport
    {
        [JsonProperty("ID_Aeropuerto")]
        public long IdAeropuerto { get; set; }

        [JsonProperty("Nombre")]
        public string Nombre { get; set; }

        [JsonProperty("Nombre_OACI")]
        public string NombreOaci { get; set; }

        [JsonProperty("NombreICAO")]
        public string NombreIcao { get; set; }

        [JsonProperty("Estado_Aeropuerto")]
        public string EstadoAeropuerto { get; set; }

        [JsonProperty("Ultima_Actualizacion")]
        public string UltimaActualizacion { get; set; }
    }
}
