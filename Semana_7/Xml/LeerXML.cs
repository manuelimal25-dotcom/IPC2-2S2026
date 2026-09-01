using System.Xml;
using Semana_7.Models;

namespace Semana_7.Estructuras
{
    public static class LeerXML
    {
        // Lee el archivo XML y devuelve un unico texto con todo lo que se encontro
        public static string LeerArchivoXML(string path)
        {
            // Se acumulan los mensajes en un solo string, sin usar ninguna coleccion nativa
            string mensajes = "";

            // Verifica que el archivo exista antes de intentar leerlo
            if (!File.Exists(path))
            {
                return "El archivo XML no existe.";
            }

            try
            {
                // Crea el documento XML vacio y carga el archivo en memoria
                XmlDocument docXml = new XmlDocument();
                docXml.Load(path);

                // Obtiene el nodo raiz config
                XmlNode? config = docXml.DocumentElement;
                if (config == null)
                {
                    return "El archivo XML esta vacio o mal formado.";
                }

                // Procesa la lista de categorias, si el archivo la incluye
                XmlNode? listaCategorias = config.SelectSingleNode("listaCategorias");
                if (listaCategorias != null)
                {
                    foreach (XmlNode nodoCategoria in listaCategorias.ChildNodes)
                    {
                        if (nodoCategoria.Name == "categoria")
                        {
                            mensajes += ProcesarCategoria(nodoCategoria);
                        }
                    }
                }

                // Procesa la lista de libros, si el archivo la incluye
                XmlNode? listaLibros = config.SelectSingleNode("listaLibros");
                if (listaLibros != null)
                {
                    foreach (XmlNode nodoLibro in listaLibros.ChildNodes)
                    {
                        if (nodoLibro.Name == "libro")
                        {
                            mensajes += ProcesarLibro(nodoLibro);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Cualquier error de lectura se agrega al texto, en vez de detener el programa
                mensajes += $"Error al leer el archivo XML: {ex.Message}\n";
            }

            return mensajes;
        }

        // Extrae el nombre y el padre de una categoria, y arma la linea correspondiente
        private static string ProcesarCategoria(XmlNode nodoCategoria)
        {
            string nombre = nodoCategoria.InnerText;
            string padre = nodoCategoria.Attributes?["padre"]?.Value ?? "Ninguno (categoria raiz)";

            return $"Categoria leida: {nombre} - Padre: {padre}\n";
        }

        // Extrae los datos de un libro y arma la linea correspondiente
        private static string ProcesarLibro(XmlNode nodoLibro)
        {
            string isbnTexto = nodoLibro.SelectSingleNode("ISBN")?.InnerText ?? "0";
            string titulo = nodoLibro.SelectSingleNode("titulo")?.InnerText ?? "";
            string autor = nodoLibro.SelectSingleNode("autor")?.InnerText ?? "";
            string categoria = nodoLibro.SelectSingleNode("categoria")?.InnerText ?? "";

            // Se crea el objeto Libro solo para reutilizar el modelo, no se inserta en ninguna estructura
            Libro libro = new Libro(int.Parse(isbnTexto), titulo, autor, categoria);

            return $"Libro leido: ISBN {libro.Isbn} - {libro.Titulo} - {libro.Autor} - Categoria: {libro.Categoria}\n";
        }
    }
}