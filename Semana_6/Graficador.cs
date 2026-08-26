using System;
using System.IO;
using System.Text;

namespace ArbolAVL
{
    // Clase encargada de generar la representación gráfica del árbol AVL en
    // formato Graphviz (.dot). Se mantiene separada del árbol para que cada
    // clase tenga una única responsabilidad: el árbol maneja datos y
    // equilibrio, el graficador solo maneja visualización.
    public class Graficador
    {
        // Genera un archivo .dot a partir del árbol recibido. Cada nodo se
        // etiqueta con su valor y, debajo, su factor de equilibrio actual,
        // igual que se acostumbra mostrar en los diagramas de árboles AVL.
        public void GenerarArchivoDot(ArbolAVL arbol, string rutaArchivo)
        {
            StringBuilder contenido = new StringBuilder();

            contenido.AppendLine("digraph ArbolAVL {");
            contenido.AppendLine("    node [shape=circle, style=filled, fillcolor=lightblue, fontname=\"Arial\"];");

            if (arbol.Raiz == null)
            {
                contenido.AppendLine("    vacio [label=\"Arbol vacio\", shape=plaintext];");
            }
            else
            {
                AgregarNodos(arbol.Raiz, contenido);
            }

            contenido.AppendLine("}");

            File.WriteAllText(rutaArchivo, contenido.ToString());
        }

        // Calcula la altura de un nodo de la misma forma que la clase del
        // árbol, necesaria aquí solo para poder mostrar el factor de
        // equilibrio en la etiqueta de cada nodo.
        private int Altura(Nodo? nodo)
        {
            if (nodo == null)
            {
                return 0;
            }

            return nodo.Altura;
        }

        // Recorre el árbol en preorden, escribiendo por cada nodo su
        // etiqueta (valor y factor de equilibrio) y sus conexiones hacia
        // hijo izquierdo y derecho.
        private void AgregarNodos(Nodo? nodo, StringBuilder contenido)
        {
            if (nodo == null)
            {
                return;
            }

            int factorEquilibrio = Altura(nodo.Izquierdo) - Altura(nodo.Derecho);

            // La etiqueta muestra el valor del nodo y, en una segunda línea,
            // su factor de equilibrio (FE), tal como se representa en el
            // material de la presentación.
            contenido.AppendLine("    " + nodo.Valor + " [label=\"" + nodo.Valor + "\\nFE=" + factorEquilibrio + "\"];");

            if (nodo.Izquierdo != null)
            {
                contenido.AppendLine("    " + nodo.Valor + " -> " + nodo.Izquierdo.Valor + " [label=\"I\"];");
                AgregarNodos(nodo.Izquierdo, contenido);
            }

            if (nodo.Derecho != null)
            {
                contenido.AppendLine("    " + nodo.Valor + " -> " + nodo.Derecho.Valor + " [label=\"D\"];");
                AgregarNodos(nodo.Derecho, contenido);
            }
        }
    }
}