using Semana_2.Modelos;

Console.WriteLine("SISTEMA DE AGRICULTURA DE PRECISION");
Console.WriteLine("------------------------------------");

// OBJETOS: Instancias de la clase EstacionBase
Console.WriteLine("--- Creando Estaciones Base ---");
EstacionBase estacion1 = new EstacionBase("e01", "Estación Central");
EstacionBase estacion2 = new EstacionBase("e02", "Estación Metropolitana");

// Llamando MÉTODOS de los objetos
estacion1.MostrarInfo();
estacion2.MostrarInfo();

Console.WriteLine("\n--- Creando Sensores ---");
SensorSuelo sensorS1 = new SensorSuelo("s01", "Sensor Suelo 01", "Humedad", estacion1);
SensorSuelo sensorS2 = new SensorSuelo("s02", "Sensor Suelo 02", "Temperatura", estacion2);
SensorCultivo sensorC1 = new SensorCultivo("t01", "Sensor Cultivo 01", "Indice vegetal", estacion1);
SensorCultivo sensorC2 = new SensorCultivo("t02", "Sensor Cultivo 03", "Estrés hídrico/térmico", estacion2);

// POLIMORFISMO: Cada sensor ejecuta su propia versión de MostrarInfo()
Console.WriteLine("\n--- Informacion de Sensores ---\n");
sensorS1.MostrarInfo();
Console.WriteLine();
sensorS2.MostrarInfo();
Console.WriteLine();
sensorC1.MostrarInfo();
Console.WriteLine();
sensorC2.MostrarInfo();

// POLIMORFISMO: Cada sensor ejecuta su propia versión de RealizarMedicion()
Console.WriteLine("\n--- Realizando Mediciones ---");
sensorS1.RealizarMedicion();
sensorS2.RealizarMedicion();
sensorC1.RealizarMedicion();
sensorC2.RealizarMedicion();

// Llamando MÉTODOS de objetos EstacionBase
// La presentación (Console.WriteLine) ahora vive aquí, no dentro del modelo (SRP)
Console.WriteLine("\n--- Gestion de Estaciones ---");
estacion2.Desactivar();
Console.WriteLine($"Estacion {estacion2.Nombre} desactivada.");
estacion2.Activar();
Console.WriteLine($"Estacion {estacion2.Nombre} activada.");

Console.WriteLine("\n--- POLIMORFISMO en accion ---");
List<Sensor> sensores = new List<Sensor>();
sensores.Add(sensorS1);  // SensorSuelo
sensores.Add(sensorS2);  // SensorSuelo
sensores.Add(sensorC1);  // SensorCultivo
sensores.Add(sensorC2);  // SensorCultivo

Console.WriteLine("Recorriendo todos los sensores:");
foreach (Sensor sensor in sensores)
{
    // POLIMORFISMO: Aunque todos son de tipo Sensor, cada uno ejecuta su versión específica
    sensor.MostrarInfo();
    sensor.RealizarMedicion();
    Console.WriteLine();
}
