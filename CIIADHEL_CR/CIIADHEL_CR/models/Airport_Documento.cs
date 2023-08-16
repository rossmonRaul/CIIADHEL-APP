using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CIIADHEL_CR.models
{
    public class Airport_Documento
    {
        [JsonProperty("ID_Documento")]
        public long ID_Documento { get; set; }

        [JsonProperty("ID2")]
        public long ID2 { get; set; }

        [JsonProperty("ID_Aeropuerto")]
        public long IdAeropuerto { get; set; }

        [JsonProperty("nombre_pdf")]
        public string nombre_pdf { get; set; }

        [JsonProperty("Contenido")]
        public string Contenido { get; set; }

        [JsonProperty("Extension")]
        public string Extension { get; set; } 

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
