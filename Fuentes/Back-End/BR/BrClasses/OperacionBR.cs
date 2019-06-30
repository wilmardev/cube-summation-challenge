using Challenge.CubeSummationNS.Model.Models;
using Challenge.CubeSummationNS.Model.Resources;
using System;

namespace Challenge.CubeSummationNS.BR
{
    public class Operacion : IOperacion
    {
        public RespuestaGeneral ProcesarOperacionUpdate(OperacionCubo operacion, int[,,] matriz, ref RespuestaGeneral respuestaGeneral)
        {
            respuestaGeneral = ValidarOperacionUpdate(operacion.InformacionOperacion, ref respuestaGeneral);
            if (respuestaGeneral.Estado)
                ActualizarValor(operacion.InformacionOperacion, matriz);
            return respuestaGeneral;
        }

        private RespuestaGeneral ValidarOperacionUpdate(int[] infoOperacion, ref RespuestaGeneral respuestaGeneral)
        {
            if (infoOperacion.Length != 4)
                return FuncionesGenericas.ObtenerRespuesta(false, respuestaGeneral.Mensaje + "\n" + CubeSummationResources.Error_Cantidad_Datos_OperacionUpdate);
            if (infoOperacion[3] <= Math.Pow(10, -9) || infoOperacion[3] >= Math.Pow(10, 9))
                return FuncionesGenericas.ObtenerRespuesta(false, respuestaGeneral.Mensaje + "\n" + CubeSummationResources.Error_Valores_Máximos_OperacionUpdate);

            return respuestaGeneral;
        }

        private void ActualizarValor(int[] infoOperacion, int[,,] matriz)
        {
            int x = infoOperacion[0] - 1;
            int y = infoOperacion[1] - 1;
            int z = infoOperacion[2] - 1;
            int valorActualizacion = infoOperacion[3];
            matriz[x, y, z] = valorActualizacion;
        }

        public RespuestaGeneral ProcesarOperacionQuery(OperacionCubo operacion, int[,,] matriz, ref RespuestaGeneral respuestaGeneral)
        {
            respuestaGeneral = ValidarOperacionQuery(operacion.InformacionOperacion, ref respuestaGeneral);
            if (!respuestaGeneral.Estado)
                return respuestaGeneral;
            return SumarValor(operacion.InformacionOperacion, matriz, ref respuestaGeneral);
        }

        private RespuestaGeneral SumarValor(int[] infoOperacion, int[,,] matriz, ref RespuestaGeneral respuestaGeneral)
        {
            throw new NotImplementedException();
        }

        private RespuestaGeneral ValidarOperacionQuery(int[] infoOperacion, ref RespuestaGeneral respuesta)
        {
            if (infoOperacion.Length != 6)
                return FuncionesGenericas.ObtenerRespuesta(false, respuesta.Mensaje + "\n" + CubeSummationResources.Error_Cantidad_Datos_OperacionQuery);

            return respuesta;
        }
    }
}