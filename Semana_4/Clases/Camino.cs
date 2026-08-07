namespace Proyecto1.Clases
{
    public class Camino : Celda
    {
        public Camino(int fila, int columna) : base(fila, columna)
        {
        }

        public override void ImprimirDatosCelda()
        {
            Console.WriteLine($"Celda [{Fila},{Columna}] - Tipo: Camino");
        }
    }
}