namespace Backend_Servi1.Utils;

// Valida y calcula el dígito verificador del NIT según módulo 11.
public static class NitValidator
{
    private const int LongitudMinima = 2;
    private const int LongitudMaxima = 21;
    private const char VerificadorEspecial = 'K';

    public static bool EsValido(string? nit)
    {
        if (string.IsNullOrWhiteSpace(nit) || nit.Length is < LongitudMinima or > LongitudMaxima)
            return false;
        var cuerpo = nit[..^1];
        var verificador = char.ToUpperInvariant(nit[^1]);
        // El cuerpo contiene todos los dígitos excepto el verificador final.
        return cuerpo.All(char.IsDigit) && verificador == CalcularVerificador(cuerpo);
    }

    public static char CalcularVerificador(string cuerpo)
    {
        if (string.IsNullOrEmpty(cuerpo) || !cuerpo.All(char.IsDigit))
            throw new ArgumentException("El cuerpo del NIT debe contener solo dígitos.", nameof(cuerpo));
        var suma = 0;
        var posicion = 2;
        // Se multiplica desde la derecha por posiciones consecutivas comenzando en 2.
        for (var indice = cuerpo.Length - 1; indice >= 0; indice--)
            suma += (cuerpo[indice] - '0') * posicion++;
        // El módulo 11 convierte el residuo en un dígito o en K cuando resulta 10.
        var resultado = (11 - suma % 11) % 11;
        return resultado == 10 ? VerificadorEspecial : (char)('0' + resultado);
    }
}