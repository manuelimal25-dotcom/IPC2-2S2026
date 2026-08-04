using System.Xml;
using Proyecto1.Clases;

namespace Proyecto1.Xml
{
    public static class EscribirXML
    {
        // Crea y guarda un archivo XML de configuracion con una ciudad y sus robots
        public static void EscribirArchivoXML(string path)
        {
            try
            {
                // Creamos el documento XML vacio
                XmlDocument docXml = new XmlDocument();

                // Creamos la declaracion XML y la agregamos al documento
                XmlDeclaration declaracion = docXml.CreateXmlDeclaration("1.0", "UTF-8", null);
                docXml.AppendChild(declaracion);

                // Creamos el nodo raiz configuracion
                XmlElement configuracion = docXml.CreateElement("configuracion");
                // Agregamos el nodo configuracion como hijo del documento
                docXml.AppendChild(configuracion);

                // Creamos el nodo listaCiudades
                XmlElement listaCiudades = docXml.CreateElement("listaCiudades");
                // Creamos una ciudad de ejemplo
                XmlElement ciudad = CrearCiudadEjemplo(docXml, "CiudadDemo", 10, 10);
                // Agregamos el nodo ciudad como hijo de listaCiudades
                listaCiudades.AppendChild(ciudad);
                // Agregamos listaCiudades como hijo de configuracion
                configuracion.AppendChild(listaCiudades);

                // Creamos el nodo robots
                XmlElement robots = docXml.CreateElement("robots");
                // Creamos un robot de ejemplo tipo ChapinRescue y lo agregamos como hijo de robots
                robots.AppendChild(CrearRobotEjemplo(docXml, "ChapinRescue", "robot01"));
                // Creamos un robot de ejemplo tipo ChapinFighter y lo agregamos como hijo de robots
                robots.AppendChild(CrearRobotEjemplo(docXml, "ChapinFighter", "robot02"));
                // Agregamos robots como hijo de configuracion
                configuracion.AppendChild(robots);

                // Guardamos el documento XML en el archivo especificado
                docXml.Save(path);
                Console.WriteLine($"Archivo XML creado exitosamente en: {path}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al escribir el archivo XML: {ex.Message}");
            }
        }

        // Crea el nodo ciudad con su nombre y dimensiones
        private static XmlElement CrearCiudadEjemplo(XmlDocument doc, string nombreCiudad, int filas, int columnas)
        {
            // Creamos el nodo ciudad
            XmlElement ciudad = doc.CreateElement("ciudad");

            // Creamos el nodo nombre
            XmlElement nombre = doc.CreateElement("nombre");
            // Agregamos los atributos filas y columnas al nodo nombre
            nombre.SetAttribute("filas", filas.ToString());
            nombre.SetAttribute("columnas", columnas.ToString());
            // Asignamos el texto interno del nodo nombre
            nombre.InnerText = nombreCiudad;
            // Agregamos nombre como hijo de ciudad
            ciudad.AppendChild(nombre);

            // Retornamos el nodo ciudad completo
            return ciudad;
        }

        // Crea el nodo robot con su tipo y nombre
        private static XmlElement CrearRobotEjemplo(XmlDocument doc, string tipoRobot, string nombre)
        {
            // Creamos el nodo robot
            XmlElement robot = doc.CreateElement("robot");

            // Creamos el nodo nombre
            XmlElement nombreElem = doc.CreateElement("nombre");
            // Agregamos el atributo tipo al nodo nombre
            nombreElem.SetAttribute("tipo", tipoRobot);
            // Asignamos el texto interno del nodo nombre
            nombreElem.InnerText = nombre;
            // Agregamos nombre como hijo de robot
            robot.AppendChild(nombreElem);

            // Retornamos el nodo robot completo
            return robot;
        }
    }
}