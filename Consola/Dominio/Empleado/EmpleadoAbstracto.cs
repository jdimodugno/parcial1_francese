namespace Consola.Dominio.Empleado
{
    // Empleado es equivalente a Beneficiario
    public abstract class EmpleadoAbstracto
    {
        public int Legajo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public double Sueldo { get; set; }

        public abstract int ObtenerBeneficio();
    }
}
