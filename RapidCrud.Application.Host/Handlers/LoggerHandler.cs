using RapidCrud.Common.Helpers.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace RapidCrud.Application.Host.Handlers
{
    public class LoggerHandler : DelegatingHandler
    {
        /// <summary>
        /// Implementación de loghelper.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = request.Headers.Contains("Authorization") ? request.Headers.GetValues("Authorization").FirstOrDefault() : "";
            var appCode = request.Headers.Contains("X-Setting-AppCode") ? request.Headers.GetValues("X-Setting-AppCode").FirstOrDefault() : "";

            var uri = request.RequestUri.ToString().ToLower();
            
            //implementacion de log trace para los endpoint.
            var log = new LogHelper();
            //log.LogParamInput("HTTP-Method", request.Method.Method);
            HttpContext.Current.Items["log-helper"] = log;

            var serverVar = HttpContext.Current.Request.ServerVariables;
            //log.IpAddress = serverVar["HTTP_X_FORWARDED_FOR"] ?? serverVar["REMOTE_ADDR"];

            var response = await base.SendAsync(request, cancellationToken);
            var strResponse = response.Content == null ? "" : await response.Content.ReadAsStringAsync();
            //log.Response = strResponse;
            //log.HttpResponse = ((int)response.StatusCode).ToString() + " - " + response.StatusCode.ToString();
            //log.Trace();
            return response;
        }
    }
}