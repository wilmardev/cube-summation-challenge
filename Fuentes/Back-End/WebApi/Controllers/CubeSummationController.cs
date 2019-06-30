using Challenge.CubeSummationNS.BR;
using Challenge.CubeSummationNS.Model.Resources;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Challenge.CodeSummation.WebApi.Controllers
{
    public class CubeSummationController : BaseController
    {
        private readonly Factory factory = new Factory();

        [HttpPost]
        public HttpResponseMessage ProcesarInformacion(string datosCubos)
        {
            try
            {
                if (string.IsNullOrEmpty(datosCubos))
                    return GenerarMensajeRespuesta(HttpStatusCode.OK, CubeSummationResources.Error_Datos_Entrada_Vacio);
                ICubeSummationBR cubeSummation = ConstruirCubeSummationBR();
                var respuesta = cubeSummation.ProcesarInformacion(datosCubos);
                return GenerarMensajeRespuesta(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return ManejarExcepcion(ex);
            }
        }

        private ICubeSummationBR ConstruirCubeSummationBR()
        {
            //Se tomarían variables de HttpSession, origen, usuario para trazabilidad
            return factory.ContruirCubeSummationBR();
        }
    }
}