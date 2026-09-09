namespace Semana_8
{
    // Clase auxiliar utilizada únicamente para deserializar el archivo JSON.
    // Sus propiedades coinciden con los campos del archivo canciones.json.
    public class CancionJson
    {
        public string Titulo { get; set; } = string.Empty;
        public string Artista { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public int Duracion { get; set; }
    }
}