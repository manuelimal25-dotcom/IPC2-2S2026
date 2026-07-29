using System;

namespace Semana_2.Modelos
{
    // CLASE: Sensor (abstracta)
    // ABSTRACCIÓN: Define la estructura general de un sensor
    public abstract class Sensor
    {
        // ATRIBUTOS: Expuestos mediante propiedades.
        // ENCAPSULAMIENTO: set privado, solo la clase puede modificarlos.
        public string Id { get; private set; }
        public string Nombre { get; private set; }
        public EstacionBase Estacion { get; private set; }

        // MÉTODO: Constructor
        public Sensor(string id, string nombre, EstacionBase estacion)
        {
            Id = id;
            Nombre = nombre;
            // Validación fail-fast: evita un NullReferenceException más adelante
            Estacion = estacion ?? throw new ArgumentNullException(nameof(estacion));
        }

        // MÉTODOS ABSTRACTOS: Deben ser implementados por las clases hijas.
        // ABSTRACCIÓN: Define comportamientos que cada tipo de sensor debe tener.
        // POLIMORFISMO: Cada clase hija los implementará de forma diferente.
        public abstract void MostrarInfo();
        public abstract void RealizarMedicion();
    }
}