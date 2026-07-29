namespace Semana_2.Modelos
{
    // CLASE: SensorSuelo.
    // HERENCIA: Hereda de la clase Sensor.
    public class SensorSuelo : Sensor
    {
        // ATRIBUTO: Específico de sensores de suelo.
        public string TipoMedicion { get; private set; }

        // MÉTODO: Constructor.
        // Llama al constructor de la clase padre con 'base'.
        public SensorSuelo(string id, string nombre, string tipoMedicion, EstacionBase estacion)
            : base(id, nombre, estacion)
        {
            TipoMedicion = tipoMedicion;
        }

        // MÉTODO: Implementación del método abstracto de la clase padre.
        // POLIMORFISMO: Versión específica para sensores de suelo.
        public override void MostrarInfo()
        {
            Console.WriteLine($"[SENSOR DE SUELO]");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Tipo: {TipoMedicion}");
            Console.WriteLine($"Conectado a: {Estacion.Nombre}");
        }

        // MÉTODO: Implementación del método abstracto.
        // POLIMORFISMO: Comportamiento específico para medir suelo.
        public override void RealizarMedicion()
        {
            Console.WriteLine($"Midiendo {TipoMedicion} del suelo...");
        }
    }
}