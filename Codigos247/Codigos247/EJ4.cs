using System;
using System.Collections.Generic;
using System.Linq;

namespace Codigos247
{
    internal class EJ4
    {
        private const int VelocidadMaximaBus = 120;
        private const int VelocidadMaximaAvioneta = 200;
        private const int CapacidadMaximaBus = 50;
        private const int CapacidadMaximaAvioneta = 4;
        private const int AlturaMaximaAvioneta = 10000; 
        private static string direccionActual = "Recto";

        static void Main(string[] args)
        {
            List<Vehiculo> vehiculos = InicializarVehiculos();
            Dictionary<Vehiculo, Dictionary<string, int>> accionesContadas = InicializarAcciones(vehiculos);

            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                MostrarMenuVehiculos(vehiculos);
                string opcionVehiculo = Console.ReadLine();

                if (opcionVehiculo.ToLower() == "x")
                {
                    salir = true;
                }
                else if (int.TryParse(opcionVehiculo, out int vehiculoIndex) && vehiculoIndex >= 1 && vehiculoIndex <= vehiculos.Count)
                {
                    Vehiculo vehiculoSeleccionado = vehiculos[vehiculoIndex - 1];
                    salir = ProcesarAccionesVehiculo(vehiculoSeleccionado, accionesContadas);
                }
                else
                {
                    MostrarMensajeError();
                }
            }

            MostrarResumen(accionesContadas);
        }

