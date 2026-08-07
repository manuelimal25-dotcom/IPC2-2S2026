namespace Proyecto1.Clases
{
    public class UnidadMilitar : Celda
    {
        public int CapacidadCombate { get; private set; }

        public UnidadMilitar(int fila, int columna, int capacidadCombate) : base(fila, columna)
        {
            CapacidadCombate = capacidadCombate;
        }

        // Indica si una capacidad de combate dada es suficiente para superar esta unidad militar
        public bool PuedeSerSuperada(int capacidadAtacante)
        {
            return capacidadAtacante > CapacidadCombate;
        }

        public override void ImprimirDatosCelda()
        {
            Console.WriteLine($"Celda [{Fila},{Columna}] - Tipo: Unidad militar - Capacidad: {CapacidadCombate}");
        }
    }
}