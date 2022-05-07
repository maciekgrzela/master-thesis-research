using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class DataContext : IdentityDbContext<User>
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

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}