using Microsoft.AspNetCore.Mvc;
using Semana_7.Estructuras;

namespace Semana_7.Controllers
{
    public class XMLController : Controller
    {
        // Responde a peticiones GET hacia /XML/CargarXml, muestra el formulario vacio
        [HttpGet]
        public IActionResult CargarXml()
        {
            return View();
        }

        // Responde a peticiones POST hacia /XML/CargarXml, cuando se envia el archivo seleccionado
        [HttpPost]
        public async Task<IActionResult> CargarXml(IFormFile archivo)
        {
            // Valida que realmente se haya seleccionado un archivo
            if (archivo == null || archivo.Length == 0)
            {
                ViewBag.Mensajes = "No se selecciono ningun archivo.";
                return View();
            }

            // Arma una ruta temporal donde guardar el archivo subido
            string path = Path.Combine(Path.GetTempPath(), archivo.FileName);

            // Copia el contenido del archivo subido hacia esa ruta temporal
            using (FileStream flujo = new FileStream(path, FileMode.Create))
            {
                await archivo.CopyToAsync(flujo);
            }

            // Reutiliza la misma logica de lectura que ya teniamos, ahora sobre el archivo subido
            ViewBag.Mensajes = LeerXML.LeerArchivoXML(path);

            return View();
        }
    }
}