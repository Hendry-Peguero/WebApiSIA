using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiSIA.Core.Domain.Entities
{
    [Table("inventory_movements")] // nombre real de la tabla
    public class InventoryMovement
    {
        [Key]
        [Column("Movement_ID")]
        public int MovementId { get; set; }   // IDENTITY(1,1)

        [Column("ITEM_ID")]
        public int ItemId { get; set; }

        [Required]
        [MaxLength(15)]
        [Column("Movement_Type")]
        public string MovementType { get; set; } = null!;

        [Column("Quantity")]
        public double Quantity { get; set; }   // float en SQL → double en C#

        [Column("Movement_Date")]
        public DateTime MovementDate { get; set; }

        [Column("Reason")]
        public string? Reason { get; set; }

        [Column("CreatedBy")]
        public int CreatedBy { get; set; }
    }
}