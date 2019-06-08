using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AppBancaEnLinea.Models
{
    [Table("Servicio")]
    public class Servicio
    {
        [PrimaryKey, AutoIncrement]
        public int SER_CODIGO { get; set; }
        [MaxLength(250)]
        public string SER_DESCRIPCION { get; set; }
        [MaxLength(250)]
        public string SER_ESTADO { get; set; }

        public Servicio()
        {

        }

    }
}
