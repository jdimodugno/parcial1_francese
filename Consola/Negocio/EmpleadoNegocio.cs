using System;
using Consola.Datos;
using Consola.Dominio;
using Consola.Dominio.Empleado;

namespace Consola.Negocio
{
    public class EmpleadoNegocio
    {
        public void CrearEmpleado(
            int legajo,
            string nombre,
            string apellido,
            double sueldo,
            TipoEmpleadoEnum tipo
        )
        {
            Repositorio repo = Repositorio.ObtenerInstacia();
            EmpleadoAbstracto nuevoEmpleado;

            if (ExisteLegajo(legajo)) throw new Exception("El empleado que desea dar de alta ya existe");

            if (tipo == TipoEmpleadoEnum.Operario)
                nuevoEmpleado = new Operario(legajo, nombre, apellido, sueldo);
            else if (tipo == TipoEmpleadoEnum.Administrativo)
                nuevoEmpleado = new Administrativo(legajo, nombre, apellido, sueldo);
            else
                nuevoEmpleado = new Directivo(legajo, nombre, apellido, sueldo);

            repo.CrearEmpleado(nuevoEmpleado);
        }

        private bool ExisteLegajo(int legajo)
        {
            Repositorio repo = Repositorio.ObtenerInstacia();
            return repo.ExisteLegajo(legajo);
        }
    }
}
