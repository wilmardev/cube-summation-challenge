using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace WebApi.Tests
{
    [TestClass]
    public class IntegrationTest
    {
        private HttpServer server;
        private const string URL_BASE = "http://localhost:9999/WebApiCodeSummation/api/";
        private const string POST_DATA = "2\n4 5\nUPDATE 2 2 2 4\nQUERY 1 1 1 3 3 3\nUPDATE 1 1 1 23\nQUERY 2 2 2 4 4 4\nQUERY 1 1 1 3 3 3\n2 4\nUPDATE 2 2 2 1\nQUERY 1 1 1 1 1 1\nQUERY 1 1 1 2 2 2\nQUERY 2 2 2 2 2 2";

        [TestInitialize]
        public void Setup()
        {
            var config = new HttpConfiguration();
            config.EnableCors();
            config.MapHttpAttributeRoutes();
            config.Formatters.Insert(0, new TextMediaTypeFormatter());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            server = new HttpServer(config);
        }

        [TestMethod]
        public void EndPoint_Test()
        {
            var client = new HttpClient(server);
            var request = CrearRequest("CubeSummation/ProcesarInformacion", HttpMethod.Post);
            StringContent queryString = new StringContent(POST_DATA, Encoding.UTF8);

            using (HttpResponseMessage response = client.PostAsync(URL_BASE + "CubeSummation/ProcesarInformacion", queryString).Result)
            {
                Assert.IsNotNull(response);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.IsNotNull(response.Content);
            }
        }

        private HttpRequestMessage CrearRequest(string url, HttpMethod method)
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(URL_BASE + url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            request.Method = method;
            byte[] byteArray = Encoding.UTF8.GetBytes(POST_DATA);
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string> { { "datosCubos", POST_DATA } });
            MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("text/plain");
            request.Content.Headers.ContentType = mediaType;
            request.Content.Headers.ContentLength = byteArray.Length;
            return request;
        }
    }
}