namespace Frontend_Servi2.Models;

// Conserva el resultado HTTP para mostrarlo de forma didáctica.
public class RespuestaApi
{
    public int Codigo { get; set; }
    public bool Exito { get; set; }
    public string Cuerpo { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;
}