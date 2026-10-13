using Frontend_Servi2.Models;
using Frontend_Servi2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Frontend_Servi2.Pages;

public class ContribuyentesModel(ApiClient apiClient) : PageModel
{
    private const string Ruta = "/api/contribuyentes";

    public List<Contribuyente> Contribuyentes { get; private set; } = [];
    public RespuestaApi UltimaRespuesta { get; private set; } = new();
    public string VerboUltimaRespuesta { get; private set; } = "GET";
    public string UrlUltimaRespuesta { get; private set; } = Ruta;

    [BindProperty] public string NitCrear { get; set; } = string.Empty;
    [BindProperty] public string NombreCrear { get; set; } = string.Empty;
    [BindProperty] public string NitConsulta { get; set; } = string.Empty;
    [BindProperty] public string NitReemplazar { get; set; } = string.Empty;
    [BindProperty] public string NombreReemplazar { get; set; } = string.Empty;
    [BindProperty] public string NitParche { get; set; } = string.Empty;
    [BindProperty] public string NombreParche { get; set; } = string.Empty;
    [BindProperty] public string NitEliminar { get; set; } = string.Empty;
    [BindProperty] public string TextoFecha { get; set; } = string.Empty;
    [BindProperty] public string TextoImportacion { get; set; } = string.Empty;

    public async Task OnGetAsync() => await CargarListaAsync("GET", Ruta);

    public async Task OnPostCrearAsync()
    {
        var contribuyente = new Contribuyente { Nit = NitCrear, Nombre = NombreCrear };
        await EjecutarAsync("POST", Ruta, () => apiClient.CrearAsync(contribuyente));
    }

    public async Task OnPostObtenerAsync()
    {
        await EjecutarAsync("GET", $"{Ruta}/{NitConsulta}", () => apiClient.ObtenerAsync(NitConsulta));
    }

    public async Task OnPostReemplazarAsync()
    {
        var contribuyente = new Contribuyente { Nit = NitReemplazar, Nombre = NombreReemplazar };
        await EjecutarAsync("PUT", $"{Ruta}/{NitReemplazar}", () => apiClient.ReemplazarAsync(NitReemplazar, contribuyente));
    }

    public async Task OnPostActualizarNombreAsync()
    {
        await EjecutarAsync("PATCH", $"{Ruta}/{NitParche}", () => apiClient.ActualizarNombreAsync(NitParche, NombreParche));
    }

    public async Task OnPostEliminarAsync()
    {
        await EjecutarAsync("DELETE", $"{Ruta}/{NitEliminar}", () => apiClient.EliminarAsync(NitEliminar));
    }

    public async Task OnPostExtraerFechaAsync()
    {
        await EjecutarAsync("GET", $"{Ruta}/fecha?texto={TextoFecha}", () => apiClient.ExtraerFechaAsync(TextoFecha));
    }

    public async Task OnPostImportarTextoAsync()
    {
        await EjecutarAsync("POST", $"{Ruta}/importar-texto", () => apiClient.ImportarTextoAsync(TextoImportacion));
    }

    public async Task OnPostEditarAsync(string nit, string nombre)
    {
        NitReemplazar = nit;
        NombreReemplazar = nombre;
        NitParche = nit;
        NombreParche = nombre;
        UltimaRespuesta = new RespuestaApi { Codigo = 0, Exito = true, Cuerpo = "Formulario preparado para PUT y PATCH." };
        VerboUltimaRespuesta = "FORMULARIO";
        UrlUltimaRespuesta = "Preparación local";
        await CargarListaAsync();
    }

    public async Task OnPostPrepararParcheAsync(string nit, string nombre)
    {
        NitParche = nit;
        NombreParche = nombre;
        UltimaRespuesta = new RespuestaApi { Codigo = 0, Exito = true, Cuerpo = "Formulario PATCH preparado." };
        VerboUltimaRespuesta = "FORMULARIO";
        UrlUltimaRespuesta = "Preparación local";
        await CargarListaAsync();
    }

    private async Task EjecutarAsync(string verbo, string ruta, Func<Task<RespuestaApi>> llamada)
    {
        var respuesta = await llamada();
        RegistrarRespuesta(verbo, ruta, respuesta);
        await CargarListaAsync();
    }

    private async Task CargarListaAsync(string verbo = "GET", string ruta = Ruta)
    {
        var resultado = await apiClient.ListarAsync();
        Contribuyentes = resultado.Contribuyentes;
        if (UltimaRespuesta.Codigo == 0 && !UltimaRespuesta.Exito)
            RegistrarRespuesta(verbo, ruta, resultado.Respuesta);
    }

    private void RegistrarRespuesta(string verbo, string ruta, RespuestaApi respuesta)
    {
        UltimaRespuesta = respuesta;
        VerboUltimaRespuesta = verbo;
        UrlUltimaRespuesta = ruta;
    }
}