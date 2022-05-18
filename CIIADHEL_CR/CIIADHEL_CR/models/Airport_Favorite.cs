using System;
using System.Collections.Generic;
using System.Text;

namespace CIIADHEL_CR.models
{
    public class Airport_Favorite
    {
        public int ID_Aeropuerto { get; set; }
        public string Identificador { get; set; }
        public string Nombre { get; set; }
        public string Nombre_OACI { get; set; }
        public int Usuario_Creacion { get; set; }
        public int Usuario_Actualizacion { get; set; }
        public string NombreICAO { get; set; }
       

    }
}
