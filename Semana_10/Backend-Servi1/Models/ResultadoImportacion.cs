namespace Backend_Servi1.Models;

// Resume cuántos contribuyentes se guardaron y qué NIT no pudo importarse.
public class ResultadoImportacion
{
    public int Creados { get; set; }
    public List<string> NitsInvalidos { get; set; } = [];
}