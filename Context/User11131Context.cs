using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using demo_tab_21_11_2024.Models;

namespace demo_tab_21_11_2024.Context;

public partial class User11131Context : DbContext
{
    public User11131Context()
    {
    }

    public User11131Context(DbContextOptions<User11131Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Cathegory> Cathegories { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProduct> OrderProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vidacha> Vidachas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=192.168.7.159:5432;Database=user11131;Username=user11131;Password=user11131");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cathegory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cathegory_pk");

            entity.ToTable("cathegory", "demo2111");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("manufacturer_pk");

            entity.ToTable("manufacturer", "demo2111");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_pk");

            entity.ToTable("order", "demo2111");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.ClientName)
                .HasColumnType("character varying")
                .HasColumnName("client_name");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.DateDeliver).HasColumnName("date_deliver");
            entity.Property(e => e.DateOrdered).HasColumnName("date_ordered");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Vidacha).HasColumnName("vidacha");

            entity.HasOne(d => d.Client).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("order_user_fk");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_status_fk");

            entity.HasOne(d => d.VidachaNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Vidacha)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_vidacha_fk");
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity.HasKey(e => new { e.Order, e.Product }).HasName("order_product_pk");

            entity.ToTable("order_product", "demo2111");

            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.Product).HasColumnName("product");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.OrderNavigation).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.Order)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_product_order_fk");

            entity.HasOne(d => d.ProductNavigation).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.Product)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_product_product_fk");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_pk");

            entity.ToTable("product", "demo2111");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.ActualDiscount).HasColumnName("actual_discount");
            entity.Property(e => e.Articul)
                .HasColumnType("character varying")
                .HasColumnName("articul");
            entity.Property(e => e.Cathegory).HasColumnName("cathegory");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .HasColumnType("character varying")
                .HasColumnName("image");
            entity.Property(e => e.Manufacturer).HasColumnName("manufacturer");
            entity.Property(e => e.MaxDiscount).HasColumnName("max_discount");
            entity.Property(e => e.Measure)
                .HasColumnType("character varying")
                .HasColumnName("measure");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.QuantityStored).HasColumnName("quantity_stored");
            entity.Property(e => e.Supplier).HasColumnName("supplier");

            entity.HasOne(d => d.CathegoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Cathegory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_cathegory_fk");

            entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Manufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_manufacturer_fk");

            entity.HasOne(d => d.SupplierNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Supplier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_supplier_fk");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pk");

            entity.ToTable("role", "demo2111");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("status_pk");

            entity.ToTable("status", "demo2111");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("manufaturer_pk");

            entity.ToTable("supplier", "demo2111");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pk");

            entity.ToTable("user", "demo2111");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Firstname)
                .HasColumnType("character varying")
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasColumnType("character varying")
                .HasColumnName("lastname");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_role_fk");
        });

        modelBuilder.Entity<Vidacha>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vidacha_pk");

            entity.ToTable("vidacha", "demo2111");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
