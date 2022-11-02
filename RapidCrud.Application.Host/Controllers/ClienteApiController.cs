using RapidCrud.Application.Host.Mappers;
using RapidCrud.BusinessEntity.Model;
using RapidCrud.BusinessLogic.Contract;
using RapidCrud.BusinessLogic.Implementation;
using RapidCrud.DataAccess.Contract;
using RapidCrud.DataAccess.Implementation;
using RapidCrud.GatewayAccess.Contract;
using RapidCrud.GatewayAccess.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RapidCrud.Application.Host.Controllers
{
    [RoutePrefix("api/cliente")]
    public class ClienteApiController : BaseApiController
    {
        private readonly IClienteLogic _clienteLogic;
        
        public ClienteApiController()
        {
            IClienteData clienteData = new ClienteData();
            IWhatsappGateway whatsappGateway = new WhatsappGateway();
            IGmailGateway gmailGateway = new GmailGateway();

            _clienteLogic = new ClienteLogic(clienteData, whatsappGateway, gmailGateway);
        }

        // GET: api/cliente
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var response = new ApiResponse<List<ClienteResponseModel>>();

            try
            {
                var listCliente = _clienteLogic.ListAll();
                response.Success = listCliente.Count() > 0;
                response.Data = listCliente.Select(c => ClienteMapper.GetResponse(c)).ToList();

                if (response.Success)
                    response.Messages.Insert(0, GetMessageSuccessful());
                else
                    response.Messages.Insert(0, GetMessageDataNotFound());
            }
            catch (Exception ex)
            {
                //Logger.Exception(ex);
                response.Messages.InsertRange(0, GetMessageException(ex));
            }

            return Ok(response);
        }

        // GET: api/cliente/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById([FromUri]int id)
        {
            var response = new ApiResponse<object>();

            try
            {
                var cliente = _clienteLogic.GetById(id);
                response.Success = cliente?.Id > 0;
                response.Data = ClienteMapper.GetResponse(cliente);

                if (response.Success)
                    response.Messages.Insert(0, GetMessageSuccessful());
                else
                    response.Messages.Insert(0, GetMessageDataNotFound());
            }
            catch (Exception ex)
            {
                //Logger.Exception(ex);
                response.Messages.InsertRange(0, GetMessageException(ex));
            }

            return Json(response);
        }

        // GET: api/cliente/dni/45454545
        [HttpGet]
        [Route("dni/{dni:regex(^\\d{8}$)}")]
        public IHttpActionResult GetByDni([FromUri] string dni)
        {
            var response = new ApiResponse<ClienteResponseModel>();

            try
            {
                var cliente = _clienteLogic.GetByDni(dni);
                response.Success = cliente?.Id > 0;
                response.Data = ClienteMapper.GetResponse(cliente);

                if (response.Success)
                    response.Messages.Insert(0, GetMessageSuccessful());
                else
                    response.Messages.Insert(0, GetMessageDataNotFound());
            }
            catch (Exception ex)
            {
                //Logger.Exception(ex);
                response.Messages.InsertRange(0, GetMessageException(ex));
            }

            return Ok(response);
        }

        // POST: api/cliente
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]ClienteRequestModel request)
        {
            var response = new ApiResponse<ClienteResponseModel>();

            try
            {
                if (request == null)
                {
                    return BadRequest("Request is null");
                }

                if (!ModelState.IsValid)
                {
                    response.Messages = GetMessageErrorModelState();
                    return Ok(response);
                }

                var cliente = ClienteMapper.GetCliente(request);
                response.Success = _clienteLogic.Create(cliente);

                if (response.Success)
                {
                    response.Data = ClienteMapper.GetResponse(cliente);
                    response.Messages.Insert(0, GetMessageSuccessful());
                }
                else
                    response.Messages.Insert(0, GetMessageUnsuccessful());
            }
            catch (Exception ex)
            {
                //Logger.Exception(ex);
                response.Messages.InsertRange(0, GetMessageException(ex));
            }

            return Ok(response);
        }

        // PUT: api/cliente/5
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Put([FromUri]int id, [FromBody] ClienteRequestModel request)
        {
            var response = new ApiResponse<ClienteResponseModel>();

            try
            {
                if (request == null)
                {
                    return BadRequest("Request is null");
                }

                if (id <= 0)
                {
                    return BadRequest("Cliente Id es incorrecto.");
                }

                if (!ModelState.IsValid)
                {
                    response.Messages = GetMessageErrorModelState();
                    return Ok(response);
                }

                var cliente = ClienteMapper.GetCliente(request, id);
                response.Success = _clienteLogic.Update(cliente);

                if (response.Success)
                {
                    response.Data = ClienteMapper.GetResponse(cliente);
                    response.Messages.Insert(0, GetMessageSuccessful());
                }
                else
                    response.Messages.Insert(0, GetMessageUnsuccessful());
            }
            catch (Exception ex)
            {
                //Logger.Exception(ex);
                response.Messages.InsertRange(0, GetMessageException(ex));
            }

            return Ok(response);
        }

        // DELETE: api/cliente/5
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            var response = new ApiResponse();

            try
            {
                if (id <= 0)
                {
                    return BadRequest("Cliente Id es incorrecto.");
                }

                response.Success = _clienteLogic.Delete(id);

                if (response.Success)
                    response.Messages.Insert(0, GetMessageSuccessful());
                else
                    response.Messages.Insert(0, GetMessageUnsuccessful());
            }
            catch (Exception ex)
            {
                //Logger.Exception(ex);
                response.Messages.InsertRange(0, GetMessageException(ex));
            }

            return Ok(response);
        }
    }
}
