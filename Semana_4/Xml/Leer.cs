using System.Xml;
using Proyecto1.Clases;
using Proyecto1.ListaCiudad;

namespace Proyecto1.Xml
{
    public static class LeerXML
    {
        // Lee el archivo XML de configuracion y procesa ciudades y robots
        public static void LeerArchivoXML(string path, ListaCiudad.ListaCiudad listaCiudades)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("El archivo XML no existe.");
                return;
            }

            try
            {
                XmlDocument docXml = new XmlDocument();
                docXml.Load(path);

                // Obtener el nodo raiz "configuracion"
                XmlNode? configuracion = docXml.DocumentElement;
                if (configuracion == null)
                {
                    Console.WriteLine("El archivo XML esta vacio o mal formado.");
                    return;
                }

                // Procesar la lista de ciudades
                XmlNode? listaCiudadesXml = configuracion.SelectSingleNode("listaCiudades");
                if (listaCiudadesXml != null)
                {
                    foreach (XmlNode nodoCiudad in listaCiudadesXml.ChildNodes)
                    {
                        if (nodoCiudad.Name == "ciudad")
                        {
                            ProcesarCiudad(nodoCiudad, listaCiudades);
                        }
                    }
                }

                // Procesar la lista de robots
                XmlNode? robots = configuracion.SelectSingleNode("robots");
                if (robots != null)
                {
                    foreach (XmlNode nodoRobot in robots.ChildNodes)
                    {
                        if (nodoRobot.Name == "robot")
                        {
                            ProcesarRobot(nodoRobot);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo XML: {ex.Message}");
            }
        }

        // Extrae los datos de una ciudad individual, arma su malla de celdas y la agrega a la lista
        private static void ProcesarCiudad(XmlNode nodoCiudad, ListaCiudad.ListaCiudad listaCiudades)
        {
            try
            {
                XmlNode? nodoNombre = nodoCiudad.SelectSingleNode("nombre");

                string nombre = nodoNombre?.InnerText ?? "";
                string filas = nodoNombre?.Attributes?["filas"]?.Value ?? "0";
                string columnas = nodoNombre?.Attributes?["columnas"]?.Value ?? "0";

                Ciudad ciudad = new Ciudad(nombre, int.Parse(filas), int.Parse(columnas));

                // Procesar cada fila y separar sus caracteres para obtener la columna de cada celda
                XmlNodeList? listaFilas = nodoCiudad.SelectNodes("fila");
                if (listaFilas != null)
                {
                    foreach (XmlNode nodoFila in listaFilas)
                    {
                        ProcesarFila(nodoFila, ciudad);
                    }
                }

                // Procesar las unidades militares, que sobrescriben celdas ya creadas como camino
                XmlNodeList? listaUnidadesMilitares = nodoCiudad.SelectNodes("unidadMilitar");
                if (listaUnidadesMilitares != null)
                {
                    foreach (XmlNode nodoUnidadMilitar in listaUnidadesMilitares)
                    {
                        ProcesarUnidadMilitar(nodoUnidadMilitar, ciudad);
                    }
                }

                listaCiudades.InsertarCiudad(ciudad);
                ciudad.ImprimirDatosCiudad();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar ciudad: {ex.Message}");
            }
        }

        // Procesa una fila completa, separando cada caracter para crear su celda correspondiente
        private static void ProcesarFila(XmlNode nodoFila, Ciudad ciudad)
        {
            string numero = nodoFila.Attributes?["numero"]?.Value ?? "0";
            int fila = int.Parse(numero);

            // El contenido viene delimitado por comillas literales, se quitan antes de recorrer los caracteres
            string contenido = nodoFila.InnerText.Trim('"');

            // Se recorre cada caracter de la fila, la posicion dentro del texto es la columna (base 1)
            for (int indice = 0; indice < contenido.Length; indice++)
            {
                char caracter = contenido[indice];
                int columna = indice + 1;

                Celda celda = CrearCeldaDesdeCaracter(caracter, fila, columna);
                ciudad.AgregarCelda(celda);
            }
        }

        // Crea la celda concreta correspondiente segun el caracter leido de la fila
        private static Celda CrearCeldaDesdeCaracter(char caracter, int fila, int columna)
        {
            switch (caracter)
            {
                case '*':
                    return new Intransitable(fila, columna);
                case 'E':
                    return new PuntoEntrada(fila, columna);
                case 'C':
                    return new UnidadCivil(fila, columna);
                case 'R':
                    return new Recurso(fila, columna);
                default:
                    return new Camino(fila, columna);
            }
        }

        // Sustituye la celda de camino existente por una celda de unidad militar con su capacidad de combate
        private static void ProcesarUnidadMilitar(XmlNode nodoUnidadMilitar, Ciudad ciudad)
        {
            string filaTexto = nodoUnidadMilitar.Attributes?["fila"]?.Value ?? "0";
            string columnaTexto = nodoUnidadMilitar.Attributes?["columna"]?.Value ?? "0";
            string capacidadTexto = nodoUnidadMilitar.InnerText;

            int fila = int.Parse(filaTexto);
            int columna = int.Parse(columnaTexto);
            int capacidad = int.Parse(capacidadTexto);

            // Se elimina la celda de camino que ocupaba esa posicion y se agrega la unidad militar
            ciudad.Malla.EliminarCelda(fila, columna);
            ciudad.AgregarCelda(new UnidadMilitar(fila, columna, capacidad));
        }

        // Extrae los datos de un robot individual y crea el objeto correspondiente
        private static void ProcesarRobot(XmlNode nodoRobot)
        {
            try
            {
                XmlNode? nodoNombre = nodoRobot.SelectSingleNode("nombre");

                string nombre = nodoNombre?.InnerText ?? "";
                string tipo = nodoNombre?.Attributes?["tipo"]?.Value ?? "";

                Robot robot;

                if (tipo == "ChapinFighter")
                {
                    string capacidad = nodoNombre?.Attributes?["capacidad"]?.Value ?? "0";
                    robot = new ChapinFighter(nombre, int.Parse(capacidad));
                }
                else
                {
                    robot = new ChapinRescue(nombre);
                }

                robot.ImprimirDatosRobot();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar robot: {ex.Message}");
            }
        }
    }
}