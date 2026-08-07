using Proyecto1.Xml;
using Proyecto1.ListaCiudad;
using Proyecto1.Clases;

public static class Program
{
    public static void Main()
    {
        bool continuar = true;
        ListaCiudad listaCiudades = new ListaCiudad();

        while (continuar)
        {
            MostrarMenu();
            string opcion = Console.ReadLine() ?? "";

            switch (opcion)
            {
                case "1":
                    LeerArchivo(listaCiudades);
                    break;
                case "2":
                    EscribirArchivo();
                    break;
                case "3":
                    EliminarCiudad(listaCiudades);
                    break;
                case "4":
                    listaCiudades.RecorrerLista();
                    break;
                case "5":
                    BuscarCiudad(listaCiudades);
                    break;
                case "6":
                    Console.WriteLine($"Tamaño de la lista: {listaCiudades.Tamanio}");
                    break;
                case "7":
                    ImprimirCeldasCiudad(listaCiudades);
                    break;
                case "8":
                    LimpiarLista(listaCiudades);
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
        Console.WriteLine("1. Leer Archivo XML");
        Console.WriteLine("2. Escribir Archivo XML");
        Console.WriteLine("3. Eliminar Ciudad de la Lista");
        Console.WriteLine("4. Recorrer Lista de Ciudades");
        Console.WriteLine("5. Buscar Ciudad por Nombre");
        Console.WriteLine("6. Tamaño de la Lista");
        Console.WriteLine("7. Imprimir Celdas de una Ciudad");
        Console.WriteLine("8. Limpiar Lista de Ciudades");
        Console.WriteLine("0. Salir");
        Console.WriteLine("--------------------------------");
        Console.Write("Opción: ");
    }

    public static void LeerArchivo(ListaCiudad listaCiudades)
    {
        string path = @"./Entrada.xml";
        LeerXML.LeerArchivoXML(path, listaCiudades);
    }

    // Llama a la función que escribe un archivo XML de salida
    public static void EscribirArchivo()
    {
        string path = @"./Salida.xml";
        EscribirXML.EscribirArchivoXML(path);
    }

    public static void EliminarCiudad(ListaCiudad listaCiudades)
    {
        Console.Write("Ingrese el nombre de la ciudad a eliminar: ");
        string nombreEliminar = Console.ReadLine() ?? "";
        listaCiudades.EliminarCiudad(nombreEliminar);
    }

    public static void BuscarCiudad(ListaCiudad listaCiudades)
    {
        Console.Write("Ingrese el nombre de la ciudad a buscar: ");
        string nombreBuscar = Console.ReadLine() ?? "";
        NodoCiudad? nodoEncontrado = listaCiudades.BuscarCiudad(nombreBuscar);

        if (nodoEncontrado != null)
        {
            Console.WriteLine("Ciudad encontrada:");
            nodoEncontrado.Dato.ImprimirDatosCiudad();
        }
        else
        {
            Console.WriteLine($"Ciudad '{nombreBuscar}' no encontrada en la lista.");
        }
    }

    public static void ImprimirCeldasCiudad(ListaCiudad listaCiudades)
    {
        Console.Write("Ingrese el nombre de la ciudad para imprimir sus celdas: ");
        string nombreCiudad = Console.ReadLine() ?? "";
        NodoCiudad? nodoCiudad = listaCiudades.BuscarCiudad(nombreCiudad);

        if (nodoCiudad != null)
        {
            Ciudad ciudad = nodoCiudad.Dato;
            ciudad.Malla.RecorrerLista();
        }
        else
        {
            Console.WriteLine($"Ciudad '{nombreCiudad}' no encontrada en la lista.");
        }
    }

    public static void LimpiarLista(ListaCiudad listaCiudades)
    {
        Console.WriteLine("Limpiando la lista de ciudades.");
        listaCiudades.LimpiarLista();
    }
}