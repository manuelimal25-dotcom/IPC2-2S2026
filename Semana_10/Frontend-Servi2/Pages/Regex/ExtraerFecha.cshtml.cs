using System.Globalization;
using System.Text.Json;
using Frontend_Servi2.Models;
using Frontend_Servi2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Frontend_Servi2.Pages.Regex;

public class ExtraerFechaModel(ApiClient apiClient) : PageModel
{
    [BindProperty] public string Texto { get; set; } = string.Empty;
    public string Fecha { get; private set; } = string.Empty;
    public RespuestaApi Respuesta { get; private set; } = new();

    public async Task OnPostAsync()
    {
        Respuesta = await apiClient.ExtraerFechaAsync(Texto);
        Fecha = Respuesta.Exito ? FormatearFecha(Respuesta.Cuerpo) : string.Empty;
        if (!string.IsNullOrWhiteSpace(Fecha))
            Respuesta.Mensaje = $"Fecha extraída: {Fecha}";
    }

    private static string FormatearFecha(string cuerpo)
    {
        var fecha = JsonSerializer.Deserialize<DateTime>(cuerpo);
        return fecha.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
    }
}
