using Frontend_Servi2.Models;
using Frontend_Servi2.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Frontend_Servi2.Pages;

public class InicializarModel(ApiClient apiClient) : PageModel
{
    public RespuestaApi Respuesta { get; private set; } = new();
    public string Verbo => "DELETE";
    public string UrlLlamada => "/api/contribuyentes/inicializar";

    public async Task OnPostAsync()
    {
        Respuesta = await apiClient.InicializarAsync();
    }
}