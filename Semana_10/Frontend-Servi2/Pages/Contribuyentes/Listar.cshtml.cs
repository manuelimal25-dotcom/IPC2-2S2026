using Frontend_Servi2.Models;
using Frontend_Servi2.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Frontend_Servi2.Pages.Contribuyentes;

public class ListarModel(ApiClient apiClient) : PageModel
{
    public List<Contribuyente> Contribuyentes { get; private set; } = [];
    public RespuestaApi Respuesta { get; private set; } = new();

    public async Task OnGetAsync()
    {
        var resultado = await apiClient.ListarAsync();
        Respuesta = resultado.Respuesta;
        Contribuyentes = resultado.Contribuyentes;
    }
}
