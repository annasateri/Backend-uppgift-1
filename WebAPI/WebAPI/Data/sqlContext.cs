using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebAPI.Entities;

#nullable disable

namespace WebAPI.Data
{
    public partial class sqlContext : DbContext
    {
        public sqlContext()
        {
        }

        public sqlContext(DbContextOptions<sqlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderLine> OrderLines { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAdress> UserAdresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('Received')");

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.HasOne(d => d.UserAdresses)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserAdressesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__UserAdre__571DF1D5");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__UsersId__5535A963");
            });

            modelBuilder.Entity<OrderLine>(entity =>
            {
                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Orders)
                    .WithMany(p => p.OrderLines)
                    .HasForeignKey(d => d.OrdersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderLine__Order__59FA5E80");

                entity.HasOne(d => d.Products)
                    .WithMany(p => p.OrderLines)
                    .HasForeignKey(d => d.ProductsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderLine__Produ__5AEE82B9");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ShortDescription)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.SubCategories)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SubCategoriesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Products__SubCat__52593CB8");
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Categories)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoriesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SubCatego__Categ__4F7CD00D");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Users__A9D105343BF76F01")
                    .IsUnique();

                entity.Property(e => e.Admin).HasDefaultValueSql("((0))");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password).IsRequired();

                entity.HasOne(d => d.UserAdresses)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserAdressesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__UserAdres__4CA06362");
            });

            modelBuilder.Entity<UserAdress>(entity =>
            {
                entity.Property(e => e.Adress)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Zip)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
