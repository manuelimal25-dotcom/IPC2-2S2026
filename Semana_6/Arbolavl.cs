using System;

namespace ArbolAVL
{
    // Clase que encapsula el comportamiento del árbol AVL: un árbol binario
    // de búsqueda que se reequilibra automáticamente después de cada
    // inserción o eliminación mediante rotaciones.
    public class ArbolAVL
    {
        // Referencia privada a la raíz. Solo se expone mediante la
        // propiedad de solo lectura Raiz, para que otras clases (como el
        // graficador) puedan leerla sin poder reasignarla desde afuera.
        // Es nullable porque un árbol recién creado no tiene raíz todavía.
        private Nodo? raiz;

        public Nodo? Raiz
        {
            get { return raiz; }
        }

        // Constructor: el árbol nace vacío.
        public ArbolAVL()
        {
            raiz = null;
        }

        // Devuelve la altura de un nodo. Se trata aparte el caso null
        // (subárbol vacío) para que su altura se considere 0.
        private int Altura(Nodo? nodo)
        {
            if (nodo == null)
            {
                return 0;
            }

            return nodo.Altura;
        }

        // Calcula el factor de equilibrio de un nodo: altura del subárbol
        // izquierdo menos altura del subárbol derecho.
        private int FactorEquilibrio(Nodo? nodo)
        {
            if (nodo == null)
            {
                return 0;
            }

            return Altura(nodo.Izquierdo) - Altura(nodo.Derecho);
        }

        // Devuelve el mayor entre dos números enteros. Se define de forma
        // manual para no depender de librerías adicionales.
        private int Maximo(int a, int b)
        {
            if (a > b)
            {
                return a;
            }

            return b;
        }

        // Recalcula la altura de un nodo a partir de la altura de sus
        // hijos. Debe llamarse cada vez que la estructura por debajo de
        // este nodo cambia (tras insertar, eliminar o rotar).
        private void ActualizarAltura(Nodo nodo)
        {
            nodo.Altura = 1 + Maximo(Altura(nodo.Izquierdo), Altura(nodo.Derecho));
        }

        // Rotación simple a la derecha: se usa cuando el desequilibrio
        // ocurre por el lado izquierdo del nodo y de su hijo izquierdo.
        // El hijo izquierdo sube a la posición del nodo actual.
        private Nodo RotacionDerecha(Nodo? nodo)
        {
            Nodo nuevaRaiz = nodo.Izquierdo;
            Nodo subArbolTemporal = nuevaRaiz.Derecho;

            // El nodo actual pasa a ser hijo derecho de su antiguo hijo izquierdo.
            nuevaRaiz.Derecho = nodo;

            // El subárbol que colgaba del hijo derecho de la nueva raíz pasa
            // a ser el hijo izquierdo del nodo que bajó de posición.
            nodo.Izquierdo = subArbolTemporal;

            // Se actualizan las alturas empezando por el nodo que quedó más
            // abajo, ya que su altura depende del nodo que quedó más arriba.
            ActualizarAltura(nodo);
            ActualizarAltura(nuevaRaiz);

            return nuevaRaiz;
        }

        // Rotación simple a la izquierda: se usa cuando el desequilibrio
        // ocurre por el lado derecho del nodo y de su hijo derecho.
        // El hijo derecho sube a la posición del nodo actual.
        private Nodo RotacionIzquierda(Nodo nodo)
        {
            Nodo nuevaRaiz = nodo.Derecho;
            Nodo subArbolTemporal = nuevaRaiz.Izquierdo;

            nuevaRaiz.Izquierdo = nodo;
            nodo.Derecho = subArbolTemporal;

            ActualizarAltura(nodo);
            ActualizarAltura(nuevaRaiz);

            return nuevaRaiz;
        }

        // Método público de inserción. Llama al método recursivo privado
        // y actualiza la raíz con el resultado, que ya viene balanceado.
        public void Insertar(int valor)
        {
            raiz = InsertarNodo(raiz, valor);
        }

