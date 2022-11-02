using RapidCrud.EntityBusiness.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidCrud.BusinessLogic.Contract
{
    public interface IClienteLogic
    {
        bool Create(Cliente cliente);
        bool Update(Cliente cliente);
        bool Delete(int clienteId);
        IEnumerable<Cliente> ListAll();
        Cliente GetById(int id);
        Cliente GetByDni(string dni);
    }
}
