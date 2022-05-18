using Newtonsoft.Json;
using System;

namespace CIIADHEL_CR.models
{
    public class Airport_NOTAMS
    {
        [JsonProperty("ID_NOTAMS")]
        public long IdNotams { get; set; }

        [JsonProperty("ID_Aeropuerto")]
        public long IdAeropuerto { get; set; }

        [JsonProperty("Notam")]
        public string NotamNotam { get; set; }

        [JsonProperty("Fecha_Creacion")]
        public DateTimeOffset FechaCreacion { get; set; }

        [JsonProperty("FechaVencimiento")]
        public object FechaVencimiento { get; set; }

        [JsonProperty("Ultima_Actualizacion")]
        public DateTimeOffset UltimaActualizacion { get; set; }

        [JsonProperty("Usuario_Creacion")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long UsuarioCreacion { get; set; }

        [JsonProperty("Usuario_Actualizacion")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long UsuarioActualizacion { get; set; }

    }
}
