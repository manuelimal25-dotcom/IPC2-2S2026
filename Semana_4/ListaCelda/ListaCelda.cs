using Proyecto1.Clases;

namespace Proyecto1.ListaCelda
{
    public class ListaDobleCelda
    {
        private NodoCelda? cabeza;
        private NodoCelda? cola;
        private int tamanio;

        public int Tamanio
        {
            get { return tamanio; }
        }

        public ListaDobleCelda()
        {
            cabeza = null;
            cola = null;
            tamanio = 0;
        }

        // Inserta una nueva celda al final de la lista
        public void InsertarCelda(Celda celda)
        {
            NodoCelda nuevoNodo = new NodoCelda(celda);

            // Si la lista esta vacia, el nuevo nodo se convierte en cabeza y cola
            if (cabeza == null)
            {
                cabeza = nuevoNodo;
                cola = nuevoNodo;
                tamanio++;
                return;
            }

            // Se enlaza el nuevo nodo despues de la cola actual, en ambos sentidos
            nuevoNodo.Anterior = cola;
            cola!.Siguiente = nuevoNodo;
            cola = nuevoNodo;
            tamanio++;
        }

        // Elimina una celda de la lista segun su fila y columna
        public void EliminarCelda(int fila, int columna)
        {
            NodoCelda? nodo = BuscarCelda(fila, columna);

            if (nodo == null)
            {
                Console.WriteLine($"Celda [{fila},{columna}] no encontrada en la lista.");
                return;
            }

            // Si el nodo tiene un anterior, se enlaza con el siguiente del nodo eliminado
            if (nodo.Anterior != null)
            {
                nodo.Anterior.Siguiente = nodo.Siguiente;
            }
            else
            {
                // El nodo eliminado era la cabeza, se actualiza la cabeza
                cabeza = nodo.Siguiente;
            }

            // Si el nodo tiene un siguiente, se enlaza con el anterior del nodo eliminado
            if (nodo.Siguiente != null)
            {
                nodo.Siguiente.Anterior = nodo.Anterior;
            }
            else
            {
                // El nodo eliminado era la cola, se actualiza la cola
                cola = nodo.Anterior;
            }

            tamanio--;
            Console.WriteLine($"Celda [{fila},{columna}] eliminada de la lista.");
        }

        // Busca una celda por fila y columna, y devuelve el nodo que la contiene
        public NodoCelda? BuscarCelda(int fila, int columna)
        {
            NodoCelda? actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.Fila == fila && actual.Dato.Columna == columna)
                {
                    return actual;
                }

                actual = actual.Siguiente;
            }

            return null;
        }

        // Recorre la lista de inicio a fin e imprime los datos de cada celda
        public void RecorrerLista()
        {
            Console.WriteLine("Lista Doblemente Enlazada de Celdas:");
            Console.WriteLine("---------------------------------------");

            NodoCelda? actual = cabeza;
            while (actual != null)
            {
                actual.Dato.ImprimirDatosCelda();
                actual = actual.Siguiente;
            }
        }

        // Recorre la lista de fin a inicio, aprovechando el enlace Anterior
        public void RecorrerListaInversa()
        {
            Console.WriteLine("Lista Doblemente Enlazada de Celdas (orden inverso):");
            Console.WriteLine("---------------------------------------");

            NodoCelda? actual = cola;
            while (actual != null)
            {
                actual.Dato.ImprimirDatosCelda();
                actual = actual.Anterior;
            }
        }

        // Limpia la lista eliminando todos los nodos y reiniciando el tamaño
        public void LimpiarLista()
        {
            cabeza = null;
            cola = null;
            tamanio = 0;
            Console.WriteLine("Lista limpiada exitosamente.");
        }
    }
}