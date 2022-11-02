using RapidCrud.BusinessLogic.Contract;
using RapidCrud.DataAccess.Contract;
using RapidCrud.EntityBusiness.Domain;
using RapidCrud.GatewayAccess.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidCrud.BusinessLogic.Implementation
{
    public class ClienteLogic : IClienteLogic
    {
        private readonly IClienteData _clienteData;
        private readonly IWhatsappGateway _whatsappGateway;
        private readonly IGmailGateway _gmailGateway;

        public ClienteLogic(IClienteData clienteData, IWhatsappGateway whatsappGateway, IGmailGateway gmailGateway)
        {
            _clienteData = clienteData;
            _whatsappGateway = whatsappGateway;
            _gmailGateway = gmailGateway;
        }

        public bool Create(Cliente cliente)
        {
            var response = _clienteData.Create(cliente);

            Task.Factory.StartNew((clienteThread) => 
            {
                var asunto = string.Format("Registro de Cliente {0}", cliente.Dni);
                var mensaje = string.Format("Se regsitró el cliente {0} {1}, con DNI {2}.",
                    cliente.Nombre, cliente.Apellido, cliente.Dni);
                _gmailGateway.SendMessage(asunto, mensaje);
                _whatsappGateway.SendMessage(mensaje);
            }, cliente, TaskCreationOptions.LongRunning);

            return response;
        }

        public bool Delete(int clienteId)
        {
            var cliente = GetById(clienteId);
            var response = _clienteData.Delete(clienteId);

            Task.Factory.StartNew((clienteThread) =>
            {
                var asunto = string.Format("Eliminación de Cliente {0}", cliente.Dni);
                var mensaje = string.Format("Se eliminó el cliente {0} {1}, con DNI {2}.",
                    cliente.Nombre, cliente.Apellido, cliente.Dni);
                _gmailGateway.SendMessage(asunto, mensaje);
                _whatsappGateway.SendMessage(mensaje);
            }, cliente, TaskCreationOptions.LongRunning);

            return response;
        }

        public Cliente GetByDni(string dni)
        {
            return _clienteData.Find(dni: dni);
        }

        public Cliente GetById(int id)
        {
            return _clienteData.Find(id: id);
        }

        public IEnumerable<Cliente> ListAll()
        {
            return _clienteData.ListAll();
        }

        public bool Update(Cliente cliente)
        {
            var response = _clienteData.Update(cliente);

            Task.Factory.StartNew((clienteThread) =>
            {
                var asunto = string.Format("Actualización de Cliente {0}", cliente.Dni);
                var mensaje = string.Format("Se actualizó el cliente {0} {1}, con DNI {2}.",
                    cliente.Nombre, cliente.Apellido, cliente.Dni);
                _gmailGateway.SendMessage(asunto, mensaje);
                _whatsappGateway.SendMessage(mensaje);
            }, cliente, TaskCreationOptions.LongRunning);

            return response;
        }
    }
}
