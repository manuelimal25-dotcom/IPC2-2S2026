using Semana_7.Models;

namespace Semana_7.Estructuras
{
    public class ArbolAVL
    {
        private NodoAVL? raiz;

        public ArbolAVL()
        {
            raiz = null;
        }

        // Inserta un nuevo libro en el arbol, ordenado por ISBN
        public void Insertar(Libro libro)
        {
            raiz = InsertarNodo(raiz, libro);
        }

        // Inserta el libro de forma recursiva y balancea el arbol en cada nivel
        private NodoAVL InsertarNodo(NodoAVL? nodo, Libro libro)
        {
            // Caso base: se encontro el lugar vacio, aqui se crea el nuevo nodo
            if (nodo == null)
            {
                return new NodoAVL(libro);
            }

            // Si el ISBN es menor, se inserta hacia la izquierda
            if (libro.Isbn < nodo.Dato.Isbn)
            {
                nodo.Izquierdo = InsertarNodo(nodo.Izquierdo, libro);
            }
            // Si el ISBN es mayor, se inserta hacia la derecha
            else if (libro.Isbn > nodo.Dato.Isbn)
            {
                nodo.Derecho = InsertarNodo(nodo.Derecho, libro);
            }
            else
            {
                // El ISBN ya existe, no se permite duplicado
                return nodo;
            }

            // Se actualiza la altura del nodo actual con base en sus hijos
            nodo.Altura = 1 + Math.Max(ObtenerAltura(nodo.Izquierdo), ObtenerAltura(nodo.Derecho));

            // Se balancea el nodo actual antes de retornarlo
            return Balancear(nodo);
        }

        // Devuelve la altura de un nodo, o 0 si el nodo no existe
        private int ObtenerAltura(NodoAVL? nodo)
        {
            return nodo == null ? 0 : nodo.Altura;
        }

        // Calcula el factor de balance: diferencia entre la altura izquierda y la derecha
        private int ObtenerFactorBalance(NodoAVL nodo)
        {
            return ObtenerAltura(nodo.Izquierdo) - ObtenerAltura(nodo.Derecho);
        }

        // Revisa el factor de balance del nodo y aplica la rotacion correspondiente
        private NodoAVL Balancear(NodoAVL nodo)
        {
            int factor = ObtenerFactorBalance(nodo);

            // Caso izquierda-izquierda: el hijo izquierdo esta cargado hacia la izquierda
            if (factor > 1 && ObtenerFactorBalance(nodo.Izquierdo!) >= 0)
            {
                return RotarDerecha(nodo);
            }

            // Caso izquierda-derecha: el hijo izquierdo esta cargado hacia la derecha
            if (factor > 1 && ObtenerFactorBalance(nodo.Izquierdo!) < 0)
            {
                nodo.Izquierdo = RotarIzquierda(nodo.Izquierdo!);
                return RotarDerecha(nodo);
            }

            // Caso derecha-derecha: el hijo derecho esta cargado hacia la derecha
            if (factor < -1 && ObtenerFactorBalance(nodo.Derecho!) <= 0)
            {
                return RotarIzquierda(nodo);
            }

            // Caso derecha-izquierda: el hijo derecho esta cargado hacia la izquierda
            if (factor < -1 && ObtenerFactorBalance(nodo.Derecho!) > 0)
            {
                nodo.Derecho = RotarDerecha(nodo.Derecho!);
                return RotarIzquierda(nodo);
            }

            // El nodo ya esta balanceado, se retorna sin cambios
            return nodo;
        }

        // Rotacion simple hacia la derecha, usada cuando el lado izquierdo pesa mas
        private NodoAVL RotarDerecha(NodoAVL nodo)
        {
            NodoAVL nuevaRaiz = nodo.Izquierdo!;
            NodoAVL? subarbolTemporal = nuevaRaiz.Derecho;

            // Se reacomodan los enlaces para que nuevaRaiz quede arriba de nodo
            nuevaRaiz.Derecho = nodo;
            nodo.Izquierdo = subarbolTemporal;

            // Se recalculan las alturas, primero la del nodo que bajo
            nodo.Altura = 1 + Math.Max(ObtenerAltura(nodo.Izquierdo), ObtenerAltura(nodo.Derecho));
            nuevaRaiz.Altura = 1 + Math.Max(ObtenerAltura(nuevaRaiz.Izquierdo), ObtenerAltura(nuevaRaiz.Derecho));

            return nuevaRaiz;
        }

        // Rotacion simple hacia la izquierda, usada cuando el lado derecho pesa mas
        private NodoAVL RotarIzquierda(NodoAVL nodo)
        {
            NodoAVL nuevaRaiz = nodo.Derecho!;
            NodoAVL? subarbolTemporal = nuevaRaiz.Izquierdo;

            // Se reacomodan los enlaces para que nuevaRaiz quede arriba de nodo
            nuevaRaiz.Izquierdo = nodo;
            nodo.Derecho = subarbolTemporal;

            // Se recalculan las alturas, primero la del nodo que bajo
            nodo.Altura = 1 + Math.Max(ObtenerAltura(nodo.Izquierdo), ObtenerAltura(nodo.Derecho));
            nuevaRaiz.Altura = 1 + Math.Max(ObtenerAltura(nuevaRaiz.Izquierdo), ObtenerAltura(nuevaRaiz.Derecho));

            return nuevaRaiz;
        }
    }
}