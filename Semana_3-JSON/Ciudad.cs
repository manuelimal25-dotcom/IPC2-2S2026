using System.Text.Json.Serialization;

namespace EjemploJSON
{
    public class Ciudad
    {
        // El atributo JsonPropertyName indica el nombre que tendra esta propiedad en el JSON
        [JsonPropertyName("nombre")]
        public string Nombre { get; private set; }

        [JsonPropertyName("filas")]
        public int Filas { get; private set; }

        [JsonPropertyName("columnas")]
        public int Columnas { get; private set; }

        public Ciudad(string nombre, int filas, int columnas)
        {
            Nombre = nombre;
            Filas = filas;
            Columnas = columnas;
        }

        public void ImprimirDatosCiudad()
        {
            Console.WriteLine($"Ciudad: {Nombre} - Filas: {Filas} - Columnas: {Columnas}");
        }
    }
}