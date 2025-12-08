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
        public DbSet<VatEntity> Vats { get; set; } = null!;
        public DbSet<ItemGroupEntity> ItemGroups { get; set; } = null!;
        public DbSet<WareHouseEntity> WareHouses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region InventoryMovementEntity
            modelBuilder.Entity<InventoryMovementEntity>(entity =>
            {
                entity.ToTable("inventory_movements");

                entity.HasKey(e => e.Movement_ID)
                      .HasName("Movement_ID");

                entity.Property(e => e.Movement_ID)
                      .HasColumnName("Movement_ID")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.ITEM_ID)
                      .HasColumnName("ITEM_ID");

                entity.Property(e => e.Movement_Type)
                      .HasColumnName("Movement_Type")
                      .HasMaxLength(15)
                      .IsRequired();

                entity.Property(e => e.Quantity)
                      .HasColumnName("Quantity");

                entity.Property(e => e.Movement_Date)
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

                entity.HasKey(e => e.ITEM_ID)
                      .HasName("ITEM_ID");

                entity.Property(e => e.ITEM_ID)
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

                entity.Property(e => e.GROUP_ID)
                      .HasColumnName("GROUP_ID");

                entity.Property(e => e.Barcode)
                      .HasColumnName("Barcode")
                      .HasMaxLength(200);

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

                entity.Property(e => e.VAT_Applicable)
                      .HasColumnName("VAT_Applicable")
                      .HasMaxLength(10);

                entity.Property(e => e.WarehouseID)
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

                entity.Property(e => e.VAT_ID)
                      .HasColumnName("VAT_ID");

                entity.Property(e => e.AllowDecimal)
                      .HasColumnName("AllowDecimal");

                entity.Property(e => e.Margen)
                      .HasColumnName("Margen")
                      .HasColumnType("decimal(5,2)");
            });
            #endregion
 
            #region UserEntity
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(e => e.USER_ID)
                      .HasName("USER_ID");

                entity.Property(e => e.USER_ID)
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

                entity.Property(e => e.Can_Add)
                      .HasColumnName("Can_Add")
                      .HasMaxLength(10);

                entity.Property(e => e.Can_Edit)
                      .HasColumnName("Can_Edit")
                      .HasMaxLength(10);

                entity.Property(e => e.Can_Delete)
                      .HasColumnName("Can_Delete")
                      .HasMaxLength(10);

                entity.Property(e => e.Can_Print)
                      .HasColumnName("Can_Print")
                      .HasMaxLength(10);
            });
            #endregion

            #region VatEntity
            modelBuilder.Entity<VatEntity>(entity =>
            {
                entity.ToTable("vat");

                entity.HasKey(e => e.ID)
                    .HasName("ID");
            });
            #endregion

            #region ItemGroup
            modelBuilder.Entity<ItemGroupEntity>(entity =>
            {
                entity.ToTable("itemgroup"); 

                entity.HasKey(e => e.GROUP_ID);

                entity.Property(e => e.GROUP_ID)
                    .HasColumnName("GROUP_ID");

                entity.Property(e => e.GROUP_NAME)
                    .HasColumnName("GROUP_NAME");
            });
            #endregion

            #region WareHouseEntity
            modelBuilder.Entity<WareHouseEntity>(entity =>
            {
                entity.ToTable("warehouse");

                entity.HasKey(e => e.WarehouseID);

                entity.Property(e => e.WarehouseID)
                    .HasColumnName("WarehouseID");

                entity.Property(e => e.WarehouseAddress)
                    .HasColumnName("WarehouseAddress");
            });
            #endregion

        }
    }
}