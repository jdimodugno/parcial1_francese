namespace Consola.Dominio.Empleado
{
    public class Directivo : EmpleadoAbstracto
    {
        public Directivo(
            int _legajo,
            string _nombre,
            string _apellido,
            double _sueldo
        ) : base()
        {
            Legajo = _legajo;
            Nombre = _nombre;
            Apellido = _apellido;
            Sueldo = _sueldo;
        }

        public override int ObtenerBeneficio() => 1;
    }
}
