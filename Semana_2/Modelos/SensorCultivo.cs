namespace Semana_2.Modelos
{
    // CLASE: SensorCultivo.
    // HERENCIA: Hereda de la clase Sensor.
    public class SensorCultivo : Sensor
    {
        // ATRIBUTO: Específico de sensores de cultivo.
        public string Indicador { get; private set; }

        // MÉTODO: Constructor.
        public SensorCultivo(string id, string nombre, string indicador, EstacionBase estacion)
            : base(id, nombre, estacion)
        {
            Indicador = indicador;
        }

        // MÉTODO: Implementación del método abstracto.
        // POLIMORFISMO: Versión específica para sensores de cultivo.
        public override void MostrarInfo()
        {
            Console.WriteLine($"[SENSOR DE CULTIVO]");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Indicador: {Indicador}");
            Console.WriteLine($"Conectado a: {Estacion.Nombre}");
        }

        // MÉTODO: Implementación del método abstracto.
        // POLIMORFISMO: Comportamiento específico para medir cultivo.
        public override void RealizarMedicion()
        {
            Console.WriteLine($"Analizando {Indicador} del cultivo...");
        }
    }
}