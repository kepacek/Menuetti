using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Menuetti.Models
{
    public partial class MenuettiDBContext : DbContext
    {
        public MenuettiDBContext()
        {
        }

        public MenuettiDBContext(DbContextOptions<MenuettiDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ingredients> Ingredients { get; set; }
        public virtual DbSet<MyLists> MyLists { get; set; }
        public virtual DbSet<Recipes> Recipes { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("MenuettiDB");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-preview3-35497");

            modelBuilder.Entity<Ingredients>(entity =>
            {
                entity.HasKey(e => e.IngredientId)
                    .HasName("PK__Ingredie__BEAEB27AC776784B");

                entity.Property(e => e.IngredientId).HasColumnName("IngredientID");

                entity.Property(e => e.AmountG).HasColumnName("Amount_g");

                entity.Property(e => e.IngredientName)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.RecipeId).HasColumnName("RecipeID");

                entity.Property(e => e.RecipeUnit)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ingredien__Recip__286302EC");
            });

            modelBuilder.Entity<MyLists>(entity =>
            {
                entity.HasKey(e => e.ListId)
                    .HasName("PK__MyLists__E3832865067A7A15");

                entity.Property(e => e.ListId).HasColumnName("ListID");

                entity.Property(e => e.MenuList)
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.ShoppingList)
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("UserID")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MyLists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MyLists__UserID__2B3F6F97");
            });

            modelBuilder.Entity<Recipes>(entity =>
            {
                entity.HasKey(e => e.RecipeId)
                    .HasName("PK__Recipes__FDD988D05BE87846");

                entity.Property(e => e.RecipeId).HasColumnName("RecipeID");

                entity.Property(e => e.DietType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Instructions)
                    //.IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.RecipeName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Recipes__UserID__25869641");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__1788CCAC9554A7AE");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });
        }
    }
}
