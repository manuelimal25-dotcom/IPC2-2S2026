using System;

namespace Semana_8
{
    // Clase que representa una canción con su información básica.
    public class Cancion
    {
        // Título de la canción, utilizado como criterio de orden en el árbol.
        public string Titulo;

        // Nombre del artista o banda que interpreta la canción.
        public string Artista;

        // Género musical al que pertenece la canción.
        public string Genero;

        // Duración de la canción en minutos.
        public int Duracion;

        // Constructor: inicializa una canción con todos sus datos.
        public Cancion(string titulo, string artista, string genero, int duracion)
        {
            Titulo = titulo;
            Artista = artista;
            Genero = genero;
            Duracion = duracion;
        }
    }
}