using EjemploJSON;

bool continuar = true;

while (continuar)
{
    MostrarMenu();
    string opcion = Console.ReadLine() ?? "0";
    Console.WriteLine("--------------------------------");

    switch (opcion)
    {
        case "1":
            LeerArchivoJson();
            break;
        case "2":
            EscribirArchivoJson();
            break;
        case "0":
            continuar = false;
            break;
        default:
            Console.WriteLine("Opción no válida. Intenta de nuevo.");
            break;
    }
}

// Muestra el menú principal de opciones
static void MostrarMenu()
{
    Console.WriteLine("--------------------------------");
    Console.WriteLine("Menú Principal");
    Console.WriteLine("--------------------------------");
    Console.WriteLine("1. Leer Archivo JSON");
    Console.WriteLine("2. Escribir Archivo JSON");
    Console.WriteLine("0. Salir");
    Console.WriteLine("--------------------------------");
    Console.Write("Opción: ");
}

// Llama a la función que lee el archivo JSON de entrada
static void LeerArchivoJson()
{
    string path = @"./Entrada.json";
    LeerJSON.LeerArchivoJSON(path);
}

// Llama a la función que escribe un archivo JSON de salida
static void EscribirArchivoJson()
{
    string path = @"./Salida.json";
    EscribirJSON.EscribirArchivoJSON(path);
}