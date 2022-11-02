using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidCrud.GatewayAccess.Contract
{
    public interface IWhatsappGateway
    {
        void SendMessage(string mensaje);
    }
}
