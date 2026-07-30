namespace Proyecto1.Clases
{
    public class ChapinRescue : Robot
    {
        public ChapinRescue(string nombre) : base(nombre)
        {
        }

        public override void ImprimirDatosRobot()
        {
            Console.WriteLine($"Robot: {Nombre} - Tipo: ChapinRescue");
        }
    }
}