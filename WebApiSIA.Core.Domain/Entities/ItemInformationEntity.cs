using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiSIA.Core.Domain.Entities
{
    public class ItemInformationEntity
    {
        [Key]
        [Column("ITEM_ID")]
        public int ITEM_ID { get; set; }

        [MaxLength(200)]
        [Column("ItemName")]
        public string? ItemName { get; set; }

        [MaxLength(200)]
        [Column("UnitOfMeasure")]
        public string? UnitOfMeasure { get; set; }

        [MaxLength(200)]
        [Column("Batch")]
        public string? Batch { get; set; }

        [Column("GROUP_ID")]
        public int? GROUP_ID { get; set; }

        [MaxLength(200)]
        [Column("Barcode")]
        public string? Barcode { get; set; }

        [Column("Cost")]
        public double? Cost { get; set; }

        [Column("Price")]
        public double? Price { get; set; }

        [Column("Price2")]
        public double? Price2 { get; set; }

        [Column("Price3")]
        public double? Price3 { get; set; }

        [Column("ReorderPoint")]
        public double? ReorderPoint { get; set; }

        [MaxLength(10)]
        [Column("VAT_Applicable")]
        public string? VAT_Applicable { get; set; }

        [Column("WarehouseID")]
        public int? WarehouseID { get; set; }

        [MaxLength(200)]
        [Column("PhotoFileName")]
        public string? PhotoFileName { get; set; }

        [MaxLength(255)]
        [Column("Barcode2")]
        public string? Barcode2 { get; set; }

        [MaxLength(255)]
        [Column("Barcode3")]
        public string? Barcode3 { get; set; }

        [Column("comment")]
        public string? Comment { get; set; }

        [Column("VAT_ID")]
        public int? VAT_ID { get; set; }

        [Column("AllowDecimal")]
        public bool? AllowDecimal { get; set; }

        [Column("Margen", TypeName = "decimal(5,2)")]
        public decimal? Margen { get; set; }
    }
}
