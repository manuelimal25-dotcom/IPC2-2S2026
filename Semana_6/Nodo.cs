using System;

namespace ArbolAVL
{
    // Clase que representa un nodo individual del árbol AVL.
    // A diferencia de un nodo de ABB normal, aquí se guarda también la
    // altura del nodo, ya que es necesaria para calcular el factor de
    // equilibrio y decidir si se requiere una rotación.
    public class Nodo
    {
        // Valor almacenado en el nodo.
        public int Valor;

        // Referencia al hijo izquierdo (valores menores).
        public Nodo? Izquierdo;

        // Referencia al hijo derecho (valores mayores).
        public Nodo? Derecho;

        // Altura del nodo dentro del árbol. Un nodo recién creado (hoja)
        // siempre inicia con altura 1.
        public int Altura;

        // Constructor: al crear el nodo, sus hijos son null porque todavía
        // no tiene descendientes, y su altura inicial es 1 (nodo hoja).
        public Nodo(int valor)
        {
            Valor = valor;
            Izquierdo = null;
            Derecho = null;
            Altura = 1;
        }
    }
}