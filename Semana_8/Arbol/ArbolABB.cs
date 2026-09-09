using System;

namespace Semana_8
{
    // Clase que encapsula el comportamiento del árbol binario de búsqueda (ABB) de canciones.
    // El árbol solo expone la raíz de forma controlada para que el resto del programa no manipule los nodos directamente, respetando encapsulamiento.
    public class ArbolBinarioBusqueda
    {
        // Referencia al nodo raíz. Es privada porque el acceso externo debe hacerse siempre a través de los métodos públicos de esta clase.
        private Nodo? raiz;

        public Nodo? Raiz
        {
            get { return raiz; }
        }

        // Constructor: el árbol nace vacío, sin raíz.
        public ArbolBinarioBusqueda()
        {
            raiz = null;
        }

        // Método público de inserción. Llama al método recursivo privado y actualiza la raíz con el resultado.
        public void Insertar(Cancion cancion)
        {
            raiz = InsertarNodo(raiz, cancion);
        }

        // Inserta una canción de forma recursiva respetando la propiedad del ABB: títulos menores a la izquierda, mayores a la derecha.
        private Nodo? InsertarNodo(Nodo? nodo, Cancion cancion)
        {
            // Caso base: si llegamos a una posición vacía, aquí se crea el nuevo nodo.
            if (nodo == null)
            {
                return new Nodo(cancion);
            }

            int comparacion = string.Compare(cancion.Titulo, nodo.Cancion.Titulo, StringComparison.OrdinalIgnoreCase);

            // Si el título es menor, la inserción continúa en el subárbol izquierdo.
            if (comparacion < 0)
            {
                nodo.Izquierdo = InsertarNodo(nodo.Izquierdo, cancion);
            }
            // Si el título es mayor, la inserción continúa en el subárbol derecho.
            else if (comparacion > 0)
            {
                nodo.Derecho = InsertarNodo(nodo.Derecho, cancion);
            }
            // Si el título ya existe, no se inserta duplicado y se retorna el nodo tal cual.

            return nodo;
        }

        // Método público de búsqueda. Retorna la canción si el título existe en el árbol, o null si no se encuentra.
        public Cancion? Buscar(string titulo)
        {
            return BuscarNodo(raiz, titulo);
        }

        // Búsqueda recursiva aprovechando el orden del ABB para descartar la mitad del árbol en cada comparación.
        private Cancion? BuscarNodo(Nodo? nodo, string titulo)
        {
            // Si el nodo es null, llegamos al final de una rama sin encontrar el título.
            if (nodo == null)
            {
                return null;
            }

            int comparacion = string.Compare(titulo, nodo.Cancion.Titulo, StringComparison.OrdinalIgnoreCase);

            // Si el título coincide con el nodo actual, la búsqueda termina con éxito.
            if (comparacion == 0)
            {
                return nodo.Cancion;
            }

            // Si el título buscado es menor, seguimos buscando en el subárbol izquierdo.
            if (comparacion < 0)
            {
                return BuscarNodo(nodo.Izquierdo, titulo);
            }

            // En cualquier otro caso, el título es mayor y seguimos por la derecha.
            return BuscarNodo(nodo.Derecho, titulo);
        }
    }
}