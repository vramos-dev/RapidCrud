using RapidCrud.EntityBusiness.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidCrud.DataAccess.Contract
{
    public interface IClienteData
    {
        bool Create(Cliente cliente);
        bool Update(Cliente cliente);
        bool Delete(int clienteId);
        IEnumerable<Cliente> ListAll();
        Cliente Find(int? id = null, string dni = null);
    }
}
