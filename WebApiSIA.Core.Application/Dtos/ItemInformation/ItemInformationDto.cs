namespace WebApiSIA.Core.Application.Dtos.ItemInformation
{
    public class ItemInformationDto
    {
        public int ITEM_ID { get; set; }
        public string? ItemName { get; set; }
        public string? UnitOfMeasure { get; set; }
        public string? Batch { get; set; }
        public int? GROUP_ID { get; set; }
        public string? Barcode { get; set; }
        public double? Cost { get; set; }
        public double? Price { get; set; }
        public double? Price2 { get; set; }
        public double? Price3 { get; set; }
        public double? ReorderPoint { get; set; }
        public string? VAT_Applicable { get; set; }
        public int? WarehouseID { get; set; }
        public string? PhotoFileName { get; set; }
        public string? Barcode2 { get; set; }
        public string? Barcode3 { get; set; }
        public string? Comment { get; set; }
        public int? VAT_ID { get; set; }
        public bool? AllowDecimal { get; set; }
        public decimal? Margen { get; set; }
    }
}
