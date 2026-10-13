namespace Backend_Servi1.Models;

// Comunica al controlador si una operación terminó correctamente.
public class ResultadoOperacion
{
    public bool Exito { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public Contribuyente? Contribuyente { get; set; }
}