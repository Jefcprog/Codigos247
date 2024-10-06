using System;

namespace Codigos247
{
    public abstract class Vehiculo
    {
        public string Modelo { get; set; }
        public float CapacidadCombustible { get; set; }
        public float Velocidad { get; set; }
        public int CantidadPasajeros { get; set; }

        public abstract void Acelerar();
        public abstract void Desacelerar();
    }

    public class Bus : Vehiculo
    {
        public override void Acelerar()
        {
            Console.WriteLine($"{Modelo} está acelerando.");
        }

        public override void Desacelerar()
        {
            Console.WriteLine($"{Modelo} está desacelerando.");
        }

        public void GirarIzq()
        {
            Console.WriteLine($"{Modelo} gira a la izquierda.");
        }

        public void GirarDer()
        {
            Console.WriteLine($"{Modelo} gira a la derecha.\n");
        }
    }

    public class Avioneta : Vehiculo
    {
        public float MaxAltitud { get; set; }
        public int NumeroMotores { get; set; }

        public override void Acelerar()
        {
            Console.WriteLine($"{Modelo} está acelerando.");
        }

        public override void Desacelerar()
        {
            Console.WriteLine($"{Modelo} está desacelerando.");
        }

        public void Ascender()
        {
            Console.WriteLine($"{Modelo} está ascendiendo.");
        }

        public void Descender()
        {
            Console.WriteLine($"{Modelo} está descendiendo.\n");
        }
    }
}
