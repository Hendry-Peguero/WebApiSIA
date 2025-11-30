using Microsoft.EntityFrameworkCore;
using WebApiSIA.Core.Domain.Entities;

namespace WebApiSIA.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<InventoryMovementEntity> InventoryMovements { get; set; } = null!;
        public DbSet<ItemInformationEntity> ItemInformation { get; set; } = null!;
        public DbSet<UserEntity> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region InventoryMovementEntity
            modelBuilder.Entity<InventoryMovementEntity>(entity =>
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
            #endregion

            #region ItemInformationEntity
            modelBuilder.Entity<ItemInformationEntity>(entity =>
            {
                entity.ToTable("iteminformation");

                entity.HasKey(e => e.ItemId)
                      .HasName("iteminformation_PRIMARY");

                entity.Property(e => e.ItemId)
                      .HasColumnName("ITEM_ID")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.ItemName)
                      .HasColumnName("ItemName")
                      .HasMaxLength(200);

                entity.Property(e => e.UnitOfMeasure)
                      .HasColumnName("UnitOfMeasure")
                      .HasMaxLength(200);

                entity.Property(e => e.Batch)
                      .HasColumnName("Batch")
                      .HasMaxLength(200);

                entity.Property(e => e.GroupId)
                      .HasColumnName("GROUP_ID");

                entity.Property(e => e.Barcode)
                      .HasColumnName("Barcode")
                      .HasMaxLength(200);

                // IMPORTANTE: Cost, Price, Price2, Price3 son float en la BD (double en C#)
                entity.Property(e => e.Cost)
                      .HasColumnName("Cost");

                entity.Property(e => e.Price)
                      .HasColumnName("Price");

                entity.Property(e => e.Price2)
                      .HasColumnName("Price2");

                entity.Property(e => e.Price3)
                      .HasColumnName("Price3");

                entity.Property(e => e.ReorderPoint)
                      .HasColumnName("ReorderPoint");

                entity.Property(e => e.VatApplicable)
                      .HasColumnName("VAT_Applicable")
                      .HasMaxLength(10);

                entity.Property(e => e.WarehouseId)
                      .HasColumnName("WarehouseID");

                entity.Property(e => e.PhotoFileName)
                      .HasColumnName("PhotoFileName")
                      .HasMaxLength(200);

                entity.Property(e => e.Barcode2)
                      .HasColumnName("Barcode2")
                      .HasMaxLength(255);

                entity.Property(e => e.Barcode3)
                      .HasColumnName("Barcode3")
                      .HasMaxLength(255);

                entity.Property(e => e.Comment)
                      .HasColumnName("comment");

                entity.Property(e => e.VatId)
                      .HasColumnName("VAT_ID");

                entity.Property(e => e.AllowDecimal)
                      .HasColumnName("AllowDecimal");

                // Margen sí es decimal en la BD
                entity.Property(e => e.Margen)
                      .HasColumnName("Margen")
                      .HasColumnType("decimal(5,2)");
            });
            #endregion

            #region UserEntity
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(e => e.UserId)
                      .HasName("users_PRIMARY");

                entity.Property(e => e.UserId)
                      .HasColumnName("USER_ID")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.FullName)
                      .HasColumnName("FullName")
                      .HasMaxLength(100);

                entity.Property(e => e.UserName)
                      .HasColumnName("UserName")
                      .HasMaxLength(100);

                entity.Property(e => e.Privilege)
                      .HasColumnName("Privilege")
                      .HasMaxLength(100);

                entity.Property(e => e.RegDate)
                      .HasColumnName("RegDate");

                entity.Property(e => e.Password)
                      .HasColumnName("Password");

                entity.Property(e => e.CanAdd)
                      .HasColumnName("Can_Add")
                      .HasMaxLength(10);

                entity.Property(e => e.CanEdit)
                      .HasColumnName("Can_Edit")
                      .HasMaxLength(10);

                entity.Property(e => e.CanDelete)
                      .HasColumnName("Can_Delete")
                      .HasMaxLength(10);

                entity.Property(e => e.CanPrint)
                      .HasColumnName("Can_Print")
                      .HasMaxLength(10);
            });
            #endregion

        }
    }
}