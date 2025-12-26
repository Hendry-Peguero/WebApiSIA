using Microsoft.Extensions.Logging;
using WebApiSIA.Core.Application.Dtos.ItemInformation;
using WebApiSIA.Core.Application.Interfaces.Services;

namespace WebApiSIA.Core.Application.Services
{
    /// <summary>
    /// Servicio para procesamiento de códigos de barras
    /// </summary>
    public class BarcodeService : IBarcodeService
    {
        private readonly IItemInformationService _itemService;
        private readonly ILogger<BarcodeService> _logger;

        public BarcodeService(
            IItemInformationService itemService,
            ILogger<BarcodeService> logger)
        {
            _itemService = itemService;
            _logger = logger;
        }

        /// <summary>
        /// Busca un producto por código de barras en los campos Barcode, Barcode2, Barcode3
        /// </summary>
        public async Task<ItemInformationDto?> ScanBarcodeAsync(string barcode)
        {
            try
            {
                _logger.LogInformation(
                    "Iniciando búsqueda de producto con barcode: {Barcode}",
                    barcode);

                // Usar el servicio existente que ya maneja el mapeo correctamente
                var result = await _itemService.GetByBarcodeAsync(barcode);

                if (result == null)
                {
                    _logger.LogWarning(
                        "Producto no encontrado para barcode: {Barcode}",
                        barcode);
                    return null;
                }

                _logger.LogInformation(
                    "Producto encontrado - ID: {ItemId}, Nombre: {ItemName}",
                    result.ITEM_ID,
                    result.ItemName);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error al buscar producto con barcode: {Barcode}",
                    barcode);
                throw;
            }
        }
    }
}
