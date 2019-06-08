using System;
using System.Collections.Generic;
using SQLite;
using AppBancaEnLinea.Models;

namespace AppBancaEnLinea.Data
{
    public class RepositorioServicio
    {

        #region Variables
        private readonly SQLiteConnection connection;
        public string StatusMessage { get; set; }
        #endregion

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="servicio">Nombre de la tabla.</param>
        public RepositorioServicio(string servicio)
        {
            connection = new SQLiteConnection(servicio);
            connection.CreateTable<Servicio>();
        }

        /// <summary>
        /// Agrega un servicio a la base de datos.
        /// </summary>
        /// <param name="servicio">Objeto tipo: Servicio. Este contiene toda la informacion de un servicio.</param>
        public void AgregarServicio(Servicio servicio)
        {
            try
            {
                var resultado = connection.Insert(servicio);
                StatusMessage = $"SQLite: {resultado} registro agregado. [Descripción: {servicio.SER_DESCRIPCION}]";
            }
            catch (Exception e)
            {
                StatusMessage = string.Format("SQLite: Error al agregar registro {0}. [Error: {1}]", servicio.SER_DESCRIPCION, e.Message);
            }
        }

        /// <summary>
        /// Elimina un servicio de la base de datos.
        /// </summary>
        /// <param name="servicio">Objeto tipo: Servicio. Este contiene toda la informacion de un servicio.</param>
        public void EliminarServicio(Servicio servicio)
        {
            try
            {
                var resultado = connection.Delete(servicio);
                StatusMessage = $"SQLite: {resultado} registro eliminado. [Descripción: {servicio.SER_DESCRIPCION}]";
            }
            catch (Exception e)
            {
                StatusMessage = string.Format("SQLite: Error al eliminar registro {0}. [Error: {1}]", servicio.SER_DESCRIPCION, e.Message);
            }
        }

        /// <summary>
        /// Muestra los servicios que se pueden realizar.
        /// </summary>
        /// <returns>Retorna una lista con los datos de los servicios existentes.</returns>
        public List<Servicio> ObtenerServicios()
        {
            return connection.Table<Servicio>().ToList();
        }

    }
}
