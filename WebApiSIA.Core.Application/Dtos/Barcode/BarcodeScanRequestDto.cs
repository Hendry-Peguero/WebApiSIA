using System.ComponentModel.DataAnnotations;

namespace WebApiSIA.Core.Application.Dtos.Barcode
{
    /// <summary>
    /// DTO para solicitud de escaneo de código de barras
    /// </summary>
    public class BarcodeScanRequestDto
    {
        /// <summary>
        /// Código de barras escaneado
        /// </summary>
        [Required(ErrorMessage = "El código de barras es requerido")]
        [MaxLength(255, ErrorMessage = "El código de barras no puede exceder 255 caracteres")]
        public string Barcode { get; set; } = string.Empty;

        /// <summary>
        /// Fecha y hora del escaneo
        /// </summary>
        [Required(ErrorMessage = "La fecha de escaneo es requerida")]
        public DateTime ScannedAt { get; set; } = DateTime.Now;
    }
}
