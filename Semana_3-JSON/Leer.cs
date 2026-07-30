using System.Text.Json;
using EjemploJSON;

namespace EjemploJSON
{
    public static class LeerJSON
    {
        // Lee un archivo JSON y lo deserializa como un objeto Ciudad
        public static void LeerArchivoJSON(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("El archivo JSON no existe.");
                return;
            }

            try
            {
                // Leemos el contenido completo del archivo como texto
                string json = File.ReadAllText(path);

                // Configuramos las opciones de deserializacion para ignorar mayusculas y minusculas
                JsonSerializerOptions opciones = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                // Reconstruimos el objeto Ciudad a partir del texto JSON, indicando el tipo esperado
                Ciudad? ciudad = JsonSerializer.Deserialize<Ciudad>(json, opciones);

                if (ciudad != null)
                {
                    ciudad.ImprimirDatosCiudad();
                }
                else
                {
                    Console.WriteLine("No se pudo deserializar la ciudad.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo JSON: {ex.Message}");
            }
        }
    }
}