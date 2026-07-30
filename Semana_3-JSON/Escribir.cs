using System.Text.Json;
using EjemploJSON;

namespace EjemploJSON
{
    public static class EscribirJSON
    {
        // Serializa un objeto Ciudad y lo guarda como archivo JSON
        public static void EscribirArchivoJSON(string path)
        {
            try
            {
                // Creamos un objeto Ciudad de ejemplo
                Ciudad ciudad = new Ciudad("CiudadDemo", 10, 10);

                // Configuramos las opciones de serializacion para que el JSON quede legible
                JsonSerializerOptions opciones = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                // Convertimos el objeto Ciudad en una cadena de texto JSON
                string json = JsonSerializer.Serialize(ciudad, opciones);

                // Guardamos la cadena JSON en el archivo especificado
                File.WriteAllText(path, json);
                Console.WriteLine($"Archivo JSON creado exitosamente en: {path}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al escribir el archivo JSON: {ex.Message}");
            }
        }
    }
}