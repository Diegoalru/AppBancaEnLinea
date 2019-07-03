using System;
using System.Collections.Generic;
using SQLite;
using AppBancaEnLinea.Models;

namespace AppBancaEnLinea.Data
{
    public class RepositorioUsuario
    {
        #region Variables
        private readonly SQLiteConnection connection;
        public string StatusMessage { get; set; }
        private Usuario usuario;
        #endregion

        public void SetUser(Usuario usuario)
        {
            this.usuario = usuario;
        }

        public Usuario GetUsuario()
        {
            return this.usuario;
        }

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="usuario">Nombre de la tabla.</param>
        public RepositorioUsuario(string usuario)
        {
            connection = new SQLiteConnection(usuario);
            connection.CreateTable<Usuario>();
        }

        /// <summary>
        /// Agrega un usuario a la base de datos.
        /// </summary>
        /// <param name="usuario">Objeto tipo: Usuario. Este contiene la informacion personal del usuario.</param>
        public void AgregarUsuario(Usuario usuario)
        {
            try
            {
                var resultado = connection.Insert(usuario);
                StatusMessage = $"SQLite: {resultado} registro agregado. [Identificacion: {usuario.USU_IDENTIFICACION}]";
            }
            catch (Exception e)
            {
                StatusMessage = string.Format("SQLite: Error al agregar usuario {0}. [Error: {1}]", usuario.USU_IDENTIFICACION, e.Message);
            }
        }

        /// <summary>
        /// Elimina un usuario de la base de datos.
        /// </summary>
        /// <param name="usuario">Objeto tipo: Usuario. Este contiene la informacion personal del usuario.</param>
        public void EliminarUsuario(Usuario usuario)
        {
            try
            {
                var resultado = connection.Delete(usuario);
                StatusMessage = $"SQLite: {resultado} registro eliminado. [Identificación: {usuario.USU_IDENTIFICACION}]";
            }
            catch (Exception e)
            {
                StatusMessage = string.Format("SQLite: Error al eliminar usuario {0}. [Error: {1}]", usuario.USU_IDENTIFICACION, e.Message);
            }
        }

        /// <summary>
        /// Muestra los usuarios registrados en la base de datos.
        /// </summary>
        /// <returns>Retorna una lista con los datos de los usuarios.</returns>
        public List<Usuario> ObtenerUsuarios()
        {
            return connection.Table<Usuario>().ToList();
        }
    }
}
