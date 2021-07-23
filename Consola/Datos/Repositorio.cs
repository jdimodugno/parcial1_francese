using Consola.Dominio.Empleado;
using Consola.Dominio;
using System.Collections.Generic;
using System;

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

        public void CrearAdelanto(Adelanto nuevoAdelanto) {
            ListaAdelantos.Add(nuevoAdelanto);
        }

        // codigoAdelanto sería el seleccionado en la grilla
        public Adelanto BuscarAdelantoPorCodigo(string codigoAdelanto)
        {
            return ListaAdelantos.Find(a => a.Codigo == codigoAdelanto);
        }

        // codigoAdelanto sería el seleccionado en la grilla
        public bool ExisteCodigo(string codigoAdelanto)
        {
            return ListaAdelantos.Exists(a => a.Codigo == codigoAdelanto);
        }

        // codigoAdelanto sería el seleccionado en la grilla
        public void ActualizarAdelanto(string codigoAdelanto)
        {
            Adelanto adelantoActual = ListaAdelantos.Find(a => a.Codigo == codigoAdelanto);
        }

        // codigoAdelanto sería el seleccionado en la grilla
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
