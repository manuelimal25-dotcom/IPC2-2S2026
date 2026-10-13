using System.Text.RegularExpressions;
using Backend_Servi1.Models;

namespace Backend_Servi1.Utils;

// Busca registros NIT-Nombre dentro de texto libre.
public static partial class TextoExtractor
{
    // Grupos nit y nombre: localizan registros con formato NIT: ... Nombre: ...
    [GeneratedRegex(@"NIT:\s*(?<nit>[0-9]{1,20}[0-9Kk])\s+Nombre:\s*(?<nombre>[^;\r\n]+)", RegexOptions.IgnoreCase)]
    private static partial Regex PatronContribuyente();

    public static List<Contribuyente> ExtraerContribuyentes(string? texto)
    {
        return PatronContribuyente().Matches(texto ?? string.Empty)
            .Select(coincidencia => new Contribuyente
            {
                Nit = coincidencia.Groups["nit"].Value.Trim().ToUpperInvariant(),
                Nombre = coincidencia.Groups["nombre"].Value.Trim()
            }).ToList();
    }
}