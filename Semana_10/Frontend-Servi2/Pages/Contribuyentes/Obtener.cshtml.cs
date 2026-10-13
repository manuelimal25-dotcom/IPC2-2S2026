using System.Text.Json;
using Frontend_Servi2.Models;
using Frontend_Servi2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Frontend_Servi2.Pages.Contribuyentes;

public class ObtenerModel(ApiClient apiClient) : PageModel
{
    [BindProperty] public string Nit { get; set; } = string.Empty;
    public Contribuyente? Contribuyente { get; private set; }
    public RespuestaApi Respuesta { get; private set; } = new();

    public async Task OnPostAsync()
    {
        Respuesta = await apiClient.ObtenerAsync(Nit);
        if (Respuesta.Exito)
            Contribuyente = JsonSerializer.Deserialize<Contribuyente>(Respuesta.Cuerpo);
    }
}
