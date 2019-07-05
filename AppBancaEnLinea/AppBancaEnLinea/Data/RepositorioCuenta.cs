using System;
using System.Collections.Generic;
using SQLite;
using AppBancaEnLinea.Models;

namespace AppBancaEnLinea.Data
{
    public class RepositorioCuenta
    {
        #region Variables
        private readonly SQLiteConnection connection;
        public string StatusMessage { get; set; }
        #endregion

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="cuenta">Nombre de la tabla.</param>
        public RepositorioCuenta(string cuenta)
        {
            connection = new SQLiteConnection(cuenta);
            connection.CreateTable<Cuenta>();
        }

        /// <summary>
        /// Agrega una cuenta a la base de datos.
        /// </summary>
        /// <param name="cuenta">Objeto tipo: Cuenta. Este contiene toda la informacion de la cuenta.</param>
        public void AgregarCuenta(Cuenta cuenta)
        {
            try
            {
                var resultado = connection.Insert(cuenta);
                if(resultado == 1)
                {
                    StatusMessage = $"SQLite: {cuenta.CUE_CODIGO} registro agregado.\n[Descripción: {cuenta.CUE_DESCRIPCION}]";
                }
                else
                {
                    throw new System.ArgumentException("Se ha producido un error al ingresar la cuenta.\nPor favor comunicarse con el administrador.");
                }
            }
            catch (Exception e)
            {
                StatusMessage = string.Format("SQLite: Error al agregar registro {0}. \nError: {1}", cuenta.CUE_DESCRIPCION, e.Message);
            }
        }

        /// <summary>
        /// Elimina una cuenta de la base de datos.
        /// </summary>
        /// <param name="cuenta">Objeto tipo: Cuenta. Este contiene toda la informacion de la cuenta.</param>
        public void EliminarCuenta(Cuenta cuenta)
        {
            try
            {
                var resultado = connection.Delete(cuenta);
                if (resultado == 1)
                {
                    StatusMessage = $"SQLite: La cuenta {cuenta.CUE_CODIGO} ha sido eliminada.\n[Descripción: {cuenta.CUE_DESCRIPCION}]";
                }
                else
                {
                    throw new System.ArgumentException("Se ha producido un error al eliminar la cuenta.\nPor favor comunicarse con el administrador.");
                }
            }
            catch (Exception e)
            {
                StatusMessage = string.Format("SQLite: Error al eliminar  {0}.\nError: {1}", cuenta.CUE_DESCRIPCION, e.Message);
            }
        }


        public void ActualizaCuenta(Cuenta cuenta)
        {
            try
            {
                var resultado = connection.Update(cuenta);
                if (resultado == 1)
                {
                    StatusMessage = $"SQLite: La cuenta {cuenta.CUE_CODIGO} ha sido actualizada.";
                }
                else
                {
                    throw new System.ArgumentException("Se ha producido un error al actualizar la cuenta.\nPor favor comunicarse con el administrador.");
                }
            }
            catch (Exception e)
            {
                StatusMessage = string.Format("SQLite: Error al actualizar {0}. Error: {1}", cuenta.CUE_DESCRIPCION, e.Message);
            }
        }

        /// <summary>
        /// Muestra las cuentas que se encuentran en la base de datos.
        /// </summary>
        /// <returns>Retorna una lista con los datos de las cuentas.</returns>
        public List<Cuenta> ObtenerCuentas()
        {
            return connection.Table<Cuenta>().ToList();
        }
    }
}
