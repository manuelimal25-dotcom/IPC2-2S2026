using Backend_Servi1.Services;

const string politicaCors = "PoliticaCors";

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5100");
builder.Services.AddControllers();
builder.Services.AddSingleton<ContribuyenteService>();
builder.Services.AddCors(opciones => opciones.AddPolicy(politicaCors, politica =>
    politica.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();
app.UseCors(politicaCors);
app.MapControllers();
app.Run();
