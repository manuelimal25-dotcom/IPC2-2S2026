using Frontend_Servi2.Models;
using Frontend_Servi2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Frontend_Servi2.Pages.Contribuyentes;

public class ReemplazarModel(ApiClient apiClient) : PageModel
{
    [BindProperty(SupportsGet = true)] public string Nit { get; set; } = string.Empty;
    [BindProperty] public string Nombre { get; set; } = string.Empty;
    public RespuestaApi Respuesta { get; private set; } = new();

    public async Task OnPostAsync()
    {
        var contribuyente = new Contribuyente { Nit = Nit, Nombre = Nombre };
        Respuesta = await apiClient.ReemplazarAsync(Nit, contribuyente);
    }
}
