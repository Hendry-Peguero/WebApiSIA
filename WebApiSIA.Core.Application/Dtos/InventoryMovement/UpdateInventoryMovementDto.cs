using System.ComponentModel.DataAnnotations;

namespace WebApiSIA.Core.Application.Dtos.InventoryMovement
{
    public class UpdateInventoryMovementDto
    {
        [Required(ErrorMessage = "El item es obligatorio")]
        public int ITEM_ID { get; set; }

        [Required(ErrorMessage = "El tipo de entrada es obligatorio")]
        public string Movement_Type { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public double Quantity { get; set; }

        [Required(ErrorMessage = "La fecha de movimiento es obligatoria")]
        public DateTime Movement_Date { get; set; }

        public string? Reason { get; set; }

        [Required(ErrorMessage = "El usuario creador es obligatorio")]
        public int CreatedBy { get; set; }
    }
}
