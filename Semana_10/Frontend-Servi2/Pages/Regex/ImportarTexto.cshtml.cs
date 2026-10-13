using System.Text.Json;
using Frontend_Servi2.Models;
using Frontend_Servi2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Frontend_Servi2.Pages.Regex;

public class ImportarTextoModel(ApiClient apiClient) : PageModel
{
    [BindProperty] public string Texto { get; set; } = string.Empty;
    public int Creados { get; private set; }
    public List<string> NitsInvalidos { get; private set; } = [];
    public RespuestaApi Respuesta { get; private set; } = new();

    public async Task OnPostAsync()
    {
        Respuesta = await apiClient.ImportarTextoAsync(Texto);
        if (Respuesta.Exito)
            LeerResultado(Respuesta.Cuerpo);
    }

    private void LeerResultado(string cuerpo)
    {
        using var documento = JsonDocument.Parse(cuerpo);
        Creados = documento.RootElement.GetProperty("creados").GetInt32();
        NitsInvalidos = documento.RootElement.GetProperty("nitsInvalidos")
            .EnumerateArray().Select(elemento => elemento.GetString() ?? string.Empty).ToList();
    }
}
