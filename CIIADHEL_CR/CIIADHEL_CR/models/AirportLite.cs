using SQLite;

namespace CIIADHEL_CR.models
{
    public class AirportLite
    {
        [PrimaryKey]
        public int ID_Aeropuerto { get; set; }
        public string Nombre { get; set; }
        public string Nombre_OACI { get; set; }
        public string NombreICAO { get; set; }
        public string Estado_Aeropuerto { get; set; }
        public string Ultima_Actualizacion_Aeropuerto { get; set; }
        public string Publico { get; set; }
        public string Controlado { get; set; }
        public string Coordenada { get; set; }
        public string Info_Torre { get; set; }
        public string Info_General { get; set; }
        public string Espacio_Aereo { get; set; }
        public string Combustible { get; set; }
        public string Norma_General { get; set; }
        public string Norma_Particular { get; set; }
        public string Frecuencia { get; set; }
        public string TipoFrecuencia { get; set; }
        public string Notam { get; set; }
        public string Fecha_Creacion { get; set; }
        public string FechaVencimiento { get; set; }
        public string Ultima_Actualizacion { get; set; }
        public string Usuario_Creacion { get; set; }
        public string Usuario_Actualizacion { get; set; }
        public string Pista { get; set; }
        public string Elevacion { get; set; }
        public string Superficie_Pista { get; set; }
        public int ASDA_Rwy_1 { get; set; }
        public int ASDA_Rwy_2 { get; set; }
        public int TODA_Rwy_1 { get; set; }
        public int TODA_Rwy_2 { get; set; }
        public int TORA_Rwy_1 { get; set; }
        public int TORA_Rwy_2 { get; set; }
        public int LDA_Rwy_1 { get; set; }
        public int LDA_Rwy_2 { get; set; }
        public string Direccion_Exacta { get; set; }
        public string Numero_Telefono1 { get; set; }
        public string Numero_Telefono2 { get; set; }
        public string Horario { get; set; }

    }

}
