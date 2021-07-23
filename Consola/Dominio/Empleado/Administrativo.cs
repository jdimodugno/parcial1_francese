using System;
namespace Consola.Dominio.Empleado
{
    public class Administrativo : EmpleadoAbstracto
    {
        public Administrativo(
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

        public override int ObtenerBeneficio() => 5;
    }
}
