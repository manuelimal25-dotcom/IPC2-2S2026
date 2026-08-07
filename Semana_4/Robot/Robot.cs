namespace Proyecto1.Clases
{
    public abstract class Robot
    {
        public string Nombre { get; private set; }

        protected Robot(string nombre)
        {
            Nombre = nombre;
        }

        public abstract void ImprimirDatosRobot();
    }
}