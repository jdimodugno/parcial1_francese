using System;
using Consola.Dominio;
using Consola.Negocio;

namespace Consola
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            try
            {
                AdelantoNegocio adelantoNegocio = new AdelantoNegocio();
                EmpleadoNegocio empleadoNegocio = new EmpleadoNegocio();

                string codigo0 = "0000abcdabcd";
                string codigo1 = "0001abcdabcd";
                string codigo2 = "0002abcdabcd";
                string codigo3 = "0003abcdabcd";
                string codigo4 = "0004abcdabcd";

                empleadoNegocio.CrearEmpleado(1, "Juan", "Di Modugno", 20000, TipoEmpleadoEnum.Operario);
                //empleadoNegocio.CrearEmpleado(2, "Carlos", "Di Modugno", 20000, TipoEmpleado.Administrativo);
                //empleadoNegocio.CrearEmpleado(3, "Germán", "Di Modugno", 20000, TipoEmpleado.Directivo);

                adelantoNegocio.CrearAdelanto(codigo0, 1, 10000);
                adelantoNegocio.CrearAdelanto(codigo1, 1, 9000);
                adelantoNegocio.CrearAdelanto(codigo2, 1, 1000);

                // cancelo deuda
                adelantoNegocio.IngresarPago(codigo0, 5000);
                adelantoNegocio.IngresarPago(codigo0, 4500);

                // pruebo pedir mas plata
                adelantoNegocio.CrearAdelanto(codigo3, 1, 1000);

                Console.WriteLine("Codigo Valido");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
