using System;

namespace Semana_2.Modelos
{
    // CLASE: EstacionBase
    public class EstacionBase
    {
        // ATRIBUTOS: Se exponen mediante propiedades.
        // ENCAPSULAMIENTO: set privado, solo la clase puede modificarlos.
        public string Id { get; private set; }
        public string Nombre { get; private set; }
        public bool Activa { get; private set; }

        // MÉTODO: Constructor
        public EstacionBase(string id, string nombre)
        {
            Id = id;
            Nombre = nombre;
            Activa = true;
        }

        // MÉTODOS: Para cambiar el estado de la estación.
        public void Activar()
        {
            Activa = true;
        }

        public void Desactivar()
        {
            Activa = false;
        }

        // MÉTODO: Para mostrar información.
        // "virtual" permite que las clases hijas (Sensor, SensorCultivo, SensorSuelo) extiendan este comportamiento con "override" y "base.MostrarInfo()".
        public virtual void MostrarInfo()
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"Estacion: {Nombre} (ID: {Id})");
            Console.WriteLine($"Estado: {(Activa ? "Activa" : "Inactiva")}");
        }
    }
}