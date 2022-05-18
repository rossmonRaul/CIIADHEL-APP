using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIIADHEL_CR.models
{
    public class Identifier
    {

        [PrimaryKey]
        public int id_Identifier { get; set; }

        public string Telephone_Number { get; set; }
    }
}
