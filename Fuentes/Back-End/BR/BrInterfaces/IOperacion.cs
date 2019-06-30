using Challenge.CubeSummationNS.Model.Models;

namespace Challenge.CubeSummationNS.BR
{
    public interface IOperacion
    {
        RespuestaGeneral ProcesarOperacionUpdate(OperacionCubo operacion, int[,,] matriz, ref RespuestaGeneral respuestaGeneral);

        RespuestaGeneral ProcesarOperacionQuery(OperacionCubo operacion, int[,,] matriz, ref RespuestaGeneral respuestaGeneral);
    }
}