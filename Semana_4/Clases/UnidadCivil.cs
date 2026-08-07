namespace Proyecto1.Clases
{
    public class UnidadCivil : Celda
    {
        public UnidadCivil(int fila, int columna) : base(fila, columna)
        {
        }

        public override void ImprimirDatosCelda()
        {
            Console.WriteLine($"Celda [{Fila},{Columna}] - Tipo: Unidad civil");
        }
    }
}