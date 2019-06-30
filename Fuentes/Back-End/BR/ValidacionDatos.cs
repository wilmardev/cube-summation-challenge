using Challenge.CubeSummationNS.Model.Models;
using Challenge.CubeSummationNS.Model.Resources;
using System;
using System.Collections.Generic;

namespace Challenge.CubeSummationNS.BR
{
    public class ValidacionDatos
    {
        public ValidacionDatos()
        {
        }

        public List<Cubo> ValidarMapearDatos(string data, ref RespuestaGeneral respuestaGeneral)
        {
            string[] informacion = data.Split('\n');
            if (Convert.ToInt32(informacion[0]) <= 0 || Convert.ToInt32(informacion[0]) > 50)
            {
                respuestaGeneral = FuncionesGenericas.ObtenerRespuesta(false, CubeSummationResources.Error_Cantidad_CasosPruebas);
                return new List<Cubo>();
            }
            return MapearCupo(informacion, respuestaGeneral);
        }

        private List<Cubo> MapearCupo(string[] informacion, RespuestaGeneral respuestaGeneral)
        {
            List<Cubo> listCubo = new List<Cubo>();
            Cubo cubo = new Cubo();
            for (int i = 1; i < informacion.Length; i++)
            {
                string[] datos = informacion[i].Split(' ');
                if (datos.Length == 2)
                {
                    if (!ValidarTamanoMatrizOperaciones(datos, ref respuestaGeneral))
                        return new List<Cubo>();

                    cubo = new Cubo
                    {
                        TamanoMatriz = Convert.ToInt32(informacion[i].Split(' ')[0]),
                        CantidadOperaciones = Convert.ToInt32(informacion[i].Split(' ')[1]),
                        OperacionesCubo = new List<OperacionCubo>()
                    };
                    listCubo.Add(cubo);
                    continue;
                }
                cubo.OperacionesCubo.Add(new OperacionCubo
                {
                    TipoOperacion = datos[0],
                    InformacionOperacion = ObtenerInformacionOperacion(datos)
                });
            }
            return listCubo;
        }

        private bool ValidarTamanoMatrizOperaciones(string[] infoMatriz, ref RespuestaGeneral respuestaGeneral)
        {
            if (Convert.ToInt32(infoMatriz[0]) < 1 || Convert.ToInt32(infoMatriz[0]) > 100)
                FuncionesGenericas.ObtenerRespuesta(false, respuestaGeneral.Mensaje + "\n" + CubeSummationResources.Error_Tamano_Matriz);

            if (Convert.ToInt32(infoMatriz[1]) < 1 || Convert.ToInt32(infoMatriz[1]) > 1000)
                FuncionesGenericas.ObtenerRespuesta(false, respuestaGeneral.Mensaje + "\n" + CubeSummationResources.Error_Tamano_Operaciones);

            return respuestaGeneral.Estado;
        }

        private int[] ObtenerInformacionOperacion(string[] datos)
        {
            int[] coordenadas = new int[datos.Length - 1];
            for (int i = 1; i < datos.Length; i++)
            {
                coordenadas[i - 1] = Convert.ToInt32(datos[i]);
            }
            return coordenadas;
        }

        public RespuestaGeneral ValidarCoordenadas(int[] informacionOperacion, Cubo cubo)
        {
            for (int i = 0; i < informacionOperacion.Length; i++)
            {
                if (informacionOperacion[i] - 1 > cubo.TamanoMatriz || informacionOperacion[i] - 1 < 0)
                    return FuncionesGenericas.ObtenerRespuesta(false, CubeSummationResources.Error_Coordenadas);
            }
            return FuncionesGenericas.ObtenerRespuesta(true);
        }
    }
}