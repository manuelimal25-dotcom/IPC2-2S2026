namespace Proyecto1.Clases
{
    public class Ciudad
    {
        public string Nombre { get; private set; }
        public int Filas { get; private set; }
        public int Columnas { get; private set; }

        public Ciudad(string nombre, int filas, int columnas)
        {
            Nombre = nombre;
            Filas = filas;
            Columnas = columnas;
        }

        public void ImprimirDatosCiudad()
        {
            Console.WriteLine($"Ciudad: {Nombre} - Filas: {Filas} - Columnas: {Columnas}");
        }
    }
}