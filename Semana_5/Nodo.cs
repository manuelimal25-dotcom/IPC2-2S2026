using System;

namespace ArbolBinarioBusqueda
{
    // Clase que representa un nodo individual del árbol binario de búsqueda.
    // Cada nodo guarda un valor entero y referencias a sus dos posibles hijos.
    public class Nodo
    {
        // Valor almacenado en el nodo. Es el dato que se compara en cada operación.
        public int Valor;

        // Referencia al hijo izquierdo. Contendrá valores menores que este nodo.
        public Nodo? Izquierdo;

        // Referencia al hijo derecho. Contendrá valores mayores que este nodo.
        public Nodo? Derecho;

        // Constructor: al crear un nodo nuevo, sus hijos siempre empiezan en null
        // porque todavía no tiene descendientes.
        public Nodo(int valor)
        {
            Valor = valor;
            Izquierdo = null;
            Derecho = null;
        }
    }
}