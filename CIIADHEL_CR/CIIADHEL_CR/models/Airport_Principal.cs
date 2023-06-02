using SQLite;

namespace CIIADHEL_CR.models
{
    public class Airport_Principal
    {
        [PrimaryKey]
        public int ID_Aeropuerto { get; set; }
        public string Nombre { get; set; }
        public string Nombre_OACI { get; set; }
        public string Estado_Aeropuerto { get; set; }
        public int Usuario_Creacion { get; set; }
        public string NombreICAO { get; set; }
        public string Direccion_Exacta { get; set; }
        public string Pista { get; set; }
        public string Coordenada { get; set; }
        public string Elevacion { get; set; }
        public string Espacio_Aereo { get; set; }
        public string Numero_Telefono1 { get; set; }
        public string Horario { get; set; }
        public bool Descargado { get; set; }

        public string Imagen
        {
            get
            {
                return Descargado ? "update4.png" : "download4.png";
            }
        }


        //-----------------------------------------//

        public bool Favorito { get; set; }

        public string ImagenFavorito
        {
            get
            {
                return Favorito ? "favorite_ok3.png" : "favorite3.png";
            }
        }

    }
}
