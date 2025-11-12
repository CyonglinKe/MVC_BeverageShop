using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    /// <summary>
    /// 應用程式資料庫上下文
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 飲品分類資料集
        /// </summary>
        public DbSet<DrinkCategory> DrinkCategories { get; set; }

        /// <summary>
        /// 飲品資料集
        /// </summary>
        public DbSet<Drink> Drinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 設定 DrinkCategory 表格
            modelBuilder.Entity<DrinkCategory>(entity =>
            {
                entity.ToTable("DrinkCategories");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(200);
                entity.Property(e => e.IconClass).HasMaxLength(50);
            });

            // 設定 Drink 表格
            modelBuilder.Entity<Drink>(entity =>
            {
                entity.ToTable("Drinks");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.ImageUrl).HasMaxLength(500);

                // 設定外鍵關係
                entity.HasOne(d => d.Category)
                      .WithMany()
                      .HasForeignKey(d => d.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}


