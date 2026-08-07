namespace Proyecto1.Clases
{
    public class PuntoEntrada : Celda
    {
        public PuntoEntrada(int fila, int columna) : base(fila, columna)
        {
        }

        public override void ImprimirDatosCelda()
        {
            Console.WriteLine($"Celda [{Fila},{Columna}] - Tipo: Punto de entrada");
        }
    }
}