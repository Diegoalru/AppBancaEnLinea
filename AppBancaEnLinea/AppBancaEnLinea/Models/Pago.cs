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

        public string PAG_DESCRIPCION_label
        {
            get
            {
                foreach (var item in App.repositorioServicio.ObtenerServicios())
                {
                    if (SER_CODIGO == item.SER_CODIGO)
                    {
                        return string.Format("Pago: {0} | Servicio: {1} | Saldo: {2} {3:N2}",
                            PAG_CODIGO,
                            item.SER_DESCRIPCION,
                            (PAG_MONEDA.Trim().Equals("DOL") ? "$" : PAG_MONEDA.Trim().Equals("COL") ? "₡" : "€"),
                            PAG_MONTO);
                    }
                }
                return string.Format("Pago: {0} | Saldo: {1} {2:N2}",
                            PAG_CODIGO,
                            (PAG_MONEDA.Trim().Equals("DOL") ? "$" : PAG_MONEDA.Trim().Equals("COL") ? "₡" : "€"),
                            PAG_MONTO);
            }
        }
        #endregion

        public Pago()
        {

        }
    }
}
