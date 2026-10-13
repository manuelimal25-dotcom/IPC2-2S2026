using System.Text.Json;
using Backend_Servi1.Models;
using Backend_Servi1.Utils;

namespace Backend_Servi1.Services;

// Administra contribuyentes en memoria y los persiste en un archivo JSON.
public class ContribuyenteService
{
    public const string MensajeCuerpoObligatorio = "El cuerpo de la petición es obligatorio.";
    public const string MensajeNitObligatorio = "El NIT es obligatorio.";
    public const string MensajeNitInvalido = "El NIT no es válido.";
    public const string MensajeNombreObligatorio = "El nombre es obligatorio.";
    public const string MensajeYaExiste = "El contribuyente ya existe.";
    public const string MensajeNoExiste = "El contribuyente no existe.";
    private const string CarpetaDatos = "Data";
    private const string ArchivoDatos = "contribuyentes.json";
    private readonly List<Contribuyente> contribuyentes = [];
    private readonly string rutaArchivo;

    public ContribuyenteService()
    {
        rutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), CarpetaDatos, ArchivoDatos);
        CargarDatos();
    }

    public List<Contribuyente> Listar() => contribuyentes.ToList();

    public Contribuyente? Obtener(string nit) => contribuyentes.FirstOrDefault(c => c.Nit == Normalizar(nit));

    public ResultadoOperacion Crear(Contribuyente? contribuyente)
    {
        if (contribuyente is null)
            return Fallo(MensajeCuerpoObligatorio);
        if (string.IsNullOrWhiteSpace(contribuyente.Nit))
            return Fallo(MensajeNitObligatorio);
        contribuyente.Nit = Normalizar(contribuyente.Nit);
        if (!NitValidator.EsValido(contribuyente.Nit))
            return Fallo(MensajeNitInvalido);
        if (string.IsNullOrWhiteSpace(contribuyente.Nombre))
            return Fallo(MensajeNombreObligatorio);
        contribuyente.Nombre = contribuyente.Nombre.Trim();
        if (Obtener(contribuyente.Nit) is not null)
            return Fallo(MensajeYaExiste);
        contribuyentes.Add(contribuyente);
        GuardarDatos();
        return Exito("Contribuyente creado.", contribuyente);
    }

    public ResultadoOperacion Reemplazar(string nit, Contribuyente? contribuyente)
    {
        if (contribuyente is null)
            return Fallo(MensajeCuerpoObligatorio);
        var nitNormalizado = Normalizar(nit);
        if (string.IsNullOrWhiteSpace(contribuyente.Nombre))
            return Fallo(MensajeNombreObligatorio);
        var existente = Obtener(nitNormalizado);
        if (existente is null)
            return Fallo(MensajeNoExiste);
        if (!NitValidator.EsValido(nitNormalizado))
            return Fallo(MensajeNitInvalido);
        existente.Nit = nitNormalizado;
        existente.Nombre = contribuyente.Nombre.Trim();
        GuardarDatos();
        return Exito("Contribuyente reemplazado.", existente);
    }

    public ResultadoOperacion ActualizarNombre(string nit, Contribuyente? contribuyente)
    {
        if (contribuyente is null)
            return Fallo(MensajeCuerpoObligatorio);
        if (string.IsNullOrWhiteSpace(contribuyente.Nombre))
            return Fallo(MensajeNombreObligatorio);
        var nitNormalizado = Normalizar(nit);
        var existente = Obtener(nitNormalizado);
        if (existente is null)
            return Fallo(MensajeNoExiste);
        if (!NitValidator.EsValido(nitNormalizado))
            return Fallo(MensajeNitInvalido);
        existente.Nombre = contribuyente.Nombre.Trim();
        GuardarDatos();
        return Exito("Nombre actualizado.", existente);
    }

    public ResultadoOperacion Eliminar(string nit)
    {
        var nitNormalizado = Normalizar(nit);
        var existente = Obtener(nitNormalizado);
        if (existente is null)
            return Fallo(MensajeNoExiste);
        if (!NitValidator.EsValido(nitNormalizado))
            return Fallo(MensajeNitInvalido);
        contribuyentes.Remove(existente);
        GuardarDatos();
        return Exito("Contribuyente eliminado.", existente);
    }

    public void Inicializar()
    {
        contribuyentes.Clear();
        GuardarDatos();
    }

    public ResultadoImportacion ImportarDesdeTexto(string? texto)
    {
        var resultado = new ResultadoImportacion();
        foreach (var contribuyente in TextoExtractor.ExtraerContribuyentes(texto))
            RegistrarImportado(contribuyente, resultado);
        return resultado;
    }

    private void RegistrarImportado(Contribuyente contribuyente, ResultadoImportacion resultado)
    {
        if (Crear(contribuyente).Exito)
            resultado.Creados++;
        else
            resultado.NitsInvalidos.Add(contribuyente.Nit);
    }

    private void CargarDatos()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(rutaArchivo)!);
        if (!File.Exists(rutaArchivo))
        {
            GuardarDatos();
            return;
        }
        var datos = JsonSerializer.Deserialize<List<Contribuyente>>(File.ReadAllText(rutaArchivo));
        if (datos is not null)
            contribuyentes.AddRange(datos);
    }

    private void GuardarDatos()
    {
        var opciones = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(rutaArchivo, JsonSerializer.Serialize(contribuyentes, opciones));
    }

    private static string Normalizar(string? nit) => (nit ?? string.Empty).Replace(" ", string.Empty).ToUpperInvariant();

    private static ResultadoOperacion Fallo(string mensaje) => new() { Mensaje = mensaje };

    private static ResultadoOperacion Exito(string mensaje, Contribuyente contribuyente) =>
        new() { Exito = true, Mensaje = mensaje, Contribuyente = contribuyente };
}