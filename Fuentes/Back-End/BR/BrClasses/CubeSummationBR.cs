using Challenge.CubeSummationNS.Model.Enumerables;
using Challenge.CubeSummationNS.Model.Models;
using System;
using System.Collections.Generic;

namespace Challenge.CubeSummationNS.BR
{
    public class CubeSummationBR : ICubeSummationBR
    {
        private readonly ValidacionDatos validacionDatos;
        private readonly IOperacion operacion;

        public CubeSummationBR(ValidacionDatos validacionDatos, IOperacion operacion)
        {
            this.validacionDatos = validacionDatos;
            this.operacion = operacion;
        }

        public RespuestaGeneral ProcesarInformacion(string datos)
        {
            RespuestaGeneral respuestaGeneral = new RespuestaGeneral { Estado = true };
            try
            {
                List<Cubo> listCubo = validacionDatos.ValidarMapearDatos(datos, ref respuestaGeneral);
                foreach (Cubo cubo in listCubo)
                {
                    if (!respuestaGeneral.Estado)
                        return respuestaGeneral;
                    respuestaGeneral = ProcesarCubo(cubo, respuestaGeneral);
                }
            }
            catch (Exception ex)
            {
                return FuncionesGenericas.ManejarExcepcion(ex);
            }
            return respuestaGeneral;
        }

        private RespuestaGeneral ProcesarCubo(Cubo cubo, RespuestaGeneral respuestaGeneral)
        {
            long[,,] matriz = new long[cubo.TamanoMatriz, cubo.TamanoMatriz, cubo.TamanoMatriz];
            return ResolverOperacionesMatriz(matriz, cubo, respuestaGeneral);
        }

        private RespuestaGeneral ResolverOperacionesMatriz(long[,,] matriz, Cubo cubo, RespuestaGeneral respuestaGeneral)
        {
            if (!respuestaGeneral.Estado)
                return respuestaGeneral;
            return ResolverOperaciones(cubo, matriz, ref respuestaGeneral);
        }

        private RespuestaGeneral ResolverOperaciones(Cubo cubo, long[,,] matriz, ref RespuestaGeneral respuestaGeneral)
        {
            foreach (OperacionCubo operacionCubo in cubo.OperacionesCubo)
            {
                string tipoOperacion = operacionCubo.TipoOperacion.ToUpper();
                if (!respuestaGeneral.Estado)
                    return respuestaGeneral;
                validacionDatos.ValidarCoordenadas(operacionCubo.InformacionOperacion, cubo, ref respuestaGeneral, tipoOperacion);

                if (tipoOperacion.Equals(TipoOperacion.UPDATE.ToString()))
                    operacion.ProcesarOperacionUpdate(operacionCubo, matriz, ref respuestaGeneral);

                if (tipoOperacion.Equals(TipoOperacion.QUERY.ToString()))
                    operacion.ProcesarOperacionQuery(operacionCubo, matriz, ref respuestaGeneral);
            }
            return respuestaGeneral;
        }
    }
}