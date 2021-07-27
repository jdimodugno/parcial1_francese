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
            try
            {
                ValidarCodigo(codigo);
                ValidarImporteMaximo(importeSolicitado, legajo);
                ValidarSituacionActual(importeSolicitado, legajo);
                Repositorio repo = Repositorio.ObtenerInstacia();
                Adelanto nuevoAdelanto = new Adelanto()
                {
                    Codigo = codigo,
                    Beneficiario = legajo,
                    ImporteOtorgado = importeSolicitado,
                    FechaOtorgamiento = DateTime.Now,
                };

                repo.CrearAdelanto(nuevoAdelanto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IngresarPago(string codigo, double montoPago)
        {
            Repositorio repo = Repositorio.ObtenerInstacia();
            Adelanto adelantoAPagar = repo.BuscarAdelantoPorCodigo(codigo);

            if (montoPago > adelantoAPagar.SaldoAdeudado)
            {
                throw new Exception($"Queres pagar ${montoPago} pero la deuda es de ${adelantoAPagar.SaldoAdeudado}. Hay un excedente de ${montoPago - adelantoAPagar.SaldoAdeudado}");
            }

            adelantoAPagar.ImportePagado += montoPago;

            DateTime fechaAplicacionBeneficio = adelantoAPagar.FechaOtorgamiento.AddDays(15);

            if (adelantoAPagar.SaldoAdeudado > 0 && DateTime.Now < fechaAplicacionBeneficio)
            {
                EmpleadoAbstracto empleado = repo.BuscarEmpleadoPorLegajo(adelantoAPagar.Beneficiario);

                double factorBeneficio = empleado.ObtenerBeneficio() / 100.0;
                double importeBeneficio = montoPago * factorBeneficio;

                if (importeBeneficio > adelantoAPagar.SaldoAdeudado)
                {
                    adelantoAPagar.ImporteBeneficio += adelantoAPagar.SaldoAdeudado - importeBeneficio;
                }
                else
                {
                    adelantoAPagar.ImporteBeneficio += importeBeneficio;
                }
            }

            repo.ActualizarAdelanto(adelantoAPagar);
            if (adelantoAPagar.SaldoAdeudado == 0) CancelarAdelanto(adelantoAPagar.Codigo);
        }

        private void CancelarAdelanto(string codigo)
        {
            Repositorio repo = Repositorio.ObtenerInstacia();
            Adelanto adelantoACancelar = repo.BuscarAdelantoPorCodigo(codigo);

            adelantoACancelar.Beneficiario = 0;
            adelantoACancelar.FechaCancelacion = DateTime.Now;

            repo.ActualizarAdelanto(adelantoACancelar);
        }

        private void ValidarCodigo(string codigo)
        {
            // valido logitud
            if (codigo.Length != 12) throw new Exception("Código Inválido");

            string parteNumerica = codigo.Substring(0, 4);
            string parteAlfabetica = codigo.Substring(4, 8);

            if (!ParteNumericaValida(parteNumerica)) throw new Exception("Código Inválido");
            if (!ParteAlfabeticaValida(parteAlfabetica)) throw new Exception("Código Inválido");
            if (ExisteCodigo(codigo)) throw new Exception("Código Inválido");
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

        private void ValidarImporteMaximo(double importeSolicitado, int legajo)
        {
            Repositorio repo = Repositorio.ObtenerInstacia();
            EmpleadoAbstracto empleado = repo.BuscarEmpleadoPorLegajo(legajo);
            if (importeSolicitado > (empleado.Sueldo / 2)) throw new Exception("Importe excede la mitad del sueldo");
        }

        private void ValidarSituacionActual(double importeSolicitado, int legajo)
        {
            Repositorio repo = Repositorio.ObtenerInstacia();
            List<Adelanto> adelantosSinCancelar = repo.ObtenerAdelantosNoCanceladosPorLegajo(legajo);

            if (adelantosSinCancelar.Count() == 3) throw new Exception("El empleado ya cuenta con 3 adelantos impagos");

            double saldoAdeudado = adelantosSinCancelar.Sum(a => a.SaldoAdeudado);
            EmpleadoAbstracto empleado = repo.BuscarEmpleadoPorLegajo(legajo);

            if ((importeSolicitado + saldoAdeudado) > empleado.Sueldo) throw new Exception("El importe solicitado excede el permitido");
        }
    }
}
