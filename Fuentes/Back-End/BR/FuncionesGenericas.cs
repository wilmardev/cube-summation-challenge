using Challenge.CubeSummationNS.Model.Models;
using Challenge.CubeSummationNS.Model.Resources;
using System;

namespace Challenge.CubeSummationNS.BR
{
    public static class FuncionesGenericas
    {
        public static RespuestaGeneral ManejarExcepcion(Exception exception)
        {
            // Grabar en log y/o visor de eventos
            return ObtenerRespuesta(false, CubeSummationResources.Error_Mensaje_Default);
        }

        public static RespuestaGeneral ObtenerRespuesta(bool estado, string mensaje = null, object informacion = null)
        {
            return new RespuestaGeneral
            {
                Estado = estado,
                Mensaje = mensaje,
                Informacion = informacion
            };
        }
    }
}