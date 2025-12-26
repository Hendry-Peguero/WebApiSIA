using WebApiSIA.Core.Application.Dtos.ItemInformation;

namespace WebApiSIA.Core.Application.Interfaces.Services
{
    /// <summary>
    /// Interfaz para servicio de escaneo de códigos de barras
    /// </summary>
    public interface IBarcodeService
    {
        /// <summary>
        /// Busca un producto por código de barras en los campos Barcode, Barcode2, Barcode3
        /// </summary>
        /// <param name="barcode">Código de barras a buscar</param>
        /// <returns>Información del producto si existe, null si no se encuentra</returns>
        Task<ItemInformationDto?> ScanBarcodeAsync(string barcode);
    }
}
