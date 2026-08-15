using System;
using System.Diagnostics;

namespace ArbolBinarioBusqueda
{
    public static class Program
    {
        public static void Main()
        {
            bool continuar = true;

            // Se crea una única instancia del árbol y del graficador que se
            // usarán durante toda la ejecución del programa.
            ArbolBinarioBusqueda arbol = new ArbolBinarioBusqueda();
            Graficador graficador = new Graficador();

            while (continuar)
            {
                MostrarMenu();
                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        InsertarValor(arbol);
                        break;
                    case "2":
                        BuscarValor(arbol);
                        break;
                    case "3":
                        EliminarValor(arbol);
                        break;
                    case "4":
                        MostrarRecorridos(arbol);
                        break;
                    case "5":
                        GenerarGrafico(arbol, graficador);
                        break;
                    case "0":
                        continuar = false;
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, ingrese una opción del menú.");
                        break;
                }
            }
        }

        // Muestra el menú principal de opciones
        public static void MostrarMenu()
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Menú Principal");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("1. Insertar valor");
            Console.WriteLine("2. Buscar valor");
            Console.WriteLine("3. Eliminar valor");
            Console.WriteLine("4. Mostrar recorridos (preorden, inorden, posorden)");
            Console.WriteLine("5. Generar gráfico del árbol (Graphviz)");
            Console.WriteLine("0. Salir");
            Console.WriteLine("--------------------------------");
            Console.Write("Opción: ");
        }

        // Pide un valor al usuario y lo inserta en el árbol
        public static void InsertarValor(ArbolBinarioBusqueda arbol)
        {
            Console.Write("Ingrese el valor a insertar: ");
            string entrada = Console.ReadLine() ?? "";

            if (int.TryParse(entrada, out int valor))
            {
                arbol.Insertar(valor);
                Console.WriteLine($"Valor {valor} insertado correctamente.");
            }
            else
            {
                Console.WriteLine("Entrada inválida. Debe ingresar un número entero.");
            }
        }

        // Pide un valor al usuario y verifica si existe en el árbol
        public static void BuscarValor(ArbolBinarioBusqueda arbol)
        {
            Console.Write("Ingrese el valor a buscar: ");
            string entrada = Console.ReadLine() ?? "";

            if (int.TryParse(entrada, out int valor))
            {
                bool encontrado = arbol.Buscar(valor);
                Console.WriteLine(encontrado
                    ? $"El valor {valor} SÍ existe en el árbol."
                    : $"El valor {valor} NO existe en el árbol.");
            }
            else
            {
                Console.WriteLine("Entrada inválida. Debe ingresar un número entero.");
            }
        }

        // Pide un valor al usuario y lo elimina del árbol si existe
        public static void EliminarValor(ArbolBinarioBusqueda arbol)
        {
            Console.Write("Ingrese el valor a eliminar: ");
            string entrada = Console.ReadLine() ?? "";

            if (int.TryParse(entrada, out int valor))
            {
                arbol.Eliminar(valor);
                Console.WriteLine($"Valor {valor} eliminado (si existía en el árbol).");
            }
            else
            {
                Console.WriteLine("Entrada inválida. Debe ingresar un número entero.");
            }
        }

        // Muestra los tres recorridos del árbol para poder compararlos
        public static void MostrarRecorridos(ArbolBinarioBusqueda arbol)
        {
            arbol.RecorridoPreorden();
            arbol.RecorridoInorden();
            arbol.RecorridoPosorden();
        }

        // Genera el archivo .dot con el estado actual del árbol y lo convierte
        // automáticamente a imagen PNG usando el comando "dot" de Graphviz
        public static void GenerarGrafico(ArbolBinarioBusqueda arbol, Graficador graficador)
        {
            string rutaDot = "arbol.dot";
            string rutaPng = "arbol.png";

            // Primero se genera el archivo de texto .dot con la descripción del árbol.
            graficador.GenerarArchivoDot(arbol, rutaDot);

            // Luego, sin pedir nada más al usuario, se invoca el comando "dot"
            // para convertir ese archivo directamente en una imagen .png.
            ProcessStartInfo info = new ProcessStartInfo
            {
                FileName = "dot",
                Arguments = $"-Tpng {rutaDot} -o {rutaPng}",
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process proceso = Process.Start(info)!)
            {
                proceso.WaitForExit();
            }

            Console.WriteLine($"Gráfico generado automáticamente: {rutaPng}");
        }
    }
}