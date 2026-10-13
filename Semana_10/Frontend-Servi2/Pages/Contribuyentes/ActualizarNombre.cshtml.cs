using Frontend_Servi2.Models;
using Frontend_Servi2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Frontend_Servi2.Pages.Contribuyentes;

public class ActualizarNombreModel(ApiClient apiClient) : PageModel
{
    [BindProperty(SupportsGet = true)] public string Nit { get; set; } = string.Empty;
    [BindProperty] public string Nombre { get; set; } = string.Empty;
    public RespuestaApi Respuesta { get; private set; } = new();

    public async Task OnPostAsync() =>
        Respuesta = await apiClient.ActualizarNombreAsync(Nit, Nombre);
}
