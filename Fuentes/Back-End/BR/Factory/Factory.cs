namespace Challenge.CubeSummationNS.BR
{
    public class Factory
    {
        public Factory()
        {
        }

        public ICubeSummationBR ContruirCubeSummationBR()
        {
            //Se construye alguna configuracion y
            //se aplica inyección de dependencias
            ValidacionDatos validacionDatos = new ValidacionDatos();
            IOperacion operacion = ConstruirOperacionBR();
            return new CubeSummationBR(validacionDatos, operacion);
        }

        public IOperacion ConstruirOperacionBR()
        {
            //Se construye alguna configuracion y
            //se aplica inyección de dependencias
            return new Operacion();
        }
    }
}