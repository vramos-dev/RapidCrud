using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace RapidCrud.Application.Host.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string ContentAbsUri(this UrlHelper urlHelper, string contentPath)
        {
            var path = urlHelper.Content(contentPath);
            var url = new Uri(urlHelper.BaseUri(), path);

            return url.AbsoluteUri;
        }

        public static Uri BaseUri(this UrlHelper urlHelper)
        {
            var hasPort = new Regex(":[0-9]{1,5}");
            if (hasPort.IsMatch(HttpContext.Current.Request.Url.ToString()))
            {
                var urlBuilder = new UriBuilder(HttpContext.Current.Request.Url.AbsoluteUri)
                {
                    Path = HttpContext.Current.Request.ApplicationPath,
                    Query = null,
                    Fragment = null
                };

                return urlBuilder.Uri;
            }
            else
            {
                var forchttps = ConfigurationManager.AppSettings["ForceHttps"] == "1";
                var urlBuilder = new UriBuilder()
                {
                    Scheme = forchttps ? "https" : HttpContext.Current.Request.Url.Scheme,
                    Host = HttpContext.Current.Request.Url.Host,
                    Path = HttpContext.Current.Request.ApplicationPath,
                    Query = null,
                    Fragment = null
                };

                return urlBuilder.Uri;
            }
        }

        public static string BaseUriString(this UrlHelper urlHelper)
        {
            var url = urlHelper.BaseUri().ToString();
            if (url.EndsWith("/"))
                url = url.Substring(0, url.Length - 1);
            return url;
        }
    }
}