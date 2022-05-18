using Newtonsoft.Json;

namespace CIIADHEL_CR.models
{
    public partial class Airport_Frequencies
    {
        [JsonProperty("ID_Frecuencia")]
        public long IdFrecuencia { get; set; }

        [JsonProperty("Frecuencia")]
        public string FrecuenciaFrecuencia { get; set; }

        [JsonProperty("TipoFrecuencia")]
        public string TipoFrecuencia { get; set; }
    }
}
