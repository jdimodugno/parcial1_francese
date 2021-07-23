using System;
using Consola.Negocio;

namespace Consola
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            try
            {
                AdelantoNegocio negocio = new AdelantoNegocio();
                Console.WriteLine("Codigo Valido");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
