using Challenge.CubeSummationNS.Model.Models;

namespace Challenge.CubeSummationNS.BR
{
    public interface IOperacion
    {
        RespuestaGeneral ProcesarOperacionUpdate(OperacionCubo operacion, long[,,] matriz, ref RespuestaGeneral respuestaGeneral);

        RespuestaGeneral ProcesarOperacionQuery(OperacionCubo operacion, long[,,] matriz, ref RespuestaGeneral respuestaGeneral);
    }
}