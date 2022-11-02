using RapidCrud.EntityBusiness.Domain;
using RapidCrud.Common.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidCrud.DataAccess.Implementation.Mapper
{
    public static class ClienteMapper
    {
        public static Cliente GetCliente(this IDataReader reader)
        {
            var cliente = new Cliente()
            {
                Id = reader.GetInt("Id"),
                Dni = reader.GetString("Dni"),
                Nombre = reader.GetString("Nombre"),
                Apellido = reader.GetString("Apellido"),
                Telefono = reader.GetString("Telefono"),
                Email = reader.GetString("Email")
            };

            return cliente;
        }
    }
}
