using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiSIA.Core.Domain.Entities
{
    public class InventoryMovementEntity
    {
        [Key]
        [Column("Movement_ID")]
        public int MovementId { get; set; } 

        [Column("ITEM_ID")]
        public int ItemId { get; set; }

        [Required]
        [MaxLength(15)]
        [Column("Movement_Type")]
        public string MovementType { get; set; } = null!;

        [Column("Quantity")]
        public double Quantity { get; set; }  

        [Column("Movement_Date")]
        public DateTime MovementDate { get; set; }

        [Column("Reason")]
        public string? Reason { get; set; }

        [Column("CreatedBy")]
        public int CreatedBy { get; set; }
    }
}