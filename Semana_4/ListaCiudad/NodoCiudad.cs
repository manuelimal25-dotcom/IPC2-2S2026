namespace Proyecto1.ListaCiudad
{
    public class NodoCiudad
    {
        public Ciudad Dato { get; private set; }
        public NodoCiudad Siguiente { get; set; }

        public NodoCiudad(Ciudad dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }
}