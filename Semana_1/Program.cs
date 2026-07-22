// Ejemplo Semana 1

//Menú De Opciones
bool continuar = true;

while (continuar)
{
    Console.WriteLine("--------------------------------");
    Console.WriteLine("=== EJEMPLOS C# - SEMANA 1 ===");
    Console.WriteLine("1. Variables y Tipos de Datos");
    Console.WriteLine("2. Condicionales");
    Console.WriteLine("3. Ciclos");
    Console.WriteLine("4. Funciones");
    Console.WriteLine("5. Strings");
    Console.WriteLine("0. Salir");
    Console.Write("\nOpcion: ");
    string opcion = Console.ReadLine();
    Console.WriteLine("--------------------------------");

    switch (opcion)
    {
        case "1":
            EjemploVariables();
            break;
        case "2":
            EjemploCondicionales();
            break;
        case "3":
            EjemploCiclos();
            break;
        case "4":
            EjemploFunciones();
            break;
        case "5":
            EjemploStrings();
            break;
        case "0":
            continuar = false;
            break;
        default:
            Console.WriteLine("Opcion no valida");
            break;
    }
}

// Ejemplos Sobre Declaracion de Variables y Tipos de Datos
void EjemploVariables()
{
    Console.WriteLine("--------------------------------");
    Console.WriteLine("Ejemplo de Variables y Tipos de Datos");
    Console.WriteLine("--------------------------------");
    int numero = 10;
    double decimalNumero = 20.5;
    float flotante = 15.5f;
    decimal decimalPreciso = 100.99m;
    char caracter = 'A';
    bool esVerdadero = true;
    bool esFalso = false;
    string texto = "Hola, Mundo!";
    Console.WriteLine($"Entero {numero}");
    Console.WriteLine($"Decimal {decimalNumero}");
    Console.WriteLine($"Flotante {flotante}");
    Console.WriteLine($"Decimal Preciso {decimalPreciso}");
    Console.WriteLine($"Caracter {caracter}");
    Console.WriteLine($"Booleano Verdadero {esVerdadero}");
    Console.WriteLine($"Booleano Falso {esFalso}");
    Console.WriteLine($"String {texto}");

}

// Ejemplos Sobre Condicionales
void EjemploCondicionales()
{
    Console.WriteLine("--------------------------------");
    Console.WriteLine("Ejemplo de Condicionales");
    Console.WriteLine("--------------------------------");
    Console.Write("Ingresa tu nota: ");
    int nota = int.Parse(Console.ReadLine());

    if (nota >= 61)
    {
        Console.WriteLine("Aprobado");
    }
    else
    {
        Console.WriteLine("Reprobado");
    }

    switch (nota/10)
    {
        case 10:
        case 9:
        case 8:
        case 7:
            Console.WriteLine("Excelente");
            break;
        case 6:
            Console.WriteLine("Bueno");
            break;
        default:
            Console.WriteLine("Necesita Mejorar");
            break;
    }
}

// Ejemplos Sobre Ciclos For, While, Do-While, Foreach
void EjemploCiclos()
    {
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Ejemplo de Ciclos");
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Ciclo For:");
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"Iteracion {i}");
        }

        Console.WriteLine("Ciclo While:");
        int contador = 1;
        while (contador <= 5)
        {
            Console.WriteLine($"Iteracion {contador}");
            contador++;
        }

        Console.WriteLine("Ciclo Do-While:");
        int contadorDo = 1;
        do
        {
            Console.WriteLine($"Iteracion {contadorDo}");
            contadorDo++;
        } while (contadorDo <= 5);

        Console.WriteLine("Foreach:");
        string[] frutas = { "Manzana", "Banana", "Cereza" };
        foreach (string fruta in frutas)
        {
            Console.WriteLine(fruta);
        }
    }

// Ejemplos Sobre Funciones
void EjemploFunciones()
    {
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Ejemplo de Funciones");
        Console.WriteLine("--------------------------------");
        Saludar("Carlos", 25);

        int resultado = Sumar(10, 20);
        Console.WriteLine($"La suma es: {resultado}");

        bool mayorDeEdad = EsMayorDeEdad(20);
        Console.WriteLine($"Es mayor de edad: {mayorDeEdad}");
    }

// Funciion Sin Retorno y con Parámetros
void Saludar(string nombre, int edad)
    {
        Console.WriteLine($"Hola, {nombre}!");
        Console.WriteLine($"Tienes {edad} años.");
    }

// Función con Retorno y con Parámetros
int Sumar(int a, int b)
    {
        return a + b;
    }

// Función con Retorno y con Parámetros
bool EsMayorDeEdad(int edad)
    {
        return edad >= 18;
    }
// Ejemplos Sobre Manipulación de Strings
void EjemploStrings()
    {
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Ejemplo de Strings");
        Console.WriteLine("--------------------------------");
        string texto = " Hola, Mundo! ";

        Console.WriteLine($"Texto Original: '{texto}'");
        Console.WriteLine($"Longitud: {texto.Length}");
        Console.WriteLine($"Mayusculas: {texto.ToUpper()}");
        Console.WriteLine($"Minusculas: {texto.ToLower()}");
        Console.WriteLine($"Trim: '{texto.Trim()}'");
        Console.WriteLine($"Contains 'Mundo': {texto.Contains("Mundo")}");
        Console.WriteLine($"Replace 'Mundo' con 'C#': {texto.Replace("Mundo", "C#")}");
    }