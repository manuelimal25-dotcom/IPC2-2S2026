using System;

namespace Semana_8
{
    // Clase que representa un nodo individual del árbol binario de búsqueda.
    // Cada nodo guarda una canción y referencias a sus dos posibles hijos.
    public class Nodo
    {
        // Canción almacenada en el nodo. Es el dato que se compara en cada operación.
        public Cancion Cancion;

        // Referencia al hijo izquierdo. Contendrá canciones con título menor que este nodo.
        public Nodo? Izquierdo;

        // Referencia al hijo derecho. Contendrá canciones con título mayor que este nodo.
        public Nodo? Derecho;

        // Constructor: al crear un nodo nuevo, sus hijos siempre empiezan en null porque todavía no tiene descendientes.
        public Nodo(Cancion cancion)
        {
            Cancion = cancion;
            Izquierdo = null;
            Derecho = null;
        }
    }
}