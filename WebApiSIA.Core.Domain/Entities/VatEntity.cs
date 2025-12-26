using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiSIA.Core.Domain.Entities
{
    public class VatEntity
    {
        public int ID { get; set; }
        public string? Descripcion { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal VAT {  get; set; }
    }
}
