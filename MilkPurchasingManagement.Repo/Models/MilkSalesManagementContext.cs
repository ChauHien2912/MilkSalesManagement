using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MilkPurchasingManagement.Repo.Models
{
    public partial class MilkSalesManagementContext : DbContext
    {
        public MilkSalesManagementContext()
        {
        }

        public MilkSalesManagementContext(DbContextOptions<MilkSalesManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
<<<<<<< HEAD
                optionsBuilder.UseSqlServer("Server=LAPTOP-Q339A538\\SQLEXPRESS;User ID=sa;Password=123456;Database=MilkSalesManagement;Trusted_Connection=False;TrustServerCertificate=True;");
=======
                optionsBuilder.UseSqlServer("Server=QUANGHUY\\QHUY;uid=sa;pwd=12345;database=MilkSalesManagement;TrustServerCertificate=True");
>>>>>>> 9687837bcfade4f90830780be544397d7f6840f6
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("price");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ProductId)
<<<<<<< HEAD
                    .HasConstraintName("FK__Cart__productId__440B1D61");
=======
                    .HasConstraintName("FK__Cart__productId__4316F928");
>>>>>>> 9687837bcfade4f90830780be544397d7f6840f6

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
<<<<<<< HEAD
                    .HasConstraintName("FK__Cart__userId__44FF419A");
=======
                    .HasConstraintName("FK__Cart__userId__440B1D61");
>>>>>>> 9687837bcfade4f90830780be544397d7f6840f6
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("createdDate");

                entity.Property(e => e.DeliveryAdress)
                    .HasMaxLength(255)
                    .HasColumnName("deliveryAdress");

                entity.Property(e => e.PaymentId).HasColumnName("paymentId");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .HasColumnName("status");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("totalAmount");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentId)
<<<<<<< HEAD
                    .HasConstraintName("FK__Order__paymentId__45F365D3");
=======
                    .HasConstraintName("FK__Order__paymentId__44FF419A");
>>>>>>> 9687837bcfade4f90830780be544397d7f6840f6

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
<<<<<<< HEAD
                    .HasConstraintName("FK__Order__userId__46E78A0C");
=======
                    .HasConstraintName("FK__Order__userId__45F365D3");
>>>>>>> 9687837bcfade4f90830780be544397d7f6840f6
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("price");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
<<<<<<< HEAD
                    .HasConstraintName("FK__OrderDeta__order__47DBAE45");
=======
                    .HasConstraintName("FK__OrderDeta__order__46E78A0C");
>>>>>>> 9687837bcfade4f90830780be544397d7f6840f6

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
<<<<<<< HEAD
                    .HasConstraintName("FK__OrderDeta__produ__48CFD27E");
=======
                    .HasConstraintName("FK__OrderDeta__produ__47DBAE45");
>>>>>>> 9687837bcfade4f90830780be544397d7f6840f6
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PaymentMethodName).HasMaxLength(255);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AgeAllowed).HasColumnName("ageAllowed");

                entity.Property(e => e.Brand)
                    .HasMaxLength(255)
                    .HasColumnName("brand");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("date")
                    .HasColumnName("expirationDate");

                entity.Property(e => e.ImgUrl)
                    .HasMaxLength(255)
                    .HasColumnName("imgUrl");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Volume)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("volume");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .HasColumnName("content")
                    .IsFixedLength();

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.Rate).HasColumnName("rate");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Review_Product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Review_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Rolename)
                    .HasMaxLength(255)
                    .HasColumnName("rolename");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .HasColumnName("fullName");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("phone");

                entity.Property(e => e.Roleid).HasColumnName("roleid");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Roleid)
<<<<<<< HEAD
                    .HasConstraintName("FK__User__roleid__4BAC3F29");
=======
                    .HasConstraintName("FK__User__roleid__48CFD27E");
>>>>>>> 9687837bcfade4f90830780be544397d7f6840f6
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
