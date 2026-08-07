using Proyecto1.ListaCelda;

namespace Proyecto1.Clases
{
    public class Ciudad
    {
        public string Nombre { get; private set; }
        public int Filas { get; private set; }
        public int Columnas { get; private set; }
        public ListaDobleCelda Malla { get; private set; }

        public Ciudad(string nombre, int filas, int columnas)
        {
            Nombre = nombre;
            Filas = filas;
            Columnas = columnas;
            Malla = new ListaDobleCelda();
        }

        // Agrega una celda a la malla de la ciudad
        public void AgregarCelda(Celda celda)
        {
            Malla.InsertarCelda(celda);
        }

        // Busca una celda de la ciudad por su fila y columna
        public NodoCelda? ObtenerCelda(int fila, int columna)
        {
            return Malla.BuscarCelda(fila, columna);
        }

        public void ImprimirDatosCiudad()
        {
            Console.WriteLine($"Ciudad: {Nombre} - Filas: {Filas} - Columnas: {Columnas}");
        }
    }
}