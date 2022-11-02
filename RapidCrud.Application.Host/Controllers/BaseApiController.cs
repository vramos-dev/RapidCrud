using RapidCrud.BusinessEntity.Model;
using RapidCrud.Common.Helpers.Constansts;
using RapidCrud.Common.Helpers.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RapidCrud.Application.Host.Controllers
{
    public class BaseApiController : ApiController
    {
        private LogHelper _logger;
        protected LogHelper Logger
        {
            get
            {
                if (_logger == null)
                {
                    _logger = (LogHelper)HttpContext.Current.Items["log-helper"];
                    var routeTemplate = ControllerContext.RouteData.Route.RouteTemplate;
                    var controller = ControllerContext.ControllerDescriptor.ControllerType.Name;
                    var actionMethod = ActionContext.ActionDescriptor.ActionName;

                    //_logger.Method = string.Format("{0} - {1}", controller, actionMethod);
                    //_logger.LogParamInput("RouteTemplate", routeTemplate);
                    //_logger.LogParamInput("Controller", controller);
                    //_logger.LogParamInput("Action-Method", actionMethod);
                }
                return _logger;
            }
        }


        protected List<MessageResponse> GetMessageException(Exception ex)
        {
            var messages = new List<MessageResponse>();

            messages.Add(new MessageResponse()
            {
                Type = MessageResponseConstant.ErrorInformation,
                Message = "An error occurred while processing the request."
            });

            messages.Add(new MessageResponse()
            {
                Type = MessageResponseConstant.SystemError,
                Message = ex.Message
            });

            //agregar condicional debug
            messages.Add(new MessageResponse()
            {
                Type = MessageResponseConstant.Debug,
                Message = ex.StackTrace
            });

            if (ex.InnerException != null)
            {
                messages.Add(new MessageResponse()
                {
                    Type = MessageResponseConstant.Debug,
                    Message = ex.InnerException.Message
                });

                //agregar condicional debug
                messages.Add(new MessageResponse()
                {
                    Type = MessageResponseConstant.Debug,
                    Message = ex.InnerException.StackTrace
                });
            }
            //fin agregar condicional debug

            return messages;
        }

        protected MessageResponse GetMessageSuccessful(string message = null)
        {
            return new MessageResponse()
            {
                Type = MessageResponseConstant.Successful,
                Message = message ?? "Operation successful."
            };
        }

        protected MessageResponse GetMessageUnsuccessful(string message = null)
        {
            return new MessageResponse()
            {
                Type = MessageResponseConstant.ErrorInformation,
                Message = message ?? "Operation unsuccessful."
            };
        }

        protected MessageResponse GetMessageDataNotFound(string entity = null)
        {
            return new MessageResponse()
            {
                Type = MessageResponseConstant.ErrorInformation,
                Message = $"{entity ?? "Data"} not found."
            };
        }

        protected List<MessageResponse> GetMessageErrorModelState()
        {
            var listError = new List<MessageResponse>();
            foreach (var error in ModelState)
            {
                var messages = error.Value.Errors.Select(e => new MessageResponse()
                {
                    Type = MessageResponseConstant.ErrorInformation,
                    Message = e.ErrorMessage
                });

                listError.AddRange(messages);
            }

            return listError;
        }
    }
}
