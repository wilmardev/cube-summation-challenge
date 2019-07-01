using Challenge.CubeSummationNS.Model.Models;
using Challenge.CubeSummationNS.Model.Resources;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Challenge.CodeSummationNS.WebApi.Controllers
{
    public class BaseController : ApiController
    {
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        protected HttpResponseMessage GenerarMensajeRespuesta(HttpStatusCode statusCode, object informacion = null)
        {
            return Request.CreateResponse(statusCode, informacion);
        }

        protected HttpResponseMessage ManejarExcepcion(Exception ex)
        {
            //Se grabaría en event log la excepcion y se retorna
            //un mensaje genérico de error
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                CubeSummationResources.Error_Mensaje_Default);
        }

        protected HttpResponseMessage GenerarRespuestaGeneral(HttpStatusCode statusCode, string mensaje, bool estado)
        {
            RespuestaGeneral respuestaGeneral = new RespuestaGeneral
            {
                Estado = estado,
                Mensaje = mensaje
            };
            return GenerarMensajeRespuesta(statusCode, respuestaGeneral);
        }
    }
}