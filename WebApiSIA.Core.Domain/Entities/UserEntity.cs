using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiSIA.Core.Domain.Entities
{
    public class UserEntity
    {
        [Key]
        [Column("USER_ID")]
        public int USER_ID { get; set; }

        [MaxLength(100)]
        [Column("FullName")]
        public string? FullName { get; set; }

        [MaxLength(100)]
        [Column("UserName")]
        public string? UserName { get; set; }

        [MaxLength(100)]
        [Column("Privilege")]
        public string? Privilege { get; set; }

        [Column("RegDate")]
        public DateTime? RegDate { get; set; }

        [Column("Password")]
        public string? Password { get; set; }

        [MaxLength(10)]
        [Column("Can_Add")]
        public string? CanAdd { get; set; }

        [MaxLength(10)]
        [Column("Can_Edit")]
        public string? CanEdit { get; set; }

        [MaxLength(10)]
        [Column("Can_Delete")]
        public string? CanDelete { get; set; }

        [MaxLength(10)]
        [Column("Can_Print")]
        public string? CanPrint { get; set; }
    }
}
