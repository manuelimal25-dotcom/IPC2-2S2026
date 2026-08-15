using System;
using System.IO;
using System.Text;

namespace ArbolBinarioBusqueda
{
    // Clase encargada únicamente de generar la representación gráfica del árbo formato Graphviz (.dot). 
    public class Graficador
    {

        public void GenerarArchivoDot(ArbolBinarioBusqueda arbol, string rutaArchivo)
        {
            // StringBuilder acumula el texto del grafo antes de escribirlo, lo cual es más eficiente que concatenar strings en un ciclo.
            StringBuilder contenido = new StringBuilder();

            // Encabezado del grafo dirigido, junto con estilo general de los nodos.
            contenido.AppendLine("digraph ArbolBinarioBusqueda {");
            contenido.AppendLine("    node [shape=circle, style=filled, fillcolor=lightblue, fontname=\"Arial\"];");

            // Si el árbol está vacío, se genera un grafo sin contenido.
            if (arbol.Raiz == null)
            {
                contenido.AppendLine("    vacio [label=\"Arbol vacio\", shape=plaintext];");
            }
            else
            {
                // Recorremos el árbol para ir agregando cada nodo y cada conexión padre-hijo.
                AgregarNodos(arbol.Raiz, contenido);
            }

            contenido.AppendLine("}");

            // Se escribe el contenido generado en el archivo .dot indicado.
            File.WriteAllText(rutaArchivo, contenido.ToString());
        }

        // Recorre el árbol en preorden y va escribiendo, por cada nodo,
        // sus conexiones hacia hijo izquierdo y derecho en sintaxis Graphviz.
        private void AgregarNodos(Nodo nodo, StringBuilder contenido)
        {
            if (nodo == null)
            {
                return;
            }

            // Si existe hijo izquierdo, se dibuja la línea y se etiqueta como "I".
            if (nodo.Izquierdo != null)
            {
                contenido.AppendLine("    " + nodo.Valor + " -> " + nodo.Izquierdo.Valor + " [label=\"I\"];");
                AgregarNodos(nodo.Izquierdo, contenido);
            }

            // Si existe hijo derecho, se dibuja la línea y se etiqueta como "D".
            if (nodo.Derecho != null)
            {
                contenido.AppendLine("    " + nodo.Valor + " -> " + nodo.Derecho.Valor + " [label=\"D\"];");
                AgregarNodos(nodo.Derecho, contenido);
            }
        }
    }
}