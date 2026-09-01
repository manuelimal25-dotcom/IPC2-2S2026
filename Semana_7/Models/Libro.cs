namespace Semana_7.Models
{
    public class Libro
    {
        public int Isbn { get; private set; }
        public string Titulo { get; private set; }
        public string Autor { get; private set; }
        public string Categoria { get; private set; }

        public Libro(int isbn, string titulo, string autor, string categoria)
        {
            Isbn = isbn;
            Titulo = titulo;
            Autor = autor;
            Categoria = categoria;
        }
    }
}