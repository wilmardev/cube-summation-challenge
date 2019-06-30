using Challenge.CubeSummationNS.Model.Resources;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Challenge.CodeSummation.WebApi.Controllers
{
    public class BaseController : ApiController
    {
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
    }
}