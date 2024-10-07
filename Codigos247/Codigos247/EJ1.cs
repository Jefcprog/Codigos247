using System;
using System.Collections.Generic;

namespace Codigos247
{
    internal class EJ1
    {
        public static void Main(string[] args)
        {
            int[] numeros;
            List<int> numerosPrimos = new List<int>();
            List<int> numerosNoPrimos = new List<int>();

            string opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1: Generar 10 números aleatorios.");
                Console.WriteLine("2: Ingresar 10 números manualmente.\n");
                Console.Write("Ingrese su opción: ");
                opcion = Console.ReadLine();

                if (opcion != "1" && opcion != "2")
                {
                    Console.WriteLine("Opción no válida. Presione cualquier tecla para intentar de nuevo...");
                    Console.ReadKey();
                }

            } while (opcion != "1" && opcion != "2");

            if (opcion == "1")
            {
                numeros = GenerarNumerosAleatorios(10, 0, 15);
            }
            else
            {
                numeros = IngresarNumerosManual(10, 0, 15);
            }

            Console.WriteLine("\nNúmeros generados:");
            MostrarArreglo(numeros);

            foreach (int num in numeros)
            {
                if (esPrimo(num))
                {
                    numerosPrimos.Add(num);
                }
                else
                {
                    numerosNoPrimos.Add(num);
                }
            }

            Console.WriteLine($"\nLos números primos son: {string.Join(", ", numerosPrimos)}");
            Console.WriteLine($"Los números no primos son: {string.Join(", ", numerosNoPrimos)}");

            double promedio = getPromedioNumerosPrimos(numerosPrimos.ToArray());
            Console.WriteLine($"\nEl promedio de los factoriales de los números primos es: {promedio}");

            Console.WriteLine("\nPresiona cualquier tecla para cerrar el programa...");
            Console.ReadKey();
        }

        public static int[] IngresarNumerosManual(int cantidad, int min, int max)
        {
            int[] numeros = new int[cantidad];
            for (int i = 0; i < cantidad; i++)
            {
                int numero;
                do
                {
                    Console.Write($"Ingrese el número {i + 1} (entre {min} y {max}): ");
                    if (!int.TryParse(Console.ReadLine(), out numero) || numero < min || numero > max)
                    {
                        Console.WriteLine("Número inválido. Intente de nuevo.");
                    }
                    else
                    {
                        break;
                    }
                } while (true);

                numeros[i] = numero;
            }
            return numeros;
        }

        public static int[] GenerarNumerosAleatorios(int cantidad, int min, int max)
        {
            Random random = new Random();
            int[] numeros = new int[cantidad];
            HashSet<int> numerosUnicos = new HashSet<int>();

            for (int i = 0; i < cantidad; i++)
            {
                int numero;
                do
                {
                    numero = random.Next(min, max + 1);
                } while (!numerosUnicos.Add(numero));

                numeros[i] = numero;
            }
            return numeros;
        }

        public static void MostrarArreglo(int[] arr)
        {
            foreach (int num in arr)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
        }

        public static double getPromedioNumerosPrimos(int[] numerosPrimos)
        {
            double sumaFactoriales = 0;
            int contadorPrimos = numerosPrimos.Length;

            foreach (int num in numerosPrimos)
            {
                double factorial = Factorial(num);
                Console.WriteLine($"\nEl factorial de {num} es: {factorial}");
                sumaFactoriales += factorial;
            }

            if (contadorPrimos == 0)
            {
                Console.WriteLine("No se encontraron números primos.");
                return 0;
            }

            double promedio = Math.Round(sumaFactoriales / contadorPrimos, 2);
            Console.WriteLine($"\nLa suma de los factoriales de los números primos es: {sumaFactoriales}");
            return promedio;
        }

        public static bool esPrimo(int numero)
        {
            if (numero <= 1) return false;
            for (int i = 2; i <= Math.Sqrt(numero); i++)
            {
                if (numero % i == 0)
                    return false;
            }
            return true;
        }

        public static double Factorial(int numero)
        {
            double factorial = 1;
            for (int i = 1; i <= numero; i++)
            {
                factorial *= i;
            }
            return factorial;
        }
    }
}
