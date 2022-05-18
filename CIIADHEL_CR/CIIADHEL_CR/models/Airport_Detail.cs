using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CIIADHEL_CR.models
{
    public partial class Airport_Detail
    {
        [JsonProperty("Aeropuerto")]
        public Airport Aeropuerto { get; set; }

        [JsonProperty("Caracteristicas_Especiales")]
        public Airport_Features Caracteristicas_Especiales { get; set; }

        [JsonProperty("Frecuencias")]
        public List<Airport_Frequencies> Frecuencias { get; set; }

        [JsonProperty("NOTAMS")]
        public List<Airport_NOTAMS> NOTAMS { get; set; }

        [JsonProperty("Pistas")]
        public Airport_Runways Pistas { get; set; }

        [JsonProperty("Contacto")]
        public Airport_Contact Contacto { get; set; }

    }

    public partial class Airport_Detail
    {
        public static Airport_Detail FromJson(string json) => JsonConvert.DeserializeObject<Airport_Detail>(json, CIIADHEL_CR.models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Airport_Detail self) => JsonConvert.SerializeObject(self, CIIADHEL_CR.models.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
