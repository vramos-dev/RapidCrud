using RapidCrud.BusinessEntity.Model;
using RapidCrud.EntityBusiness.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RapidCrud.Application.Host.Mappers
{
    public static class ClienteMapper
    {
        public static ClienteResponseModel GetResponse(Cliente cliente)
        {
            if (cliente == null)
                return null;

            var model = new ClienteResponseModel();
            model.Id = cliente.Id;
            model.Dni = cliente.Dni;
            model.Nombre = cliente.Nombre;
            model.Apellido = cliente.Apellido;
            model.Telefono = cliente.Telefono;
            model.Email = cliente.Email;
            return model;
        }

        public static Cliente GetCliente(ClienteRequestModel model, int id = 0)
        {
            if (model == null)
                return null;

            var cliente = new Cliente();
            cliente.Id = id;
            cliente.Dni = model.Dni;
            cliente.Nombre = model.Nombre;
            cliente.Apellido = model.Apellido;
            cliente.Telefono = model.Telefono;
            cliente.Email = model.Email;
            return cliente;
        }
    }
}