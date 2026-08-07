namespace Proyecto1.Clases
{
    public class ChapinFighter : Robot
    {
        public int CapacidadCombate { get; private set; }

        public ChapinFighter(string nombre, int capacidadCombate) : base(nombre)
        {
            CapacidadCombate = capacidadCombate;
        }
        public void ReducirCapacidadCombate(int capacidadUnidadMilitar)
        {
            CapacidadCombate -= capacidadUnidadMilitar;
        }
        public override void ImprimirDatosRobot()
        {
            Console.WriteLine($"Robot: {Nombre} - Tipo: ChapinFighter - Capacidad: {CapacidadCombate}");
        }
    }
}