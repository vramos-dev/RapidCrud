using RapidCrud.Common.Helpers.Constansts;
using RapidCrud.Common.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidCrud.DataAccess.Implementation
{
    public class BaseData
    {
        protected readonly SqlConnection _sqlConnection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">Nombre de la cadena de conexión</param>
        protected BaseData(DataBaseEnum db)
        {
            _sqlConnection = new SqlConnection(GetConecction(db));
        }

        private string GetConecction(DataBaseEnum db)
        {
            string connectionString = string.Empty;
            switch (db)
            {
                case DataBaseEnum.Rapid:
                    connectionString = ConnectionStringConstanst.Rapid;
                    break;
            }

            return ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
        }

        private void _execute(Action<SqlCommand> cfgCmd, Action<IDbCommand> processCmd)
        {
            using (var cmd = _sqlConnection.CreateCommand())
            {
                cfgCmd(cmd);

                _sqlConnection.Open();

                try
                {
                    processCmd(cmd);
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }

        protected List<S> GetList<S>(Action<SqlCommand> cfgCmd, Func<IDataReader, S> makeData)
        {
            var resultList = new List<S>();

            _execute(cfgCmd, c =>
            {
                using (var dr = c.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        resultList.Add(makeData(dr));
                    }
                }
            });

            return resultList;
        }

        protected S GetOne<S>(Action<SqlCommand> cfgCmd, Func<IDataReader, S> makeData)
        {
            var result = default(S);

            _execute(cfgCmd, c =>
            {
                using (var dr = c.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = makeData(dr);
                    }
                }
            });

            return result;
        }

        protected int Insert(Action<SqlCommand> cfgCmd)
        {
            var result = default(int);

            _execute(cfgCmd, c =>
            {
                result = (int)c.ExecuteScalar(); 
            });

            return result;
        }

        protected int Update(Action<SqlCommand> cfgCmd)
        {
            var result = default(int);

            _execute(cfgCmd, c =>
            {
                result = c.ExecuteNonQuery();
            });

            return result;
        }

        protected int Delete(Action<SqlCommand> cfgCmd)
        {
            var result = default(int);

            _execute(cfgCmd, c =>
            {
                result = c.ExecuteNonQuery();
            });

            return result;
        }
    }
}
