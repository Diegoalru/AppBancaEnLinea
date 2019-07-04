using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AppBancaEnLinea.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int USU_CODIGO { get; set; }

        [MaxLength(250)]
        public string USU_IDENTIFICACION { get; set; }

        [MaxLength(250)]
        public string USU_NOMBRE { get; set; }

        [MaxLength(250)]
        public string USU_USERNAME { get; set; }

        [MaxLength(250)]
        public string USU_PASSWORD { get; set; }

        [MaxLength(250)]
        public string USU_EMAIL { get; set; }
        /*
         * FALTA ATRIBUTO
         */
        public DateTime USU_FEC_NAC { get; set; }
        [MaxLength(1)]
        public string USU_ESTADO { get; set; }
        /*
         * FALTA ATRIBUTO
         */
        //public SecurityToken Token { get; set; }
        //[MaxLength(250)]
        //public string TOKEN { get; set; }

        #region Constructores
        public Usuario()
        {}

        public Usuario(int code, string user)
        { this.USU_CODIGO = code; this.USU_USERNAME = user; }
        #endregion


        public override string ToString()
        {
            return string.Format("Verifique los datos:\nCedula: {0}\nNombre: {1}\nUsuario: {2}\nEmail: {3}\n", USU_IDENTIFICACION, USU_NOMBRE, USU_USERNAME, USU_EMAIL);
        }

    }
}
