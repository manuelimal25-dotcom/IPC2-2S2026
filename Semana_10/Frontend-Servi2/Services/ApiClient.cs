using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Frontend_Servi2.Models;

namespace Frontend_Servi2.Services;

// Centraliza las llamadas HTTP que consume el frontend.
public class ApiClient
{
    private const int CodigoSolicitudInvalida = 400;
    private const int CodigoSinConexion = 0;
    private const int CodigoCreado = 201;
    private const string MensajeNombreObligatorio = "El nombre es obligatorio.";
    private const string MensajeNitObligatorio = "El NIT es obligatorio.";
    private const string MensajeDatosInvalidos = "Los datos enviados no son válidos.";
    private const string MensajeConexion = "No se pudo conectar con la API en http://localhost:5100";
    private const string MensajeCreado = "Contribuyente creado correctamente.";
    private const string MensajeOperacion = "Operación realizada correctamente.";
    private const string MensajeError = "Ocurrió un error inesperado.";
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
        TieneNit(nit)
            ? EjecutarAsync(() => cliente.GetAsync($"{RutaContribuyentes}/{Uri.EscapeDataString(NormalizarTexto(nit))}"))
            : Task.FromResult(RespuestaNitObligatorio());

    // POST crea un contribuyente enviando JSON.
    public Task<RespuestaApi> CrearAsync(Contribuyente contribuyente) =>
        EjecutarAsync(() => cliente.PostAsJsonAsync(RutaContribuyentes, PrepararContribuyente(contribuyente)));

    // PUT reemplaza todos los datos del contribuyente.
    public Task<RespuestaApi> ReemplazarAsync(string nit, Contribuyente contribuyente) =>
        TieneNit(nit)
            ? EjecutarAsync(() => cliente.PutAsJsonAsync($"{RutaContribuyentes}/{Uri.EscapeDataString(NormalizarTexto(nit))}", PrepararContribuyente(contribuyente)))
            : Task.FromResult(RespuestaNitObligatorio());

    // PATCH actualiza solo el nombre usando una solicitud JSON parcial.
    public Task<RespuestaApi> ActualizarNombreAsync(string nit, string nombre)
    {
        if (!TieneNit(nit))
            return Task.FromResult(RespuestaNitObligatorio());
        var contenido = JsonContent.Create(new Contribuyente { Nombre = NormalizarTexto(nombre) });
        var solicitud = new HttpRequestMessage(HttpMethod.Patch,
            $"{RutaContribuyentes}/{Uri.EscapeDataString(NormalizarTexto(nit))}") { Content = contenido };
        return EjecutarSolicitudAsync(solicitud);
    }

    // DELETE elimina un contribuyente por su NIT.
    public Task<RespuestaApi> EliminarAsync(string nit) =>
        TieneNit(nit)
            ? EjecutarAsync(() => cliente.DeleteAsync($"{RutaContribuyentes}/{Uri.EscapeDataString(NormalizarTexto(nit))}"))
            : Task.FromResult(RespuestaNitObligatorio());

    // DELETE elimina todos los datos guardados.
    public Task<RespuestaApi> InicializarAsync() =>
        EjecutarAsync(() => cliente.DeleteAsync($"{RutaContribuyentes}/inicializar"));

    // POST envía texto libre para extraer contribuyentes con Regex.
    public Task<RespuestaApi> ImportarTextoAsync(string texto)
    {
        var contenido = new StringContent(NormalizarTexto(texto), Encoding.UTF8, "text/plain");
        return EjecutarAsync(() => cliente.PostAsync($"{RutaContribuyentes}/importar-texto", contenido));
    }

    // GET demuestra la extracción de una fecha mediante Regex en el backend.
    public Task<RespuestaApi> ExtraerFechaAsync(string texto) =>
        EjecutarAsync(() => cliente.GetAsync($"{RutaContribuyentes}/fecha?texto={Uri.EscapeDataString(NormalizarTexto(texto))}"));

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
            return ErrorConexion();
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
            return ErrorConexion();
        }
    }

    private static async Task<RespuestaApi> CrearRespuestaAsync(HttpResponseMessage respuesta)
    {
        var codigo = (int)respuesta.StatusCode;
        var exito = respuesta.IsSuccessStatusCode;
        var cuerpo = await respuesta.Content.ReadAsStringAsync();
        return new RespuestaApi
        {
            Codigo = codigo,
            Exito = exito,
            Cuerpo = cuerpo,
            Mensaje = ObtenerMensaje(codigo, exito, cuerpo)
        };
    }

    private static RespuestaApi ErrorConexion() => new()
    {
        Codigo = CodigoSinConexion,
        Exito = false,
        Cuerpo = MensajeConexion,
        Mensaje = MensajeConexion
    };

    private static string NormalizarTexto(string? texto) => texto ?? string.Empty;

    private static Contribuyente PrepararContribuyente(Contribuyente? contribuyente) => new()
    {
        Nit = NormalizarTexto(contribuyente?.Nit),
        Nombre = NormalizarTexto(contribuyente?.Nombre)
    };

    private static bool TieneNit(string? nit) => !string.IsNullOrWhiteSpace(nit);

    // Evita construir una ruta sin NIT, que podría coincidir con otro endpoint.
    private static RespuestaApi RespuestaNitObligatorio() => CrearRespuestaLocal(MensajeNitObligatorio);

    private static RespuestaApi CrearRespuestaLocal(string mensaje) => new()
    {
        Codigo = CodigoSolicitudInvalida,
        Exito = false,
        Cuerpo = $"{{\"exito\":false,\"mensaje\":\"{mensaje}\",\"contribuyente\":null}}",
        Mensaje = mensaje
    };

    private static string ObtenerMensaje(int codigo, bool exito, string cuerpo)
    {
        var mensaje = LeerMensajeJson(cuerpo);
        if (!string.IsNullOrWhiteSpace(mensaje))
            return mensaje;
        if (!exito)
            return EsJson(cuerpo) ? MensajeDatosInvalidos : MensajeError;
        return codigo == CodigoCreado ? MensajeCreado : MensajeOperacion;
    }

    private static string LeerMensajeJson(string cuerpo)
    {
        try
        {
            using var documento = JsonDocument.Parse(cuerpo);
            return documento.RootElement.ValueKind == JsonValueKind.Object
                && documento.RootElement.TryGetProperty("mensaje", out var mensaje)
                ? mensaje.GetString() ?? string.Empty
                : string.Empty;
        }
        catch (JsonException)
        {
            return string.Empty;
        }
    }

    private static bool EsJson(string cuerpo)
    {
        try
        {
            using var documento = JsonDocument.Parse(cuerpo);
            return documento.RootElement.ValueKind != JsonValueKind.Null
                && documento.RootElement.ValueKind != JsonValueKind.Undefined;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}