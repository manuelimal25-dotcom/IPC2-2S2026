using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Frontend_Servi2.Models;

namespace Frontend_Servi2.Services;

// Centraliza las llamadas HTTP que consume el frontend.
public class ApiClient
{
    private const string RutaContribuyentes = "api/contribuyentes";
    private readonly HttpClient cliente;
    private readonly JsonSerializerOptions opcionesJson = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public ApiClient(HttpClient cliente) => this.cliente = cliente;

    // GET obtiene todos los contribuyentes y deserializa el cuerpo JSON.
    public async Task<(RespuestaApi Respuesta, List<Contribuyente> Contribuyentes)> ListarAsync()
    {
        var respuesta = await EjecutarAsync(() => cliente.GetAsync(RutaContribuyentes));
        var lista = respuesta.Exito
            ? JsonSerializer.Deserialize<List<Contribuyente>>(respuesta.Cuerpo, opcionesJson) ?? []
            : [];
        return (respuesta, lista);
    }

    // GET obtiene un contribuyente usando su NIT en la ruta.
    public Task<RespuestaApi> ObtenerAsync(string nit) =>
        EjecutarAsync(() => cliente.GetAsync($"{RutaContribuyentes}/{Uri.EscapeDataString(nit)}"));

    // POST crea un contribuyente enviando JSON.
    public Task<RespuestaApi> CrearAsync(Contribuyente contribuyente) =>
        EjecutarAsync(() => cliente.PostAsJsonAsync(RutaContribuyentes, contribuyente));

    // PUT reemplaza todos los datos del contribuyente.
    public Task<RespuestaApi> ReemplazarAsync(string nit, Contribuyente contribuyente) =>
        EjecutarAsync(() => cliente.PutAsJsonAsync($"{RutaContribuyentes}/{Uri.EscapeDataString(nit)}", contribuyente));

    // PATCH actualiza solo el nombre usando una solicitud JSON parcial.
    public Task<RespuestaApi> ActualizarNombreAsync(string nit, string nombre)
    {
        var contenido = JsonContent.Create(new Contribuyente { Nombre = nombre });
        var solicitud = new HttpRequestMessage(HttpMethod.Patch,
            $"{RutaContribuyentes}/{Uri.EscapeDataString(nit)}") { Content = contenido };
        return EjecutarSolicitudAsync(solicitud);
    }

    // DELETE elimina un contribuyente por su NIT.
    public Task<RespuestaApi> EliminarAsync(string nit) =>
        EjecutarAsync(() => cliente.DeleteAsync($"{RutaContribuyentes}/{Uri.EscapeDataString(nit)}"));

    // DELETE elimina todos los datos guardados.
    public Task<RespuestaApi> InicializarAsync() =>
        EjecutarAsync(() => cliente.DeleteAsync($"{RutaContribuyentes}/inicializar"));

    // POST envía texto libre para extraer contribuyentes con Regex.
    public Task<RespuestaApi> ImportarTextoAsync(string texto)
    {
        var contenido = new StringContent(texto, Encoding.UTF8, "text/plain");
        return EjecutarAsync(() => cliente.PostAsync($"{RutaContribuyentes}/importar-texto", contenido));
    }

    // GET demuestra la extracción de una fecha mediante Regex en el backend.
    public Task<RespuestaApi> ExtraerFechaAsync(string texto) =>
        EjecutarAsync(() => cliente.GetAsync($"{RutaContribuyentes}/fecha?texto={Uri.EscapeDataString(texto)}"));

    private async Task<RespuestaApi> EjecutarAsync(Func<Task<HttpResponseMessage>> solicitud)
    {
        try
        {
            using var respuesta = await solicitud();
            return await CrearRespuestaAsync(respuesta);
        }
        catch (HttpRequestException)
        {
            return ErrorConexion();
        }
        catch (TaskCanceledException)
        {
            return ErrorConexion("La solicitud tardó demasiado y fue cancelada.");
        }
    }

    private async Task<RespuestaApi> EjecutarSolicitudAsync(HttpRequestMessage solicitud)
    {
        try
        {
            using (solicitud)
            {
                using var respuesta = await cliente.SendAsync(solicitud);
                return await CrearRespuestaAsync(respuesta);
            }
        }
        catch (HttpRequestException)
        {
            return ErrorConexion();
        }
        catch (TaskCanceledException)
        {
            return ErrorConexion("La solicitud tardó demasiado y fue cancelada.");
        }
    }

    private static async Task<RespuestaApi> CrearRespuestaAsync(HttpResponseMessage respuesta) =>
        new()
        {
            Codigo = (int)respuesta.StatusCode,
            Exito = respuesta.IsSuccessStatusCode,
            Cuerpo = await respuesta.Content.ReadAsStringAsync()
        };

    private static RespuestaApi ErrorConexion(string mensaje = "No se pudo conectar con la API en http://localhost:5100") =>
        new() { Codigo = 0, Exito = false, Cuerpo = mensaje };
}