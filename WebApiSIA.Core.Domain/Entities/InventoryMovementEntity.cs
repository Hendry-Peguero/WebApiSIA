using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiSIA.Core.Domain.Entities
{
    public class InventoryMovementEntity
    {
        [Key]
        [Column("Movement_ID")]
        public int Movement_ID { get; set; } 

        [Column("ITEM_ID")]
        public int ITEM_ID { get; set; }

        [Required]
        [MaxLength(15)]
        [Column("Movement_Type")]
        public string Movement_Type { get; set; } = null!;

        [Column("Quantity")]
        public double Quantity { get; set; }  

        [Column("Movement_Date")]
        public DateTime Movement_Date { get; set; }

        [Column("Reason")]
        public string? Reason { get; set; }

        [Column("CreatedBy")]
        public int CreatedBy { get; set; }
    }
}