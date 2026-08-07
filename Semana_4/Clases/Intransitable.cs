namespace Proyecto1.Clases
{
    public class Intransitable : Celda
    {
        public Intransitable(int fila, int columna) : base(fila, columna)
        {
        }

        public override void ImprimirDatosCelda()
        {
            Console.WriteLine($"Celda [{Fila},{Columna}] - Tipo: Intransitable");
        }
    }
}