using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Persistence.Context;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("Waiter"));
                await roleManager.CreateAsync(new IdentityRole("Chef"));
                await roleManager.CreateAsync(new IdentityRole("HallManager"));
                await roleManager.CreateAsync(new IdentityRole("CompanyManager"));
            }


            if(!context.CourseCategories.Any())
            {
                var categories = new List<CoursesCategory> 
                {
                    new CoursesCategory
                    {
                        Id = Guid.Parse("83dddb44-b180-4e29-bb17-3a3a7b10ed64"),
                        Name = "Dania Główne"
                    },
                    new CoursesCategory
                    {
                        Id = Guid.Parse("79302c40-4665-4c7b-98e0-6ee1f9cdde6f"),
                        Name = "Przystawki"
                    },
                    new CoursesCategory
                    {
                        Id = Guid.Parse("5b3bce93-e3c3-48e1-9dfa-4232a6c658b6"),
                        Name = "Zupy"
                    },
                    new CoursesCategory
                    {
                        Id = Guid.Parse("cebbc7f2-b28c-40ae-9892-617ab29483dd"),
                        Name = "Napoje"
                    },
                };

                await context.CourseCategories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            if(!context.Courses.Any())
            {
                var courses = new List<Course>
                {
                    new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "Spaghetti Bolonese",
                        GrossPrice = 19.90,
                        NetPrice = 13.90,
                        Tax = 23,
                        PreparationTimeInMinutes = 5,
                        CoursesCategoryId = Guid.Parse("83dddb44-b180-4e29-bb17-3a3a7b10ed64")
                    },
                    new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "Spaghetti Carbonara",
                        GrossPrice = 21.90,
                        NetPrice = 15.90,
                        Tax = 18,
                        PreparationTimeInMinutes = 10,
                        CoursesCategoryId = Guid.Parse("83dddb44-b180-4e29-bb17-3a3a7b10ed64")
                    },
                    new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "Sok Jabłkowy",
                        GrossPrice = 6.90,
                        NetPrice = 3.40,
                        Tax = 20,
                        PreparationTimeInMinutes = 15,
                        CoursesCategoryId = Guid.Parse("cebbc7f2-b28c-40ae-9892-617ab29483dd")
                    }
                };

                await context.Courses.AddRangeAsync(courses);
                await context.SaveChangesAsync();
            }

            if(!context.ProductsCategories.Any())
            {
                var categories = new List<ProductsCategory> 
                {
                    new ProductsCategory
                    {
                        Id = Guid.Parse("89f40459-b79a-4aa3-a99e-4609a9b89c45"),
                        Name = "Warzywa"
                    },
                    new ProductsCategory
                    {
                        Id = Guid.Parse("a886d570-f18a-47e6-9ac7-a4569fd62fcf"),
                        Name = "Owoce"
                    },
                    new ProductsCategory
                    {
                        Id = Guid.Parse("3cb8db0a-d590-47ba-8197-8ccd3ca3d14e"),
                        Name = "Produkty sypkie"
                    },
                    new ProductsCategory
                    {
                        Id = Guid.Parse("75d9b6c6-e2f5-49b1-818d-468e957d2088"),
                        Name = "Bezglutenowe (bo przecież nikt nie lubi glutenu :( )"
                    },
                };

                await context.ProductsCategories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            if(!context.Products.Any())
            {
                var products = new List<Product> 
                {
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Marchewka",
                        Amount = 20,
                        Unit = "kg",
                        ProductsCategoryId = Guid.Parse("89f40459-b79a-4aa3-a99e-4609a9b89c45")
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Pomidor",
                        Amount = 100,
                        Unit = "kg",
                        ProductsCategoryId = Guid.Parse("89f40459-b79a-4aa3-a99e-4609a9b89c45")
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Makaron Spaghetti",
                        Amount = 30,
                        Unit = "kg",
                        ProductsCategoryId = Guid.Parse("3cb8db0a-d590-47ba-8197-8ccd3ca3d14e")
                    },
                    
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }

            if(!context.Halls.Any())
            {
                var halls = new List<Hall>
                {
                    new Hall
                    {
                        Id = Guid.Parse("b9ff53de-9603-4d36-9ca3-923d836785e6"),
                        ColumnNumber = 20,
                        RowNumber = 20,
                        Description = "Sala Główna",
                        Tables = new List<Table>()
                    },
                    new Hall
                    {
                        Id = Guid.Parse("18efd3d5-1d48-43b8-af96-585cd642ea31"),
                        ColumnNumber = 10,
                        RowNumber = 15,
                        Description = "Sala Bankietowa",
                        Tables = new List<Table>()
                    },
                    new Hall
                    {
                        Id = Guid.Parse("9bf9fd63-0511-420e-9d6b-2e36056d924a"),
                        ColumnNumber = 10,
                        RowNumber = 10,
                        Description = "Patio",
                        Tables = new List<Table>()
                    }
                };

                await context.Halls.AddRangeAsync(halls);
                await context.SaveChangesAsync();
            }

            if(!context.Tables.Any())
            {
                var tables = new List<Table>
                {
                    new Table
                    {
                        Id = Guid.NewGuid(),
                        HallId = Guid.Parse("b9ff53de-9603-4d36-9ca3-923d836785e6"),
                        Orders = new List<Order>(),
                        Reservations = new List<Reservation>(),
                        StartCoordinateX = 0,
                        StartCoordinateY = 0,
                        EndCoordinateX = 2,
                        EndCoordinateY = 2
                    },
                    new Table
                    {
                        Id = Guid.NewGuid(),
                        HallId = Guid.Parse("18efd3d5-1d48-43b8-af96-585cd642ea31"),
                        Orders = new List<Order>(),
                        Reservations = new List<Reservation>(),
                        StartCoordinateX = 4,
                        StartCoordinateY = 4,
                        EndCoordinateX = 8,
                        EndCoordinateY = 8
                    },
                    new Table
                    {
                        Id = Guid.NewGuid(),
                        HallId = Guid.Parse("9bf9fd63-0511-420e-9d6b-2e36056d924a"),
                        Orders = new List<Order>(),
                        Reservations = new List<Reservation>(),
                        StartCoordinateX = 2,
                        StartCoordinateY = 2,
                        EndCoordinateX = 3,
                        EndCoordinateY = 3
                    }
                };
                
                await context.Tables.AddRangeAsync(tables);
                await context.SaveChangesAsync();
            }

            if(!context.Statuses.Any())
            {
                var statuses = new List<Status>
                {
                    new Status
                    {
                        Id = Guid.NewGuid(),
                        Name = "Ordered"
                    },
                    new Status
                    {
                        Id = Guid.NewGuid(),
                        Name = "Pending"
                    },
                    new Status
                    {
                        Id = Guid.NewGuid(),
                        Name = "Ready"
                    },
                    new Status
                    {
                        Id = Guid.NewGuid(),
                        Name = "Delivered"
                    },
                    new Status
                    {
                        Id = Guid.NewGuid(),
                        Name = "Paid"   
                    }
                };

                await context.Statuses.AddRangeAsync(statuses);
                await context.SaveChangesAsync();
            }

        }
    }
}