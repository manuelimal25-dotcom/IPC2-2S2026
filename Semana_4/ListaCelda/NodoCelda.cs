using Proyecto1.Clases;

namespace Proyecto1.ListaCelda
{
    public class NodoCelda
    {
        public Celda Dato { get; private set; }
        public NodoCelda? Siguiente { get; set; }
        public NodoCelda? Anterior { get; set; }

        public NodoCelda(Celda dato)
        {
            Dato = dato;
            Siguiente = null;
            Anterior = null;
        }
    }
}