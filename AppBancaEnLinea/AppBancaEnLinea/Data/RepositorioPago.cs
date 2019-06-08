using System;
using System.Collections.Generic;
using SQLite;
using AppBancaEnLinea.Models;

namespace AppBancaEnLinea.Data
{
    public class RepositorioPago
    {
        #region Variables
        private readonly SQLiteConnection connection;
        public string StatusMessage { get; set; }
        #endregion

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="pago">Nombre de la tabla.</param>
        public RepositorioPago(string pago)
        {
            connection = new SQLiteConnection(pago);
            connection.CreateTable<Pago>();
        }

        /// <summary>
        /// Agrega un pago a la base de datos.
        /// </summary>
        /// <param name="pago">Objeto tipo: Pago. Este contiene toda la informacion del pago realizado.</param>
        public void AgregarPago(Pago pago)
        {
            try
            {
                var resultado = connection.Insert(pago);
                StatusMessage = $"SQLite: {resultado} pago agregado. [Descripción: {pago.SER_CODIGO}]";
            }
            catch (Exception e)
            {
                StatusMessage = string.Format("SQLite: Error al agregar registro {0}. [Error: {1}]", pago.SER_CODIGO, e.Message);
            }
        }

        /// <summary>
        /// Elimina un pago de la base de datos.
        /// </summary>
        /// <param name="pago">Objeto tipo: Pago. Este contiene toda la informacion del pago.</param>
        public void EliminarPago(Pago pago)
        {
            try
            {
                var resultado = connection.Delete(pago);
                StatusMessage = $"SQLite: {resultado} registro eliminado. [Descripción: {pago.SER_CODIGO}]";
            }
            catch (Exception e)
            {
                StatusMessage = string.Format("SQLite: Error al eliminar registro {0}. [Error: {1}]", pago.SER_CODIGO, e.Message);
            }
        }

        /// <summary>
        /// Muestra los pagos que se hayan realizado.
        /// </summary>
        /// <returns>Retorna una lista con los datos de los pagos realizados.</returns>
        public List<Pago> ObtenerPagos()
        {
            return connection.Table<Pago>().ToList();
        }
    }
}
