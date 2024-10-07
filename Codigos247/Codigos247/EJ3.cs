using System;

namespace Codigos247
{
    internal class EJ3
    {
        static void Main(string[] args)
        {
            Bus bus = new Bus
            {
                Modelo = "El bus",
                CapacidadCombustible = 200,
                Velocidad = 80,
                CantidadPasajeros = 50
            };

            Avioneta avioneta = new Avioneta
            {
                Modelo = "La avioneta",
                CapacidadCombustible = 100,
                Velocidad = 200,
                CantidadPasajeros = 4,
                MaxAltitud = 3000,
                NumeroMotores = 2
            };

            bus.Acelerar();
            bus.Desacelerar();
            bus.GirarIzq();
            bus.GirarDer();

            avioneta.Acelerar();
            avioneta.Desacelerar();
            avioneta.Ascender();
            avioneta.Descender();

            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
