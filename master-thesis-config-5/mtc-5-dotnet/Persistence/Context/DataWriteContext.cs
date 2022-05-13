using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class DataWriteContext : IdentityDbContext<User>
    {
        public override DbSet<User> Users { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<OrderedCourse> OrderedCourses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<StatusEntry> StatusEntries { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CoursesCategory> CourseCategories { get; set; }
        public DbSet<ProductsCategory> ProductsCategories { get; set; }
        
        public DbSet<Table> Tables { get; set; }

        public DbSet<Hall> Halls { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DataWriteContext(DbContextOptions<DataWriteContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Bill>()
                .HasIndex(p => p.CustomerId);
            
            builder.Entity<Bill>()
                .HasIndex(p => p.OrderId);
            
            builder.Entity<Course>()
                .HasIndex(p => p.CoursesCategoryId);
            
            builder.Entity<Course>()
                .HasIndex(p => p.Name)
                .IsUnique();
            
            builder.Entity<CoursesCategory>()
                .HasIndex(p => p.Name)
                .IsUnique();

            builder.Entity<Customer>()
                .HasIndex(p => p.NIP)
                .IsUnique();
            
            builder.Entity<Customer>()
                .HasIndex(p => p.Name)
                .IsUnique();

            builder.Entity<Ingredient>()
                .HasIndex(p => p.ProductId);
            
            builder.Entity<Order>()
                .HasIndex(p => p.TableId);
            
            builder.Entity<Order>()
                .HasIndex(p => p.Created);

            builder.Entity<ProductsCategory>()
                .HasIndex(p => p.Name)
                .IsUnique();
            
            builder.Entity<Product>()
                .HasIndex(p => p.ProductsCategoryId);
            
            builder.Entity<OrderedCourse>()
                .HasIndex(p => p.CourseId);
            
            builder.Entity<OrderedCourse>()
                .HasIndex(p => p.OrderId);
            
            builder.Entity<OrderedCourse>()
                .HasIndex(p => p.BillId);
            
            builder.Entity<Reservation>()
                .HasIndex(p => p.TableId);
            
            builder.Entity<StatusEntry>()
                .HasIndex(p => p.StatusId);
            
            builder.Entity<StatusEntry>()
                .HasIndex(p => p.OrderedCourseId);
            
            builder.Entity<StatusEntry>()
                .HasIndex(p => p.OrderId);
            
            builder.Entity<Table>()
                .HasIndex(p => p.HallId);

            builder.Entity<Status>()
                .HasIndex(p => p.Name)
                .IsUnique();
        }
    }
}