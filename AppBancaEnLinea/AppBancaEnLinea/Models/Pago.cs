using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppBancaEnLinea.Models
{
    [Table("Pago")]
    public class Pago
    {
        #region Variables
        [PrimaryKey, AutoIncrement]
        public int PAG_CODIGO { get; set; }

        [MaxLength(4)]
        public int SER_CODIGO { get; set; }

        [MaxLength(4)]
        public int CUE_CODIGO { get; set; }

        public DateTime PAG_FECHA { get; set; }

        [MaxLength(3)]
        public string PAG_MONEDA { get; set; }

        [MaxLength(12)]
        public decimal PAG_MONTO { get; set; }
        #endregion

        public Pago()
        {

        }
    }
}
