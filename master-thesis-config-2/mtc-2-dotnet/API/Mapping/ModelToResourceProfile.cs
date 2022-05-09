using System.Linq;
using Application;
using Application.Resources.Auth;
using Application.Resources.Bills.Get;
using Application.Resources.Coordinate.List;
using Application.Resources.Course.Get;
using Application.Resources.CourseCategories.Get;
using Application.Resources.Customers.Get;
using Application.Resources.Hall.Get;
using Application.Resources.Ingredients.Get;
using Application.Resources.OrderedCourses.Get;
using Application.Resources.Orders.Get;
using Application.Resources.Products.Get;
using Application.Resources.Products.Save;
using Application.Resources.ProductsCategories.Get;
using Application.Resources.Reservation.Get;
using Application.Resources.StatusEntries.Get;
using Application.Resources.Statuses.Get;
using Application.Resources.Table.Get;
using AutoMapper;
using Domain.Algorithms.Models;
using Domain.Models;

namespace API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Bill, BillResource>()
                .ForMember(p => p.OrderId, opt => opt.MapFrom(p => p.Order.Id))
                .ForMember(p => p.CustomerId, opt => opt.MapFrom(p => p.Customer.Id))
                .ForMember(p => p.OrderedCourses, opt => opt.MapFrom(p => p.OrderedCourses.Select(p => p.Id).ToList()));
            CreateMap<Course, CourseResource>()
                .ForMember(p => p.CoursesCategoryId, opt => opt.MapFrom(p => p.CoursesCategory.Id))
                .ForMember(p => p.CoursesCategoryName, opt => opt.MapFrom(p => p.CoursesCategory.Name));
            CreateMap<CoursesCategory, CoursesCategoryResource>();
            CreateMap<Customer, Application.Resources.Customers.Get.CustomerResource>();
            CreateMap<Customer, Application.Resources.Bills.Get.CustomerResource>();
            CreateMap<Ingredient, IngredientResource>()
                .ForMember(p => p.Unit, opt => opt.MapFrom(p => p.Product.Unit));
            CreateMap<OrderedCourse, OrderedCourseResource>()
                .ForMember(p => p.OrderId, opt => opt.MapFrom(p => p.Order.Id))
                .ForMember(p => p.BillId, opt => opt.MapFrom(p => p.Bill.Id));
            CreateMap<Order, Application.Resources.Orders.Get.OrderResource>()
                .ForMember(p => p.UserId, opt => opt.MapFrom(p => p.User.Id))
                .ForMember(p => p.OrderedCourses, opt => opt.MapFrom(p => p.OrderedCourses));
            CreateMap<Order, Application.Resources.Bills.Get.OrderResource>()
                .ForMember(p => p.UserId, opt => opt.MapFrom(p => p.User.Id));

            CreateMap<Product, ProductResource>()
                .ForMember(p => p.ProductsCategoryId, opt => opt.MapFrom(p => p.ProductsCategory.Id))
                .ForMember(p => p.ProductsCategoryName, opt => opt.MapFrom(p => p.ProductsCategory.Name));
            CreateMap<ProductsCategory, ProductsCategoryResource>();
            CreateMap<StatusEntry, Application.Resources.StatusEntries.Get.StatusEntryResource>();
            CreateMap<StatusEntry, Application.Resources.Bills.Get.StatusEntryResource>();
            CreateMap<Status, Application.Resources.Statuses.Get.StatusResource>();
            CreateMap<Status, Application.Resources.Bills.Get.StatusResource>();
            CreateMap<SignedUser, SignedUserResource>();
            CreateMap<Hall, HallResource>();
            CreateMap<Table, Application.Resources.Table.Get.TableResource>();
            CreateMap<Table, Application.Resources.Bills.Get.TableResource>();
            CreateMap<Reservation, ReservationResource>();
            CreateMap<Ingredient, IngredientForCourseResource>();
            CreateMap<Table, TableForHallResource>();
            CreateMap<Product, ProductForIngredientForCourseResource>();
            CreateMap<Course, CourseForOrderResource>();
            CreateMap<OrderedCourse, OrderedCourseForOrderResource>()
                .ForMember(p => p.CourseId, opt => opt.MapFrom(p => p.Course.Id));
            CreateMap<StatusEntry, StatusEntryForOrderResource>()
                .ForMember(p => p.StatusId, opt => opt.MapFrom(p => p.Status.Id))
                .ForMember(p => p.StatusName, opt => opt.MapFrom(p => p.Status.Name));
            CreateMap<StatusEntry, StatusEntryForOrderedCourseResource>()
                .ForMember(p => p.StatusId, opt => opt.MapFrom(p => p.Status.Id))
                .ForMember(p => p.StatusName, opt => opt.MapFrom(p => p.Status.Name));
            CreateMap<Customer, CustomerForBillResource>();

            CreateMap<Product, SaveProductResource>();
            CreateMap<Coordinate, CoordinateResource>();
        }
    }
}