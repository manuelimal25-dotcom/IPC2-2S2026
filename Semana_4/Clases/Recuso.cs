namespace Proyecto1.Clases
{
    public class Recurso : Celda
    {
        public Recurso(int fila, int columna) : base(fila, columna)
        {
        }

        public override void ImprimirDatosCelda()
        {
            Console.WriteLine($"Celda [{Fila},{Columna}] - Tipo: Recurso");
        }
    }
}