        private static List<Vehiculo> InicializarVehiculos()
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            for (int i = 1; i <= 3; i++)
            {
                vehiculos.Add(new Bus { Modelo = $"Bus Modelo {i}", CapacidadCombustible = 200 + (i * 10), Velocidad = 0, CantidadPasajeros = CapacidadMaximaBus - (i * 5) });
            }
            for (int i = 1; i <= 3; i++)
            {
                vehiculos.Add(new Avioneta { Modelo = $"Avioneta Modelo {i}", CapacidadCombustible = 100 + (i * 15), Velocidad = 0, CantidadPasajeros = CapacidadMaximaAvioneta - 2, MaxAltitud = AlturaMaximaAvioneta - (i * 1000), NumeroMotores = 2 });
            }
            return vehiculos;
        }

        private static Dictionary<Vehiculo, Dictionary<string, int>> InicializarAcciones(List<Vehiculo> vehiculos)
        {
            var accionesContadas = new Dictionary<Vehiculo, Dictionary<string, int>>();
            foreach (var vehiculo in vehiculos)
            {
                accionesContadas[vehiculo] = new Dictionary<string, int>();
            }
            return accionesContadas;
        }

        private static void MostrarMenuVehiculos(List<Vehiculo> vehiculos)
        {
            Console.WriteLine("Seleccione un vehículo:");
            for (int i = 0; i < vehiculos.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {vehiculos[i].Modelo}");
            }
            Console.WriteLine("x: Salir del programa");
            Console.Write("Ingrese su opción: ");
        }

        private static bool ProcesarAccionesVehiculo(Vehiculo vehiculoSeleccionado, Dictionary<Vehiculo, Dictionary<string, int>> accionesContadas)
        {
            while (true)
            {
                Console.Clear();
                MostrarMenuAcciones(vehiculoSeleccionado);

                string opcionAccion = Console.ReadLine();

                if (opcionAccion.ToLower() == "x")
                {
                    return true;
                }

                if (opcionAccion == "0")
                {
                    break;
                }

                EjecutarAccion(vehiculoSeleccionado, accionesContadas, opcionAccion);
            }

            return false;
        }

        private static void MostrarMenuAcciones(Vehiculo vehiculo)
        {
            Console.WriteLine($"Acciones para {vehiculo.Modelo}:");
            Console.WriteLine($"Velocidad actual: {vehiculo.Velocidad} km/h (Máx: {(vehiculo is Bus ? VelocidadMaximaBus : VelocidadMaximaAvioneta)} km/h)");
            Console.WriteLine($"Cantidad de pasajeros actual: {vehiculo.CantidadPasajeros} (Máx: {(vehiculo is Bus ? CapacidadMaximaBus : CapacidadMaximaAvioneta)})");
            if (vehiculo is Avioneta avioneta)
            {
                Console.WriteLine($"Altitud actual: {avioneta.MaxAltitud} m (Máx: {AlturaMaximaAvioneta} m)");
            }
            Console.WriteLine($"Dirección actual: {direccionActual}");
            Console.WriteLine("1: Acelerar");
            Console.WriteLine("2: Desacelerar");
            Console.WriteLine("3: Subir pasajeros");
            Console.WriteLine("4: Bajar pasajeros");
            if (vehiculo is Bus)
            {
                Console.WriteLine("5: Girar a la izquierda");
                Console.WriteLine("6: Girar a la derecha");
            }
            else
            {
                Console.WriteLine("5: Ascender");
                Console.WriteLine("6: Descender");
            }
            Console.WriteLine("0: Volver al menú de vehículos");
            Console.WriteLine("x: Salir del programa");
            Console.Write("Ingrese su opción: ");
        }

        private static void EjecutarAccion(Vehiculo vehiculo, Dictionary<Vehiculo, Dictionary<string, int>> accionesContadas, string opcionAccion)
        {
            bool accionValida = true;
            switch (opcionAccion)
            {
                case "1":
                    if (vehiculo.Velocidad < (vehiculo is Bus ? VelocidadMaximaBus : VelocidadMaximaAvioneta))
                    {
                        vehiculo.Velocidad += 20;
                        vehiculo.Acelerar();
                        ContarAccion(accionesContadas[vehiculo], "Acelerar");
                    }
                    else
                    {
                        Console.WriteLine("No se puede acelerar más. Velocidad máxima alcanzada.");
                        accionValida = false;
                    }
                    break;
                case "2":
                    if (vehiculo.Velocidad > 0)
                    {
                        vehiculo.Velocidad -= 20;
                        vehiculo.Desacelerar();
                        ContarAccion(accionesContadas[vehiculo], "Desacelerar");
                    }
                    else
                    {
                        Console.WriteLine("No se puede desacelerar. Velocidad actual es 0.");
                        accionValida = false;
                    }
                    break;
                case "3":
                    SubirPasajeros(vehiculo);
                    break;
                case "4":
                    BajarPasajeros(vehiculo);
                    break;
                case "5":
                    if (vehiculo is Bus bus)
                    {
                        ((Bus)vehiculo).GirarIzq();
                        direccionActual = (direccionActual == "Derecha") ? "Recto" : "Izquierda";
                        ContarAccion(accionesContadas[vehiculo], "Girar a la izquierda");
                    }
                    else
                    {
                        ((Avioneta)vehiculo).Ascender();
                        AscenderAvioneta((Avioneta)vehiculo);
                    }
                    break;
                case "6":
                    if (vehiculo is Bus)
                    {
                        ((Bus)vehiculo).GirarDer();
                        direccionActual = (direccionActual == "Izquierda") ? "Recto" : "Derecha";
                        ContarAccion(accionesContadas[vehiculo], "Girar a la derecha");
                    }
                    else
                    {
                        ((Avioneta)vehiculo).Descender();
                        DescenderAvioneta((Avioneta)vehiculo);
                    }
                    break;
                default:
                    accionValida = false;
                    MostrarMensajeError();
                    break;
            }

            if (accionValida)
            {
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        private static void SubirPasajeros(Vehiculo vehiculo)
        {
            Console.Write("Ingrese la cantidad de pasajeros a subir: ");
            if (int.TryParse(Console.ReadLine(), out int cantidad) && cantidad > 0)
            {
                if (vehiculo.CantidadPasajeros + cantidad <= (vehiculo is Bus ? CapacidadMaximaBus : CapacidadMaximaAvioneta))
                {
                    vehiculo.CantidadPasajeros += cantidad;
                    Console.WriteLine($"Se subieron {cantidad} pasajeros. Total actual: {vehiculo.CantidadPasajeros}.");
                }
                else
                {
                    Console.WriteLine("No se pueden subir más pasajeros de los que permite la capacidad máxima.");
                }
            }
            else
            {
                Console.WriteLine("Cantidad inválida.");
            }
        }

        private static void BajarPasajeros(Vehiculo vehiculo)
        {
            Console.Write("Ingrese la cantidad de pasajeros a bajar: ");
            if (int.TryParse(Console.ReadLine(), out int cantidad) && cantidad > 0)
            {
                if (vehiculo.CantidadPasajeros - cantidad >= 0)
                {
                    vehiculo.CantidadPasajeros -= cantidad;
                    Console.WriteLine($"Se bajaron {cantidad} pasajeros. Total actual: {vehiculo.CantidadPasajeros}.");
                }
                else
                {
                    Console.WriteLine("No se pueden bajar más pasajeros de los que hay actualmente.");
                }
            }
            else
            {
                Console.WriteLine("Cantidad inválida.");
            }
        }

        private static void AscenderAvioneta(Avioneta avioneta)
        {
            if (avioneta.MaxAltitud < AlturaMaximaAvioneta)
            {
                avioneta.MaxAltitud += 500;
                if (avioneta.MaxAltitud > AlturaMaximaAvioneta)
                {
                    avioneta.MaxAltitud = AlturaMaximaAvioneta; 
                }
                Console.WriteLine($"La avioneta ha ascendido a {avioneta.MaxAltitud} metros.");
            }
            else
            {
                Console.WriteLine("Altitud máxima alcanzada.");
            }
        }

        private static void DescenderAvioneta(Avioneta avioneta)
        {
            if (avioneta.MaxAltitud > 0)
            {
                avioneta.MaxAltitud -= 500;
                if (avioneta.MaxAltitud < 0)
                {
                    avioneta.MaxAltitud = 0; 
                }
                Console.WriteLine($"La avioneta ha descendido a {avioneta.MaxAltitud} metros.");
            }
            else
            {
                Console.WriteLine("La avioneta ya está en el suelo.");
            }
        }

        private static void MostrarResumen(Dictionary<Vehiculo, Dictionary<string, int>> accionesContadas)
        {
            Console.Clear();
            bool hayAcciones = false;
            Console.WriteLine("Resumen de acciones:");

            foreach (var vehiculo in accionesContadas)
            {
                if (vehiculo.Value.Values.Any(v => v > 0))
                {
                    Console.WriteLine($"\nVehículo: {vehiculo.Key.Modelo}");
                    Console.WriteLine($"  Velocidad final: {vehiculo.Key.Velocidad} km/h");
                    Console.WriteLine($"  Cantidad de pasajeros: {vehiculo.Key.CantidadPasajeros}");

                    if (vehiculo.Key is Bus)
                    {
                        Console.WriteLine($"  Dirección actual: {direccionActual}");
                    }
                    else if (vehiculo.Key is Avioneta avioneta)
                    {
                        Console.WriteLine($"  Altitud final: {avioneta.MaxAltitud} m");
                    }

                    bool accionesRealizadas = false;

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
                Console.WriteLine("\nNo se han realizado acciones.");
            }

            Console.WriteLine("\nPresiona cualquier tecla para salir...");
            Console.ReadKey();
        }


        private static void MostrarMensajeError()
        {
            Console.WriteLine("Opción no válida. Intente nuevamente.");
        }

        private static void ContarAccion(Dictionary<string, int> acciones, string accion)
        {
            if (acciones.ContainsKey(accion))
            {
                acciones[accion]++;
            }
            else
            {
                acciones[accion] = 1;
            }
        }
    }
}