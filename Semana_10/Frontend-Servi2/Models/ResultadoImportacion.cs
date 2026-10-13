namespace Frontend_Servi2.Models;

// Representa el resumen que devuelve la importación de texto.
public class ResultadoImportacion
{
    public int Creados { get; set; }
    public List<string> NitsInvalidos { get; set; } = [];
}