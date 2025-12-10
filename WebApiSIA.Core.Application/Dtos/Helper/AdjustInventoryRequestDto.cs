namespace WebApiSIA.Core.Application.Dtos
{
    public class AdjustInventoryRequestDto
    {
        public int ITEM_ID { get; set; }
        public string Movement_Type { get; set; }  
        public double Quantity { get; set; }
        public int WarehouseID { get; set; }
        public int SHELF_ID { get; set; }
        public int CreatedBy { get; set; }
        public string Reason { get; set; }
    }
}
