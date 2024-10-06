using System;
using System.Collections.Generic;

namespace Codigos247
{
    internal class EJ4
    {
        static void Main(string[] args)
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            Dictionary<Vehiculo, Dictionary<string, int>> accionesContadas = new Dictionary<Vehiculo, Dictionary<string, int>>();

            for (int i = 1; i <= 3; i++)
            {
                Bus bus = new Bus
                {
                    Modelo = $"Bus Modelo {i}",
                    CapacidadCombustible = 200 + (i * 10),
                    Velocidad = 80 + (i * 5),
                    CantidadPasajeros = 50 + (i * 2)
                };
                vehiculos.Add(bus);
                accionesContadas[bus] = new Dictionary<string, int>();
            }

            for (int i = 1; i <= 3; i++)
            {
                Avioneta avioneta = new Avioneta
                {
                    Modelo = $"Avioneta Modelo {i}",
                    CapacidadCombustible = 100 + (i * 15),
                    Velocidad = 200 + (i * 10),
                    CantidadPasajeros = 4,
                    MaxAltitud = 3000 + (i * 500),
                    NumeroMotores = 2
                };
                vehiculos.Add(avioneta);
                accionesContadas[avioneta] = new Dictionary<string, int>();
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Seleccione un vehículo:");
                for (int i = 0; i < vehiculos.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {vehiculos[i].Modelo}");
                }
                Console.WriteLine("0: Salir del menú de vehículos");
                Console.WriteLine("x: Salir del programa");
                Console.Write("Ingrese su opción: ");

                string opcionVehiculo = Console.ReadLine();

                if (opcionVehiculo.ToLower() == "x")
                {
                    MostrarResumen(accionesContadas);
                    break;
                }

                if (int.TryParse(opcionVehiculo, out int vehiculoIndex) && vehiculoIndex >= 1 && vehiculoIndex <= vehiculos.Count)
                {
                    Vehiculo vehiculoSeleccionado = vehiculos[vehiculoIndex - 1];

                    bool accionesRealizadas = false;

                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine($"Acciones para {vehiculoSeleccionado.Modelo}:");
                        Console.WriteLine("1: Acelerar");
                        Console.WriteLine("2: Desacelerar");
                        if (vehiculoSeleccionado.EsBus())
                        {
                            Console.WriteLine("3: Girar a la izquierda");
                            Console.WriteLine("4: Girar a la derecha");
                        }
                        else
                        {
                            Console.WriteLine("3: Ascender");
                            Console.WriteLine("4: Descender");
                        }
                        Console.WriteLine("0: Volver al menú de vehículos");
                        Console.WriteLine("x: Salir del programa");
                        Console.Write("Ingrese su opción: ");

                        string opcionAccion = Console.ReadLine();

                        if (opcionAccion.ToLower() == "x")
                        {
                            MostrarResumen(accionesContadas);
                            return;
                        }

                        if (opcionAccion == "0")
                        {
                            break;
                        }

                        switch (opcionAccion)
                        {
                            case "1":
                                vehiculoSeleccionado.Acelerar();
                                ContarAccion(accionesContadas[vehiculoSeleccionado], "Acelerar");
                                accionesRealizadas = true;
                                break;
                            case "2":
                                vehiculoSeleccionado.Desacelerar();
                                ContarAccion(accionesContadas[vehiculoSeleccionado], "Desacelerar");
                                accionesRealizadas = true;
                                break;
                            case "3":
                                if (vehiculoSeleccionado.EsBus())
                                {
                                    ((Bus)vehiculoSeleccionado).GirarIzq();
                                    ContarAccion(accionesContadas[vehiculoSeleccionado], "Girar a la izquierda");
                                    accionesRealizadas = true;
                                }
                                else
                                {
                                    ((Avioneta)vehiculoSeleccionado).Ascender();
                                    ContarAccion(accionesContadas[vehiculoSeleccionado], "Ascender");
                                    accionesRealizadas = true;
                                }
                                break;
                            case "4":
                                if (vehiculoSeleccionado.EsBus())
                                {
                                    ((Bus)vehiculoSeleccionado).GirarDer();
                                    ContarAccion(accionesContadas[vehiculoSeleccionado], "Girar a la derecha");
                                    accionesRealizadas = true;
                                }
                                else
                                {
                                    ((Avioneta)vehiculoSeleccionado).Descender();
                                    ContarAccion(accionesContadas[vehiculoSeleccionado], "Descender");
                                    accionesRealizadas = true;
                                }
                                break;
                            default:
                                Console.WriteLine("Opción no válida. Presione cualquier tecla para intentar de nuevo...");
                                Console.ReadKey();
                                break;
                        }

                        if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.X)
                        {
                            MostrarResumen(accionesContadas);
                            return;
                        }

                        Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                    }

                    if (!accionesRealizadas)
                    {
                        MostrarResumen(accionesContadas);
                        return;
                    }
                }
                else if (opcionVehiculo == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Opción no válida. Presione cualquier tecla para intentar de nuevo...");
                    Console.ReadKey();
                }
            }

            MostrarResumen(accionesContadas);
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }

        private static void ContarAccion(Dictionary<string, int> contadorAcciones, string accion)
        {
            if (contadorAcciones.ContainsKey(accion))
            {
                contadorAcciones[accion]++;
            }
            else
            {
                contadorAcciones[accion] = 1;
            }
        }

        private static void MostrarResumen(Dictionary<Vehiculo, Dictionary<string, int>> accionesContadas)
        {
            Console.Clear();
            bool hayAcciones = false;
            Console.WriteLine("Resumen de acciones:");
            foreach (var vehiculo in accionesContadas)
            {
                if (vehiculo.Value.Count > 0)
                {
                    bool accionesRealizadas = false;
                    Console.WriteLine($"\nVehículo: {vehiculo.Key.Modelo}\n");
                    foreach (var accion in vehiculo.Value)
                    {
                        if (accion.Value > 0)
                        {
                            string cantidad = accion.Value == 1 ? "vez" : "veces";
                            Console.WriteLine($"  {accion.Key}: {accion.Value} {cantidad}");
                            accionesRealizadas = true;
                        }
                    }
                    if (accionesRealizadas) hayAcciones = true;
                }
            }

            if (!hayAcciones)
            {
                Console.WriteLine("\nNo se han realizado acciones.\n");
            }
            Console.WriteLine("\nPresiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }

    public static class VehiculoExtensions
    {
        public static bool EsBus(this Vehiculo vehiculo)
        {
            return vehiculo is Bus;
        }
    }
}
