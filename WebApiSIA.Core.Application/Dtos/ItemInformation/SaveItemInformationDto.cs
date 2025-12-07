using System.ComponentModel.DataAnnotations;

namespace WebApiSIA.Core.Application.Dtos.ItemInformation
{
    public class SaveItemInformationDto
    {
        [MaxLength(200, ErrorMessage = "El nombre no puede exceder 200 caracteres")]
        public string? ItemName { get; set; }

        [MaxLength(200, ErrorMessage = "La unidad de medida no puede exceder 200 caracteres")]
        public string? UnitOfMeasure { get; set; }

        [MaxLength(200, ErrorMessage = "El lote no puede exceder 200 caracteres")]
        public string? Batch { get; set; }

        public int? GROUP_ID { get; set; }

        [MaxLength(200, ErrorMessage = "El código de barras no puede exceder 200 caracteres")]
        public string? Barcode { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El costo debe ser mayor o igual a 0")]
        public double? Cost { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser mayor o igual a 0")]
        public double? Price { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio 2 debe ser mayor o igual a 0")]
        public double? Price2 { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio 3 debe ser mayor o igual a 0")]
        public double? Price3 { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El punto de reorden debe ser mayor o igual a 0")]
        public double? ReorderPoint { get; set; }

        [MaxLength(10, ErrorMessage = "VAT Applicable no puede exceder 10 caracteres")]
        public string? VAT_Applicable { get; set; }

        public int? WarehouseID { get; set; }

        public string? PhotoFileName { get; set; }

        [MaxLength(255, ErrorMessage = "El código de barras 2 no puede exceder 255 caracteres")]
        public string? Barcode2 { get; set; }

        [MaxLength(255, ErrorMessage = "El código de barras 3 no puede exceder 255 caracteres")]
        public string? Barcode3 { get; set; }

        public string? Comment { get; set; }

        public int? VAT_ID { get; set; }

        public bool? AllowDecimal { get; set; }

        [Range(0, 999.99, ErrorMessage = "El margen debe estar entre 0 y 999.99")]
        public decimal? Margen { get; set; }
    }
}
