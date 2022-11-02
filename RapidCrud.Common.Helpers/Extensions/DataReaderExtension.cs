using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidCrud.Common.Helpers.Extensions
{
    public static class DataReaderExtension
    {
        /// <summary>
        /// Recupera un valor int
        /// </summary>
        /// <param name="reader">Objeto DataReader</param>
        /// <param name="paramName">Nombre del parametro</param>
        /// <returns>Int</returns>
        public static int GetInt(this IDataReader reader, string paramName)
        {
            return reader.IsDBNull(reader.GetOrdinal(paramName)) ? 0 : reader.GetInt32(reader.GetOrdinal(paramName));
        }

        /// <summary>
        /// Recupera un valor string
        /// </summary>
        /// <param name="reader">Objeto DataReader</param>
        /// <param name="paramName">Nombre del parametro</param>
        /// <param name="defaultValue">Valor por defecto</param>
        /// <returns>String</returns>
        public static string GetString(this IDataReader reader, string paramName, string defaultValue = null)
        {
            if (reader.IsDBNull(reader.GetOrdinal(paramName)))
            {
                return defaultValue;
            }
            return reader.GetString(reader.GetOrdinal(paramName));
        }
    }
}
