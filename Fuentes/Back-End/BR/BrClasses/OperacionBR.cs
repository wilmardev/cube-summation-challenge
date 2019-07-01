using Challenge.CubeSummationNS.Model.Models;
using Challenge.CubeSummationNS.Model.Resources;
using System;

namespace Challenge.CubeSummationNS.BR
{
    public class Operacion : IOperacion
    {
        public RespuestaGeneral ProcesarOperacionUpdate(OperacionCubo operacion, long[,,] matriz, ref RespuestaGeneral respuestaGeneral)
        {
            respuestaGeneral = ValidarOperacionUpdate(operacion.InformacionOperacion, ref respuestaGeneral);
            if (respuestaGeneral.Estado)
                ActualizarValor(operacion.InformacionOperacion, matriz);
            return respuestaGeneral;
        }

        private RespuestaGeneral ValidarOperacionUpdate(long[] infoOperacion, ref RespuestaGeneral respuestaGeneral)
        {
            if (infoOperacion.Length != 4)
                return FuncionesGenericas.ObtenerRespuesta(false, respuestaGeneral.Mensaje + "\n" + CubeSummationResources.Error_Cantidad_Datos_OperacionUpdate);
            if (infoOperacion[3] <= Math.Pow(10, -9) || infoOperacion[3] >= Math.Pow(10, 9))
                return FuncionesGenericas.ObtenerRespuesta(false, respuestaGeneral.Mensaje + "\n" + CubeSummationResources.Error_Valores_Máximos_OperacionUpdate);

            return respuestaGeneral;
        }

        private void ActualizarValor(long[] infoOperacion, long[,,] matriz)
        {
            long x = infoOperacion[0] - 1;
            long y = infoOperacion[1] - 1;
            long z = infoOperacion[2] - 1;
            long valorActualizacion = infoOperacion[3];
            matriz[x, y, z] = valorActualizacion;
        }

        public RespuestaGeneral ProcesarOperacionQuery(OperacionCubo operacion, long[,,] matriz, ref RespuestaGeneral respuestaGeneral)
        {
            respuestaGeneral = ValidarOperacionQuery(operacion.InformacionOperacion, ref respuestaGeneral);
            if (!respuestaGeneral.Estado)
                return respuestaGeneral;
            return SumarValor(operacion.InformacionOperacion, matriz, ref respuestaGeneral);
        }

        private RespuestaGeneral SumarValor(long[] infoOperacion, long[,,] matriz, ref RespuestaGeneral respuestaGeneral)
        {
            long sumatoria = 0;
            int length = matriz.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    for (int k = 0; k < length; k++)
                    {
                        if (infoOperacion[0] - 1 <= i && infoOperacion[1] - 1 <= j && infoOperacion[2] - 1 <= k
                            && i <= infoOperacion[3] - 1 && j <= infoOperacion[4] - 1 && k <= infoOperacion[5] - 1)
                            sumatoria += matriz[i, j, k];
                    }
                }
            }
            respuestaGeneral.Mensaje += sumatoria + "\n";
            return respuestaGeneral;
        }

        private RespuestaGeneral ValidarOperacionQuery(long[] infoOperacion, ref RespuestaGeneral respuesta)
        {
            if (infoOperacion.Length != 6)
                return FuncionesGenericas.ObtenerRespuesta(false, respuesta.Mensaje + "\n" + CubeSummationResources.Error_Cantidad_Datos_OperacionQuery);

            return respuesta;
        }
    }
}