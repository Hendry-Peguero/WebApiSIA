using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiSIA.Core.Application.Dtos.Barcode;
using WebApiSIA.Core.Application.Dtos.Common;
using WebApiSIA.Core.Application.Interfaces.Services;
using WebApiSIA.Core.Application.Validators;

namespace WebApiSIA.Controllers
{
    /// <summary>
    /// Controlador para operaciones de escaneo de códigos de barras
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requiere JWT para todos los endpoints
    public class BarcodeController : ControllerBase
    {
        private readonly IBarcodeService _barcodeService;
        private readonly ILogger<BarcodeController> _logger;

        public BarcodeController(
            IBarcodeService barcodeService,
            ILogger<BarcodeController> logger)
        {
            _barcodeService = barcodeService;
            _logger = logger;
        }

        /// <summary>
        /// Endpoint para recibir y procesar códigos de barras escaneados
        /// </summary>
        /// <param name="request">Datos del escaneo del código de barras</param>
        /// <returns>Información del producto si existe, error 404 si no</returns>
        [HttpPost("scan")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ScanBarcode([FromBody] BarcodeScanRequestDto request)
        {
            try
            {
                // Validar modelo básico
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning(
                        "Solicitud de escaneo inválida - ModelState: {Errors}",
                        string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                    
                    return BadRequest(new ErrorResponseDto("Invalid barcode format"));
                }

                // Sanitizar el barcode
                var sanitizedBarcode = BarcodeValidator.Sanitize(request.Barcode);

                // Validar y sanitizar el barcode
                if (!BarcodeValidator.Validate(sanitizedBarcode, out string errorMessage))
                {
                    _logger.LogWarning(
                        "Código de barras inválido rechazado: {Barcode} - Razón: {Reason}",
                        request.Barcode,
                        errorMessage);
                    
                    return BadRequest(new ErrorResponseDto(errorMessage));
                }

                _logger.LogInformation(
                    "Procesando escaneo de barcode: {Barcode} - Escaneado en: {ScannedAt}",
                    sanitizedBarcode,
                    request.ScannedAt);

                // Buscar el producto en la base de datos
                var product = await _barcodeService.ScanBarcodeAsync(sanitizedBarcode);

                if (product == null)
                {
                    _logger.LogWarning(
                        "Producto no encontrado para barcode: {Barcode}",
                        sanitizedBarcode);
                    
                    return NotFound(new ErrorResponseDto("Barcode not found"));
                }

                _logger.LogInformation(
                    "Escaneo exitoso - Producto: {ProductName} (ID: {ProductId})",
                    product.ItemName,
                    product.ITEM_ID);

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error interno al procesar barcode: {Barcode}",
                    request.Barcode);

                // No exponer detalles del error en producción
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ErrorResponseDto("Internal server error"));
            }
        }
    }
}
