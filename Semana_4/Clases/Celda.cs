namespace Proyecto1.Clases
{
    public abstract class Celda
    {
        public int Fila { get; private set; }
        public int Columna { get; private set; }

        protected Celda(int fila, int columna)
        {
            Fila = fila;
            Columna = columna;
        }

        public abstract void ImprimirDatosCelda();
    }
}