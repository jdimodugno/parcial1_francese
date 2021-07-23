using System;
using System.Collections.Generic;
using System.Linq;
using Consola.Datos;
using Consola.Dominio;
using Consola.Dominio.Empleado;

namespace Consola.Negocio
{
    public class AdelantoNegocio
    {
        public Adelanto BuscarAdelanto(string codigo)
        {
            Repositorio repo = Repositorio.ObtenerInstacia();
            return repo.BuscarAdelantoPorCodigo(codigo);
        }

        public void CrearAdelanto(
            string codigo,
            int legajo,
            double importeSolicitado
        )
        {
            if(!ValidarCodigo(codigo)) throw new Exception("Código Inválido");
            if(!ValidarImporteMaximo(importeSolicitado, legajo)) throw new Exception("Importe excede la mitad del sueldo");
            try
            {
                ValidarSituacionActual(importeSolicitado, legajo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool ValidarCodigo(string codigo)
        {
            // valido logitud
            if (codigo.Length != 12) return false;

            string parteNumerica = codigo.Substring(0, 4);
            string parteAlfabetica = codigo.Substring(4, 8);

            if (!ParteNumericaValida(parteNumerica)) return false;
            if (!ParteAlfabeticaValida(parteAlfabetica)) return false;
            if (ExisteCodigo(codigo)) return false;

            return true;
        }

        private bool ParteNumericaValida(string parteNumerica)
        {
            return Int32.TryParse(parteNumerica, out _);
        }

        private bool ParteAlfabeticaValida(string parteAlfabetica)
        {
            return parteAlfabetica.All(char.IsLetter);
        }

        private bool ExisteCodigo(string codigo)
        {
            Repositorio repo = Repositorio.ObtenerInstacia();
            return repo.ExisteCodigo(codigo);
        }

        private bool ValidarImporteMaximo(double importeSolicitado, int legajo)
        {
            Repositorio repo = Repositorio.ObtenerInstacia();
            EmpleadoAbstracto empleado = repo.BuscarEmpleadoPorLegajo(legajo);
            return importeSolicitado <= (empleado.Sueldo / 2);
        }

        private void ValidarSituacionActual(double importeSolicitado, int legajo)
        {
            Repositorio repo = Repositorio.ObtenerInstacia();
            List<Adelanto> adelantosSinCancelar = repo.ObtenerAdelantosNoCanceladosPorLegajo(legajo);

            if (adelantosSinCancelar.Count() == 3) throw new Exception("El empleado ya cuenta con 3 adelantos impagos");

        }

    }
}
