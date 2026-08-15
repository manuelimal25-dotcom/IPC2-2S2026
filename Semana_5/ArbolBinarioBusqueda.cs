using System;

namespace ArbolBinarioBusqueda
{
    // Clase que encapsula el comportamiento del árbol binario de búsqueda (ABB).
    // El árbol solo expone la raíz de forma controlada para que el resto del
    // programa no manipule los nodos directamente, respetando encapsulamiento.
    public class ArbolBinarioBusqueda
    {
        // Referencia al nodo raíz. Es privada porque el acceso externo debe
        // hacerse siempre a través de los métodos públicos de esta clase.
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
        public void Insertar(int valor)
        {
            raiz = InsertarNodo(raiz, valor);
        }

        // Inserta un valor de forma recursiva respetando la propiedad del ABB:
        // menores a la izquierda, mayores a la derecha.
        private Nodo? InsertarNodo(Nodo? nodo, int valor)
        {
            // Caso base: si llegamos a una posición vacía, aquí se crea el nuevo nodo.
            if (nodo == null)
            {
                return new Nodo(valor);
            }

            // Si el valor es menor, la inserción continúa en el subárbol izquierdo.
            if (valor < nodo.Valor)
            {
                nodo.Izquierdo = InsertarNodo(nodo.Izquierdo, valor);
            }
            // Si el valor es mayor, la inserción continúa en el subárbol derecho.
            else if (valor > nodo.Valor)
            {
                nodo.Derecho = InsertarNodo(nodo.Derecho, valor);
            }
            // Si el valor ya existe, no se inserta duplicado y se retorna el nodo tal cual.

            return nodo;
        }

        // Método público de búsqueda. Retorna true si el valor existe en el árbol.
        public bool Buscar(int valor)
        {
            return BuscarNodo(raiz, valor);
        }

        // Búsqueda recursiva aprovechando el orden del ABB para descartar la mitad del árbol en cada comparación.
        private bool BuscarNodo(Nodo? nodo, int valor)
        {
            // Si el nodo es null, llegamos al final de una rama sin encontrar el valor.
            if (nodo == null)
            {
                return false;
            }

            // Si el valor coincide con el nodo actual, la búsqueda termina con éxito.
            if (valor == nodo.Valor)
            {
                return true;
            }

            // Si el valor buscado es menor, seguimos buscando en el subárbol izquierdo.
            if (valor < nodo.Valor)
            {
                return BuscarNodo(nodo.Izquierdo, valor);
            }

            // En cualquier otro caso, el valor es mayor y seguimos por la derecha.
            return BuscarNodo(nodo.Derecho, valor);
        }

        // Método público de eliminación. Actualiza la raíz con el árbol resultante.
        public void Eliminar(int valor)
        {
            raiz = EliminarNodo(raiz, valor);
        }

        // Elimina un valor manejando los tres casos posibles: nodo hoja, nodo con un solo hijo, y nodo con dos hijos.
        private Nodo? EliminarNodo(Nodo? nodo, int valor)
        {
            // Si el árbol o subárbol está vacío, no hay nada que eliminar.
            if (nodo == null)
            {
                return null;
            }

            // Buscamos el nodo a eliminar igual que en una búsqueda normal.
            // Si el valor es menor, seguimos por la izquierda.
            if (valor < nodo.Valor)
            {
                nodo.Izquierdo = EliminarNodo(nodo.Izquierdo, valor);
            }
            // Si el valor es mayor, seguimos por la derecha.
            else if (valor > nodo.Valor)
            {
                nodo.Derecho = EliminarNodo(nodo.Derecho, valor);
            }
            // Si encontramos el nodo, procedemos a eliminarlo según los casos posibles.
            else
            {
                // Encontramos el nodo a eliminar.

                // Caso 1: no tiene hijo izquierdo, entonces se reemplaza por el derecho
                // (esto también cubre el caso de nodo hoja, donde Derecho es null).
                if (nodo.Izquierdo == null)
                {
                    return nodo.Derecho;
                }

                // Caso 2: no tiene hijo derecho, entonces se reemplaza por el izquierdo.
                if (nodo.Derecho == null)
                {
                    return nodo.Izquierdo;
                }

                // Caso 3: tiene dos hijos. Buscamos el sucesor inorden, que es el valor
                // más pequeño del subárbol derecho, para mantener el orden del ABB.
                Nodo sucesor = EncontrarMinimo(nodo.Derecho);

                // Copiamos el valor del sucesor al nodo actual.
                nodo.Valor = sucesor.Valor;

                // Eliminamos el sucesor de su posición original en el subárbol derecho.
                nodo.Derecho = EliminarNodo(nodo.Derecho, sucesor.Valor);
            }

            return nodo;
        }

        // Encuentra el nodo con el valor mínimo dentro de un subárbol, recorriendo siempre hacia la izquierda hasta el final.
        private Nodo EncontrarMinimo(Nodo nodo)
        {
            while (nodo.Izquierdo != null)
            {
                nodo = nodo.Izquierdo;
            }

            return nodo;
        }

        // Recorrido preorden: raíz, subárbol izquierdo, subárbol derecho.
        public void RecorridoPreorden()
        {
            Console.Write("Preorden: ");
            Preorden(raiz);
            Console.WriteLine();
        }

        private void Preorden(Nodo? nodo)
        {
            if (nodo == null)
            {
                return;
            }

            Console.Write(nodo.Valor + " ");
            Preorden(nodo.Izquierdo);
            Preorden(nodo.Derecho);
        }

        // Recorrido inorden: subárbol izquierdo, raíz, subárbol derecho.
        public void RecorridoInorden()
        {
            Console.Write("Inorden: ");
            Inorden(raiz);
            Console.WriteLine();
        }

        private void Inorden(Nodo? nodo)
        {
            if (nodo == null)
            {
                return;
            }

            Inorden(nodo.Izquierdo);
            Console.Write(nodo.Valor + " ");
            Inorden(nodo.Derecho);
        }

        // Recorrido posorden: subárbol izquierdo, subárbol derecho, raíz.
        public void RecorridoPosorden()
        {
            Console.Write("Posorden: ");
            Posorden(raiz);
            Console.WriteLine();
        }

        private void Posorden(Nodo? nodo)
        {
            if (nodo == null)
            {
                return;
            }

            Posorden(nodo.Izquierdo);
            Posorden(nodo.Derecho);
            Console.Write(nodo.Valor + " ");
        }
    }
}