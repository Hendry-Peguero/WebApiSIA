using System.Text.RegularExpressions;

namespace WebApiSIA.Core.Application.Validators
{
    /// <summary>
    /// Validador manual para códigos de barras con sanitización contra XSS/SQL Injection
    /// </summary>
    public static class BarcodeValidator
    {
        private const int MaxBarcodeLength = 255;
        
        // Patrón para validar solo caracteres alfanuméricos, guiones, espacios y guiones bajos
        // Bloquea símbolos peligrosos como <, >, ', ", ;, (, ), etc.
        private static readonly Regex AllowedCharsPattern = new Regex(@"^[a-zA-Z0-9\s\-_]+$", RegexOptions.Compiled);

        /// <summary>
        /// Valida y sanitiza un código de barras
        /// </summary>
        /// <param name="barcode">Código de barras a validar</param>
        /// <param name="errorMessage">Mensaje de error si la validación falla</param>
        /// <returns>True si el barcode es válido, false en caso contrario</returns>
        public static bool Validate(string? barcode, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Validar que no esté vacío o nulo
            if (string.IsNullOrWhiteSpace(barcode))
            {
                errorMessage = "Invalid barcode format";
                return false;
            }

            // Validar longitud máxima
            if (barcode.Length > MaxBarcodeLength)
            {
                errorMessage = "Invalid barcode format";
                return false;
            }

            // Validar solo caracteres permitidos (alfanuméricos + guiones + espacios)
            if (!AllowedCharsPattern.IsMatch(barcode))
            {
                errorMessage = "Invalid barcode format";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Sanitiza un código de barras removiendo espacios excesivos y trim
        /// </summary>
        /// <param name="barcode">Código de barras a sanitizar</param>
        /// <returns>Código de barras sanitizado</returns>
        public static string Sanitize(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
                return string.Empty;

            // Trim y remover espacios múltiples
            return Regex.Replace(barcode.Trim(), @"\s+", " ");
        }
    }
}
