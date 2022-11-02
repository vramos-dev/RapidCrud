using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidCrud.GatewayAccess.Contract
{
    public interface IGmailGateway
    {
        void SendMessage(string asunto, string mensaje);
    }
}
