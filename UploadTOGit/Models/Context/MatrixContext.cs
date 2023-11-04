using MatrixEC.Models.CoFigrationPerEntity;
using MatrixEC.Models.Entiy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MatrixEC.Models.Context
{
    public class MatrixContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PropertyValue> PropertyValues { get; set; }
        public MatrixContext(DbContextOptions<MatrixContext> options):base(options)
        {   
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