        private Nodo InsertarNodo(Nodo? nodo, int valor)
        {
            // Paso 1: inserción normal de árbol binario de búsqueda.
            if (nodo == null)
            {
                return new Nodo(valor);
            }

            if (valor < nodo.Valor)
            {
                nodo.Izquierdo = InsertarNodo(nodo.Izquierdo, valor);
            }
            else if (valor > nodo.Valor)
            {
                nodo.Derecho = InsertarNodo(nodo.Derecho, valor);
            }
            else
            {
                // Valor duplicado: no se inserta y se retorna el nodo tal cual.
                return nodo;
            }

            // Paso 2: se actualiza la altura del nodo actual, ya que uno de
            // sus subárboles pudo haber crecido con la inserción.
            ActualizarAltura(nodo);

            // Paso 3: se calcula el factor de equilibrio para saber si este
            // nodo quedó desbalanceado después de la inserción.
            int equilibrio = FactorEquilibrio(nodo);

            // Caso Izquierda-Izquierda: el nodo está cargado a la izquierda
            // y el valor insertado es menor que el hijo izquierdo.
            if (equilibrio > 1 && valor < nodo.Izquierdo.Valor)
            {
                return RotacionDerecha(nodo);
            }

            // Caso Derecha-Derecha: el nodo está cargado a la derecha y el
            // valor insertado es mayor que el hijo derecho.
            if (equilibrio < -1 && valor > nodo.Derecho.Valor)
            {
                return RotacionIzquierda(nodo);
            }

            // Caso Izquierda-Derecha: desequilibrio en zigzag hacia la
            // izquierda. Se corrige el hijo izquierdo primero y luego se
            // rota el nodo actual a la derecha.
            if (equilibrio > 1 && valor > nodo.Izquierdo.Valor)
            {
                nodo.Izquierdo = RotacionIzquierda(nodo.Izquierdo);
                return RotacionDerecha(nodo);
            }

            // Caso Derecha-Izquierda: desequilibrio en zigzag hacia la
            // derecha. Se corrige el hijo derecho primero y luego se rota
            // el nodo actual a la izquierda.
            if (equilibrio < -1 && valor < nodo.Derecho.Valor)
            {
                nodo.Derecho = RotacionDerecha(nodo.Derecho);
                return RotacionIzquierda(nodo);
            }

            // Si no cayó en ningún caso anterior, el nodo sigue balanceado.
            return nodo;
        }

        // Método público de búsqueda. Al igual que en un ABB, aprovecha el
        // orden de los valores para descartar la mitad del árbol en cada paso.
        public bool Buscar(int valor)
        {
            return BuscarNodo(raiz, valor);
        }

        private bool BuscarNodo(Nodo? nodo, int valor)
        {
            if (nodo == null)
            {
                return false;
            }

            if (valor == nodo.Valor)
            {
                return true;
            }

            if (valor < nodo.Valor)
            {
                return BuscarNodo(nodo.Izquierdo, valor);
            }

            return BuscarNodo(nodo.Derecho, valor);
        }

        // Método público de eliminación. Actualiza la raíz con el árbol
        // resultante, ya reequilibrado.
        public void Eliminar(int valor)
        {
            raiz = EliminarNodo(raiz, valor);
        }

        private Nodo? EliminarNodo(Nodo? nodo, int valor)
        {
            // Paso 1: eliminación normal de árbol binario de búsqueda.
            if (nodo == null)
            {
                return null;
            }

            if (valor < nodo.Valor)
            {
                nodo.Izquierdo = EliminarNodo(nodo.Izquierdo, valor);
            }
            else if (valor > nodo.Valor)
            {
                nodo.Derecho = EliminarNodo(nodo.Derecho, valor);
            }
            else
            {
                // Se encontró el nodo a eliminar.
                if (nodo.Izquierdo == null)
                {
                    return nodo.Derecho;
                }

                if (nodo.Derecho == null)
                {
                    return nodo.Izquierdo;
                }

                // Nodo con dos hijos: se reemplaza por su sucesor inorden
                // (el valor más pequeño del subárbol derecho).
                Nodo sucesor = EncontrarMinimo(nodo.Derecho);
                nodo.Valor = sucesor.Valor;
                nodo.Derecho = EliminarNodo(nodo.Derecho, sucesor.Valor);
            }

            // Paso 2: se actualiza la altura del nodo actual, ya que la
            // eliminación pudo haber reducido la altura de algún subárbol.
            ActualizarAltura(nodo);

            // Paso 3: se calcula el factor de equilibrio para saber si este
            // nodo quedó desbalanceado después de la eliminación.
            int equilibrio = FactorEquilibrio(nodo);

            // Caso Izquierda-Izquierda: el hijo izquierdo no está cargado
            // hacia la derecha, por lo que basta una rotación simple.
            if (equilibrio > 1 && FactorEquilibrio(nodo.Izquierdo) >= 0)
            {
                return RotacionDerecha(nodo);
            }

            // Caso Izquierda-Derecha: el hijo izquierdo está cargado hacia
            // la derecha, se necesita una rotación doble.
            if (equilibrio > 1 && FactorEquilibrio(nodo.Izquierdo) < 0)
            {
                nodo.Izquierdo = RotacionIzquierda(nodo.Izquierdo);
                return RotacionDerecha(nodo);
            }

            // Caso Derecha-Derecha: el hijo derecho no está cargado hacia
            // la izquierda, por lo que basta una rotación simple.
            if (equilibrio < -1 && FactorEquilibrio(nodo.Derecho) <= 0)
            {
                return RotacionIzquierda(nodo);
            }

            // Caso Derecha-Izquierda: el hijo derecho está cargado hacia la
            // izquierda, se necesita una rotación doble.
            if (equilibrio < -1 && FactorEquilibrio(nodo.Derecho) > 0)
            {
                nodo.Derecho = RotacionDerecha(nodo.Derecho);
                return RotacionIzquierda(nodo);
            }

            return nodo;
        }

        // Encuentra el nodo con el valor mínimo dentro de un subárbol,
        // recorriendo siempre hacia la izquierda hasta el final.
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
        // Al ser también un árbol binario de búsqueda, devuelve los
        // valores en orden ascendente.
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