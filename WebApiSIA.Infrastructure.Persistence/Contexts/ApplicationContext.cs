using Microsoft.EntityFrameworkCore;
using WebApiSIA.Core.Domain.Entities;

namespace WebApiSIA.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        // Aquí agregas la tabla
        public DbSet<InventoryMovement> InventoryMovements { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // (Opcional, porque ya usamos DataAnnotations)
            modelBuilder.Entity<InventoryMovement>(entity =>
            {
                entity.ToTable("inventory_movements");

                entity.HasKey(e => e.MovementId)
                      .HasName("PK_inventory_movements");

                entity.Property(e => e.MovementId)
                      .HasColumnName("Movement_ID")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.ItemId)
                      .HasColumnName("ITEM_ID");

                entity.Property(e => e.MovementType)
                      .HasColumnName("Movement_Type")
                      .HasMaxLength(15)
                      .IsRequired();

                entity.Property(e => e.Quantity)
                      .HasColumnName("Quantity");

                entity.Property(e => e.MovementDate)
                      .HasColumnName("Movement_Date");

                entity.Property(e => e.Reason)
                      .HasColumnName("Reason");

                entity.Property(e => e.CreatedBy)
                      .HasColumnName("CreatedBy");
            });
        }
    }
}
