using System;
using System.Collections.Generic;

namespace Codigos247
{
    internal class EJ2
    {
        public static void Main(string[] args)
        {
            int tamaño;

            do
            {
                Console.Write("Ingrese el tamaño del arreglo (máximo 30): ");
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out tamaño) && tamaño > 0 && tamaño <= 30)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Opción no válida. Por favor, ingrese un número entre 1 y 30.");
                }
            } while (true);

            int[] numeros = GenerarNumerosAleatorios(tamaño, 0, 50);

            Console.WriteLine("\nArreglo original:");
            MostrarArreglo(numeros);

            int orden;

            do
            {
                Console.Write("\n¿Deseas ordenar en forma ascendente (0) o descendente (1)? Ingrese 0 o 1: ");
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out orden) && (orden == 0 || orden == 1))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
                }
            } while (true);

            int movimientos = ordenamiento(numeros, orden);
            if (movimientos >= 0)
            {
                string tipoOrden = (orden == 0) ? "ascendente" : "descendente";
                Console.WriteLine($"\nArreglo ordenado en forma {tipoOrden}:");
                MostrarArreglo(numeros);
                Console.WriteLine($"\nTotal de movimientos realizados: {movimientos}");
            }

            Console.WriteLine("\nPresiona cualquier tecla para cerrar el programa...");
            Console.ReadKey();
        }

        public static int[] GenerarNumerosAleatorios(int cantidad, int min, int max)
        {
            Random random = new Random();
            HashSet<int> numerosUnicos = new HashSet<int>();

            while (numerosUnicos.Count < cantidad)
            {
                int numero = random.Next(min, max + 1);
                numerosUnicos.Add(numero);
            }

            return new List<int>(numerosUnicos).ToArray();
        }

        public static int ordenamiento(int[] arr, int orden)
        {
            if (orden != 0 && orden != 1)
            {
                return -1;
            }

            int n = arr.Length;
            int contadorMovimientos = 0;
            bool esAscendente = (orden == 0);

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (esAscendente && arr[j] > arr[j + 1])
                    {
                        Intercambiar(ref arr[j], ref arr[j + 1]);
                        contadorMovimientos++;
                    }
                    else if (!esAscendente && arr[j] < arr[j + 1])
                    {
                        Intercambiar(ref arr[j], ref arr[j + 1]);
                        contadorMovimientos++;
                    }
                }
            }
            return contadorMovimientos;
        }

        public static void Intercambiar(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        public static void MostrarArreglo(int[] arr)
        {
            foreach (int num in arr)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
        }
    }
}