using Newtonsoft.Json;

namespace CIIADHEL_CR.models
{
    public partial class Airport_Features
    {
        [JsonProperty("ID_CarESP_Aero")]
        public long IdCarEspAero { get; set; }

        [JsonProperty("ID_Aeropuerto")]
        public long IdAeropuerto { get; set; }

        [JsonProperty("Publico")]
        public long Publico { get; set; }

        [JsonProperty("Controlado")]
        public long Controlado { get; set; }

        [JsonProperty("Coordenada")]
        public string Coordenada { get; set; }

        [JsonProperty("Info_Torre")]
        public string InfoTorre { get; set; }

        [JsonProperty("Info_General")]
        public string InfoGeneral { get; set; }

        [JsonProperty("Espacio_Aereo")]
        public string EspacioAereo { get; set; }

        [JsonProperty("Combustible")]
        public string Combustible { get; set; }

        [JsonProperty("Norma_General")]
        public string NormaGeneral { get; set; }

        [JsonProperty("Norma_Particular")]
        public string NormaParticular { get; set; }

    }
}
