using Newtonsoft.Json;

namespace CIIADHEL_CR.models
{
    public class Airport_Runways
    {
        [JsonProperty("ID_Pista")]
        public long IdPista { get; set; }

        [JsonProperty("ID_Aeropuerto")]
        public long IdAeropuerto { get; set; }

        [JsonProperty("Pista")]
        public string Pista { get; set; }

        [JsonProperty("Elevacion")]
        public string Elevacion { get; set; }

        [JsonProperty("Superficie_Pista")]
        public string SuperficiePista { get; set; }

        [JsonProperty("ASDA_Rwy_1")]
        public int AsdaRwy1 { get; set; }

        [JsonProperty("ASDA_Rwy_2")]
        public int AsdaRwy2 { get; set; }

        [JsonProperty("TODA_Rwy_1")]
        public int TodaRwy1 { get; set; }

        [JsonProperty("TODA_Rwy_2")]
        public int TodaRwy2 { get; set; }

        [JsonProperty("TORA_Rwy_1")]
        public int ToraRwy1 { get; set; }

        [JsonProperty("TORA_Rwy_2")]
        public int ToraRwy2 { get; set; }

        [JsonProperty("LDA_Rwy_1")]
        public int LdaRwy1 { get; set; }

        [JsonProperty("LDA_Rwy_2")]
        public int LdaRwy2 { get; set; }

    }
}
