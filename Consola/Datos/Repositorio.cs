using Consola.Dominio.Empleado;
using Consola.Dominio;
using System.Collections.Generic;

namespace Consola.Datos
{
    public class Repositorio
    {
        static Repositorio Instancia;

        List<EmpleadoAbstracto> ListaBeneficiarios = new List<EmpleadoAbstracto>();
        List<Adelanto> ListaAdelantos = new List<Adelanto>();

        // Similar al patrón singleton
        public static Repositorio ObtenerInstacia()
        {
            if (Instancia == null) Instancia = new Repositorio();
            return Instancia;
        }

        public void CrearAdelanto(Adelanto nuevoAdelanto)
        {
            ListaAdelantos.Add(nuevoAdelanto);
        }

        public void CrearEmpleado(EmpleadoAbstracto nuevoEmpleado)
        {
            ListaBeneficiarios.Add(nuevoEmpleado);
        }

        public Adelanto BuscarAdelantoPorCodigo(string codigoAdelanto)
        {
            return ListaAdelantos.Find(a => a.Codigo == codigoAdelanto);
        }

        public bool ExisteCodigo(string codigoAdelanto)
        {
            return ListaAdelantos.Exists(a => a.Codigo == codigoAdelanto);
        }

        public bool ExisteLegajo(int legajo)
        {
            return ListaBeneficiarios.Exists(e => e.Legajo == legajo);
        }

        public void ActualizarAdelanto(Adelanto adelanto)
        {
            int indiceAdelanto = ListaAdelantos.FindIndex(a => a.Codigo == adelanto.Codigo);
            ListaAdelantos[indiceAdelanto] = adelanto;
        }

        public void ActualizarEmpleado(EmpleadoAbstracto empleado)
        {
            int indiceEmpleado = ListaBeneficiarios.FindIndex(a => a.Legajo == empleado.Legajo);
            ListaBeneficiarios[indiceEmpleado] = empleado;
        }

        public List<Adelanto> ObtenerAdelantosNoCanceladosPorLegajo(int legajo)
        {
            List<Adelanto> adelantosSinCancelar = ListaAdelantos.FindAll(a => a.Beneficiario == legajo && a.FechaCancelacion == null);
            return adelantosSinCancelar;
        }

        public EmpleadoAbstracto BuscarEmpleadoPorLegajo(int legajo)
        {
            return ListaBeneficiarios.Find(e => e.Legajo == legajo);
        }
    }
}
