using Microsoft.AspNetCore.Mvc;
using Semana_7.Estructuras;
using Semana_7.Models;

namespace Semana_7.Controllers
{
    public class LibroController : Controller
    {
        // Campo static para que el arbol viva durante toda la aplicacion, no solo una peticion
        private static ArbolAVL arbol = new ArbolAVL();

        // Responde a peticiones GET hacia /Libro/Registrar
        [HttpGet]
        public IActionResult Registrar()
        {
            // Devuelve la vista vacia, sin ningun dato precargado
            return View();
        }

        // Responde a peticiones POST hacia /Libro/Registrar, cuando se envia el formulario
        [HttpPost]
        public IActionResult Registrar(int isbn, string titulo, string autor, string categoria)
        {
            // Crea el objeto Libro con los datos recibidos del formulario
            Libro libro = new Libro(isbn, titulo, autor, categoria);

            // Inserta el libro en el arbol AVL, balanceandolo automaticamente
            arbol.Insertar(libro);

            // Guarda un mensaje de confirmacion para mostrarlo en la vista
            ViewBag.Mensaje = $"Libro '{titulo}' con ISBN {isbn} registrado correctamente.";

            // Vuelve a mostrar la misma vista, ahora con el mensaje de confirmacion
            return View();
        }
    }
}