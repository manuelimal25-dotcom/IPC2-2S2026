using Backend_Servi1.Models;
using Backend_Servi1.Services;
using Backend_Servi1.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Servi1.Controllers;

[ApiController]
[Route("api/contribuyentes")]
public class ContribuyentesController(ContribuyenteService servicio) : ControllerBase
{
    [HttpPost]
    public IActionResult Crear([FromBody] Contribuyente? contribuyente)
    {
        var resultado = servicio.Crear(contribuyente);
        if (resultado.Exito)
            return CreatedAtAction(nameof(Obtener), new { nit = resultado.Contribuyente!.Nit }, resultado.Contribuyente);
        return resultado.Mensaje.Contains("ya existe") ? Conflict(resultado) : BadRequest(resultado);
    }

    [HttpGet]
    public ActionResult<List<Contribuyente>> Listar() => Ok(servicio.Listar());

    [HttpGet("fecha")]
    public IActionResult ExtraerFecha([FromQuery] string? texto)
    {
        var fecha = FechaParser.ExtraerFecha(texto);
        return fecha.HasValue ? Ok(fecha.Value) : BadRequest("No se encontró una fecha válida.");
    }

    [HttpGet("{nit}")]
    public IActionResult Obtener(string nit)
    {
        var contribuyente = servicio.Obtener(nit);
        return contribuyente is null ? NotFound() : Ok(contribuyente);
    }

    [HttpPut("{nit}")]
    public IActionResult Reemplazar(string nit, [FromBody] Contribuyente? contribuyente)
    {
        var resultado = servicio.Reemplazar(nit, contribuyente);
        return resultado.Exito ? Ok(resultado.Contribuyente) : ResponderError(resultado);
    }

    [HttpPatch("{nit}")]
    public IActionResult ActualizarNombre(string nit, [FromBody] Contribuyente? contribuyente)
    {
        var resultado = servicio.ActualizarNombre(nit, contribuyente?.Nombre);
        return resultado.Exito ? Ok(resultado.Contribuyente) : ResponderError(resultado);
    }

    [HttpDelete("inicializar")]
    public IActionResult Inicializar()
    {
        servicio.Inicializar();
        return NoContent();
    }

    [HttpDelete("{nit}")]
    public IActionResult Eliminar(string nit)
    {
        var resultado = servicio.Eliminar(nit);
        return resultado.Exito ? NoContent() : NotFound(resultado);
    }

    [HttpPost("importar-texto")]
    [Consumes("text/plain")]
    public IActionResult ImportarTexto([FromBody] string? texto) => Ok(servicio.ImportarDesdeTexto(texto));

    private IActionResult ResponderError(ResultadoOperacion resultado) =>
        resultado.Mensaje.Contains("no existe") ? NotFound(resultado) : BadRequest(resultado);
}