// Crea el constructor de la aplicacion web con los argumentos recibidos
var builder = WebApplication.CreateBuilder(args);

// Registra los servicios de controladores y vistas (MVC) en el contenedor de dependencias
builder.Services.AddControllersWithViews();

// Construye la aplicacion con todos los servicios ya configurados
var app = builder.Build();

// Verifica si el entorno actual NO es de desarrollo
if (!app.Environment.IsDevelopment())
{
    // Obliga al navegador a usar siempre HTTPS con este sitio
    app.UseHsts();
}

// Redirige automaticamente las solicitudes HTTP hacia HTTPS
app.UseHttpsRedirection();

// Habilita el sistema de enrutamiento de la aplicacion
app.UseRouting();

// Habilita la verificacion de permisos y autorizacion de los usuarios
app.UseAuthorization();

// Habilita el servicio de archivos estaticos (css, js, imagenes) con cache optimizado
app.MapStaticAssets();

// Define la ruta por defecto: controlador Home, accion Index, id opcional
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets(); // Asocia esta ruta con el manejo de archivos estaticos

// Inicia la aplicacion y la deja escuchando solicitudes
app.Run();