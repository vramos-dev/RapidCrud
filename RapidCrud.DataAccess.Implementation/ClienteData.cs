using RapidCrud.Common.Helpers.Enums;
using RapidCrud.DataAccess.Contract;
using RapidCrud.DataAccess.Implementation.Mapper;
using RapidCrud.EntityBusiness.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidCrud.DataAccess.Implementation
{
    public class ClienteData : BaseData, IClienteData
    {
        public ClienteData() : base(DataBaseEnum.Rapid) { }

        public bool Create(Cliente cliente)
        {
            cliente.Id = Insert(cmd =>
            {
                cmd.CommandText = "uspClienteInsert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Dni", cliente.Dni);
                cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
            });

            return cliente.Id > 0;
        }

        public bool Delete(int clienteId)
        {
            var result = Delete(cmd =>
            {
                cmd.CommandText = "uspClienteDelete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", clienteId);
            });

            return result > 0;
        }

        public Cliente Find(int? id = null, string dni = null)
        {
            var cliente = GetOne(cmd =>
            {
                cmd.CommandText = "uspClienteGetOne";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Dni", dni);
            }, ClienteMapper.GetCliente);

            return cliente;
        }

        public IEnumerable<Cliente> ListAll()
        {
            var listCliente = GetList(cmd =>
            {
                cmd.CommandText = "uspClienteListAll";
                cmd.CommandType = CommandType.StoredProcedure;
            }, ClienteMapper.GetCliente);

            return listCliente;
        }

        public bool Update(Cliente cliente)
        {
            var result = Update(cmd =>
            {
                cmd.CommandText = "uspClienteUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", cliente.Id);
                cmd.Parameters.AddWithValue("@Dni", cliente.Dni);
                cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
            });

            return result > 0;
        }
    }
}
