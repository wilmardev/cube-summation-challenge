using System.Collections.Generic;

namespace Challenge.CubeSummationNS.Model.Models
{
    public class Cubo
    {
        public int TamanoMatriz { get; set; }
        public int CantidadOperaciones { get; set; }
        public List<OperacionCubo> OperacionesCubo { get; set; }
    }
}