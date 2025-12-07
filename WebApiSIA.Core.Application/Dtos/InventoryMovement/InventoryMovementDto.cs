namespace WebApiSIA.Core.Application.Dtos.InventoryMovement
{
    public class InventoryMovementDto
    {
        public int Movement_ID { get; set; }
        public int ITEM_ID { get; set; }
        public string Movement_Type { get; set; } = null!;
        public double Quantity { get; set; }
        public DateTime Movement_Date { get; set; }
        public string? Reason { get; set; }
        public int CreatedBy { get; set; }
    }
}
