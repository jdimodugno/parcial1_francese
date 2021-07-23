using System;

namespace Consola.Dominio
{
    public class Adelanto
    {
        public string Codigo { get; set; }
        public DateTime FechaOtorgamiento { get; set; }
        public DateTime? FechaCancelacion { get; set; } = null;

        public double ImporteOtorgado { get; set; }
        public double ImportePagado { get; set; }
        public double ImporteBeneficio { get; set; }

        // Relación con empleado
        public int Beneficiario { get; set; }

        public double SaldoAdeudado => (ImporteOtorgado - ImportePagado - ImporteBeneficio);
    }
}
