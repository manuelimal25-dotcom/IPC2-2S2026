using System.Xml;
using Proyecto1.Clases;

namespace Proyecto1.Xml
{
    public static class LeerXML
    {
        // Lee el archivo XML de configuracion y procesa ciudades y robots
        public static void LeerArchivoXML(string path)
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
                XmlNode? listaCiudades = configuracion.SelectSingleNode("listaCiudades");
                if (listaCiudades != null)
                {
                    Console.WriteLine($"Verificando {listaCiudades.ChildNodes.Count} Ciudades en el Archivo XML.");
                    foreach (XmlNode nodoCiudad in listaCiudades.ChildNodes)
                    {
                        if (nodoCiudad.Name == "ciudad")
                        {
                            ProcesarCiudad(nodoCiudad);
                        }
                    }
                }

                // Procesar la lista de robots
                XmlNode? robots = configuracion.SelectSingleNode("robots");
                if (robots != null)
                {
                    Console.WriteLine($"Verificando {robots.ChildNodes.Count} Robots en el Archivo XML.");
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

        // Extrae los datos de una ciudad individual y crea el objeto Ciudad
        private static void ProcesarCiudad(XmlNode nodoCiudad)
        {
            try
            {
                XmlNode? nodoNombre = nodoCiudad.SelectSingleNode("nombre");

                string nombre = nodoNombre?.InnerText ?? "";
                string filas = nodoNombre?.Attributes?["filas"]?.Value ?? "0";
                string columnas = nodoNombre?.Attributes?["columnas"]?.Value ?? "0";

                XmlNodeList? listaFilas = nodoCiudad.SelectNodes("fila");
                if (listaFilas != null)
                {
                    foreach (XmlNode nodoFila in listaFilas)
                    {
                        string numero = nodoFila.Attributes?["numero"]?.Value ?? "0";
                        string contenido = nodoFila.InnerText;

                        Console.WriteLine($"Fila {numero}: {contenido}");
                    }
                }

                Ciudad ciudad = new Ciudad(nombre, int.Parse(filas), int.Parse(columnas));
                ciudad.ImprimirDatosCiudad();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar ciudad: {ex.Message}");
            }
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