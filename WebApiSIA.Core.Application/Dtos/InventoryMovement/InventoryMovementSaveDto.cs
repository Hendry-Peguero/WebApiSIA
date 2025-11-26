
namespace WebApiSIA.Core.Application.Dtos.InventoryMovement
{
    public class InventoryMovementSaveDto
    {
        public int ItemId { get; set; }
        public string MovementType { get; set; } = null!;
        public double Quantity { get; set; }
        public DateTime MovementDate { get; set; }
        public string? Reason { get; set; }
        public int CreatedBy { get; set; }
    }
}
