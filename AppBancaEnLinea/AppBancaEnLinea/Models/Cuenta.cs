using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AppBancaEnLinea.Models
{
    [Table("Cuenta")]
    public class Cuenta
    {
        #region Variables
        [PrimaryKey, AutoIncrement]
        public int CUE_CODIGO { get; set; }

        [MaxLength(250)]
        public int USU_CODIGO { get; set; }

        [MaxLength(250)]
        public string CUE_DESCRIPCION { get; set; }

        [MaxLength(3)]
        public string CUE_MONEDA { get; set; }

        [MaxLength(250)]
        public decimal CUE_SALDO { get; set; }

        [MaxLength(1)]
        public string CUE_ESTADO { get; set; }
        #endregion

        public Cuenta()
        {}

    }
}
