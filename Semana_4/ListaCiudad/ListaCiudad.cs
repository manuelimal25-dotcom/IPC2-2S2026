using Proyecto1.Clases;
namespace Proyecto1.ListaCiudad
{
    public class ListaCiudad
    {
        private NodoCiudad? cabeza;
        private int tamanio;

        public int Tamanio
        {
            get { return tamanio; }
        }

        public ListaCiudad()
        {
            cabeza = null;
            tamanio = 0;
        }

        // Inserta una nueva ciudad al final de la lista
        public void InsertarCiudad(Ciudad ciudad)
        {
            NodoCiudad nuevoNodo = new NodoCiudad(ciudad);

            // Si la lista esta vacia, el nuevo nodo se convierte en la cabeza
            if (cabeza == null)
            {
                cabeza = nuevoNodo;
                tamanio++;
                return;
            }

            // Si la lista no esta vacia, se recorre hasta el final y se agrega el nuevo nodo
            NodoCiudad actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }

            actual.Siguiente = nuevoNodo;
            tamanio++;
        }

        // Elimina una ciudad de la lista por su nombre
        public void EliminarCiudad(string nombre)
        {
            if (cabeza == null)
            {
                Console.WriteLine("La lista esta vacia. No se puede eliminar ninguna ciudad.");
                return;
            }

            // Si la ciudad a eliminar es la cabeza de la lista
            if (cabeza.Dato.Nombre == nombre)
            {
                cabeza = cabeza.Siguiente;
                tamanio--;
                Console.WriteLine($"Ciudad '{nombre}' eliminada de la lista.");
                return;
            }

            // Recorrer la lista para encontrar la ciudad a eliminar
            NodoCiudad? actual = cabeza;
            while (actual?.Siguiente != null)
            {
                if (actual.Siguiente.Dato.Nombre == nombre)
                {
                    // Se actualiza el enlace del nodo actual para saltar el nodo a eliminar
                    actual.Siguiente = actual.Siguiente.Siguiente;
                    tamanio--;
                    Console.WriteLine($"Ciudad '{nombre}' eliminada de la lista.");
                    return;
                }

                actual = actual.Siguiente;
            }

            Console.WriteLine($"Ciudad '{nombre}' no encontrada en la lista.");
        }

        // Recorre la lista e imprime los datos de cada ciudad
        public void RecorrerLista()
        {
            Console.WriteLine("Lista Simplemente Enlazada de Ciudades:");
            Console.WriteLine("---------------------------------------");

            NodoCiudad? actual = cabeza;
            while (actual != null)
            {
                actual.Dato.ImprimirDatosCiudad();
                actual = actual.Siguiente;
            }
        }

        // Busca una ciudad por su nombre y devuelve el nodo que la contiene
        public NodoCiudad? BuscarCiudad(string nombre)
        {
            NodoCiudad? actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.Nombre == nombre)
                {
                    return actual;
                }

                actual = actual.Siguiente;
            }

            return null;
        }

        // Limpia la lista eliminando todos los nodos y reiniciando el tamaño
        public void LimpiarLista()
        {
            cabeza = null;
            tamanio = 0;
            Console.WriteLine("Lista limpiada exitosamente.");
        }
    }
}