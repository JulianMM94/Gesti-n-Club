using System;
using System.Collections.Generic;
using System.Linq;

namespace GestionClub
{
    public class MenuOpciones
    {
        private List<Socio> socios = new List<Socio>(); // Lista interna para almacenar socios
        private static Random random = new Random(); // Generador de IDs aleatorios

        #region Metodo Menu de opciones
        public void MostrarMenu()
        {
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("\n--- Sistema de Gestión de Socios ---");
                Console.WriteLine("1. Ingresar socios");
                Console.WriteLine("2. Mostrar lista de socios");
                Console.WriteLine("3. Mostrar total de socios");
                Console.WriteLine("4. Calcular edad promedio");
                Console.WriteLine("5. Clasificar socios por categoría");
                Console.WriteLine("6. Buscar socio de mayor edad");
                Console.WriteLine("7. Salir");
                Console.Write("\nSeleccione una opción: ");

                int opcion;
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Entrada inválida. Ingrese un número válido.");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        Console.Clear();
                        IngresarSocios();
                        pulseTecla();
                        break;
                    case 2:
                        Console.Clear();
                        MostrarListaSocios();
                        pulseTecla();
                        break;
                    case 3:
                        Console.Clear();
                        MostrarTotalSocios();
                        pulseTecla();
                        break;
                    case 4:
                        Console.Clear();
                        MostrarEdadPromedio();
                        pulseTecla();
                        break;
                    case 5:
                        Console.Clear();
                        ClasificarSocios();
                        pulseTecla();
                        break;
                    case 6:
                        Console.Clear();
                        MostrarSocioMayor();
                        pulseTecla();
                        break;
                    case 7:
                        continuar = false;
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Intente nuevamente.");
                        break;
                }
            }
        }
        #endregion

        #region Metodo Ingreso de Socios
        private void IngresarSocios()
        {
                Console.WriteLine("\nRegistro de nuevo socio:");
                string id = GenerarIDUnico(); // Generar ID aleatorio

                Console.Write("Nombre: ");
                string? nombre = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(nombre)) // Evitar nombres vacíos o nulos
                {
                    Console.Write("Nombre inválido. Ingrese un nombre válido: ");
                    nombre = Console.ReadLine();
                }

                Console.Write("Legajo: ");
                int legajo;
                while (!int.TryParse(Console.ReadLine(), out legajo)) // Validar número
                {
                    Console.Write("Legajo inválido. Ingrese un número válido: ");
                }

                Console.Write("Edad: ");
                byte edad;
                while (!byte.TryParse(Console.ReadLine(), out edad)) // Validar edad
                {
                    Console.Write("Edad inválida. Ingrese un número válido: ");
                }

                socios.Add(new Socio(id, nombre, legajo, edad));

                Console.WriteLine($"Socio agregado correctamente con ID: {id}");
        }
        #endregion

        #region Metodo Mostrar el Listado de Socios
        private void MostrarListaSocios()
        {
            if (socios.Count == 0)
            {
                Console.WriteLine("\nNo hay socios registrados.");
                return;
            }

            Console.WriteLine("\nLista de Socios:");
            Console.WriteLine("+-------+----------------------+--------+------+\n" +
                              "| ID    | Nombre               | Legajo | Edad |\n" +
                              "+-------+----------------------+--------+------+");

            foreach (var socio in socios)
            {
                Console.WriteLine($"| {socio.ID,-6} | {socio.Nombre ?? "N/A",-20} | {socio.Legajo,-6} | {socio.Edad,-4} |");
            }

            Console.WriteLine("+-------+----------------------+--------+------+");
        }
        #endregion

        #region Metodo Mostrar el Total de Socios
        private void MostrarTotalSocios()
        {
            Console.WriteLine($"\n\nTotal de socios registrados: {socios.Count}\n\n");
        }
        #endregion

        #region Metodo Mostrar el Promedio de las Edades
        private void MostrarEdadPromedio()
        {
            if (socios.Count == 0)
            {
                Console.WriteLine("\nNo hay socios registrados.");
                return;
            }

            double promedio = socios.Average(s => s.Edad);
            Console.WriteLine($"\nEdad promedio de los socios: {promedio:F2} años\n");
        }
        #endregion

        #region Metodo Categoria de los Socios
        private void ClasificarSocios()
        {
            int infantiles = 0, juveniles = 0, activos = 0, veteranos = 0;

            foreach (var socio in socios)
            {
                if (socio.Edad <= 12)
                    infantiles++;
                else if (socio.Edad <= 17)
                    juveniles++;
                else if (socio.Edad <= 59)
                    activos++;
                else
                    veteranos++;
            }

            Console.WriteLine("\n\nTotal de socios por categorías:");
            Console.WriteLine($"Infantiles (Hasta 12 años): {infantiles}");
            Console.WriteLine($"Juveniles (Hasta 17 años): {juveniles}");
            Console.WriteLine($"Activos (Hasta 59 años): {activos}");
            Console.WriteLine($"Veteranos (60 años o más): {veteranos}");
        }
        #endregion

        #region Metodo Socio de Mayor Edad
        private void MostrarSocioMayor()
        {
            if (socios.Count == 0)
            {
                Console.WriteLine("\nNo hay socios registrados.");
                return;
            }

            Socio socioMayor = socios.OrderByDescending(s => s.Edad).First();

            Console.WriteLine("\n\nSocio de mayor edad:");
            Console.WriteLine("+-------+----------------------+--------+------+\n" +
                              "| ID    | Nombre               | Legajo | Edad |\n" +
                              "+-------+----------------------+--------+------+");

            Console.WriteLine($"| {socioMayor.ID,-6} | {socioMayor.Nombre,-20} | {socioMayor.Legajo,-6} | {socioMayor.Edad,-4} |");
            Console.WriteLine("+-------+----------------------+--------+------+");
        }
        #endregion

        #region Metodo Generacion de ID de los Socios
        private string GenerarIDUnico()
        {
            string id;
            do
            {
                id = random.Next(10000, 99999).ToString(); // Genera un ID único entre 10000 y 99999
            } while (socios.Exists(s => s.ID == id)); // Evita IDs duplicados

            return id;
        }
        #endregion

        #region Metodo Pulsar una Tecla para Continuar
        private void pulseTecla()
        {
            Console.WriteLine("\n\nPresionar tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
        #endregion
    }

    #region Metodo Datos de Socios
    public class Socio
    {
        public string ID { get; set; }
        public string Nombre { get; set; }
        public int Legajo { get; set; }
        public byte Edad { get; set; }

        public Socio(string id, string nombre, int legajo, byte edad)
        {
            ID = id;
            Nombre = nombre;
            Legajo = legajo;
            Edad = edad;
        }
    }
    #endregion
}


