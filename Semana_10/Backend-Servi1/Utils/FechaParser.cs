using System.Globalization;
using System.Text.RegularExpressions;

namespace Backend_Servi1.Utils;

// Extrae una fecha y hora escrita en textos emitidos por el RTU.
public static partial class FechaParser
{
    // Grupo fecha: día/mes/año, hora y minutos; permite el sufijo hrs.
    [GeneratedRegex(@"(?<fecha>\d{2}/\d{2}/\d{4}\s+\d{2}:\d{2})(?:\s*hrs?\.?)?", RegexOptions.IgnoreCase)]
    private static partial Regex PatronFecha();

    public static DateTime? ExtraerFecha(string? texto)
    {
        var coincidencia = PatronFecha().Match(texto ?? string.Empty);
        if (!coincidencia.Success)
            return null;
        return DateTime.TryParseExact(coincidencia.Groups["fecha"].Value, "dd/MM/yyyy HH:mm",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out var fecha) ? fecha : null;
    }
}