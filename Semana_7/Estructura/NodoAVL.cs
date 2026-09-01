using Semana_7.Models;

namespace Semana_7.Estructuras
{
    public class NodoAVL
    {
        public Libro Dato { get; private set; }
        public NodoAVL? Izquierdo { get; set; }
        public NodoAVL? Derecho { get; set; }
        public int Altura { get; set; }

        public NodoAVL(Libro dato)
        {
            Dato = dato;
            Izquierdo = null;
            Derecho = null;
            Altura = 1;
        }
    }
}