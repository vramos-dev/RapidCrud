using RapidCrud.GatewayAccess.Contract;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidCrud.GatewayAccess.Implementation
{
    public class WhatsappGateway : IWhatsappGateway
    {
        public void SendMessage(string mensaje)
        {
            var url = ConfigurationManager.AppSettings["URLWhatsapp"].ToString();
            var token = ConfigurationManager.AppSettings["TokenWhatsapp"].ToString();
            var telefono = ConfigurationManager.AppSettings["TelefonoVendedor"].ToString();

            var restClient = new RestClient(url);
            
            var request = new RestRequest("", Method.Post);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new
            {
                op = "registermessage",
                token_qr = token,
                mensajes = new List<object>
                {
                    new
                    {
                        numero = telefono,
                        mensaje = mensaje
                    }
                }
            });

            var response = restClient.Execute(request);
        }
    }
}
