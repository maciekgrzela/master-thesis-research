using Application.Resources.Auth;
using Application.Resources.Bills.Get;
using Application.Resources.Bills.Save;
using Application.Resources.Course.Get;
using Application.Resources.Course.Save;
using Application.Resources.CourseCategories.Save;
using Application.Resources.Customers.Save;
using Application.Resources.Hall.Save;
using Application.Resources.Ingredients.Save;
using Application.Resources.OrderedCourses.Save;
using Application.Resources.Orders.Save;
using Application.Resources.ProductsCategories.Save;
using Application.Resources.Reservation.Save;
using Application.Resources.Table.Save;
using AutoMapper;
using Domain.Models;

namespace API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCustomerResource, Customer>();
            CreateMap<SaveBillResource, Bill>();
            CreateMap<SaveCoursesCategoryResource, CoursesCategory>();
            CreateMap<SaveIngredientResource, Ingredient>();
            CreateMap<SaveOrderedCourseResource, OrderedCourse>();
            CreateMap<SaveProductsCategoryResource, ProductsCategory>();
            CreateMap<LoginCredentialsResource, LoginCredentials>();
            CreateMap<RegisterCredentialsResource, RegisterCredentials>();
            CreateMap<SaveTableResource, Table>();
            CreateMap<SaveHallResource, Hall>();
            CreateMap<SaveReservationResource, Reservation>();
            CreateMap<CustomerForBillResource, Customer>();
            CreateMap<SaveCourseResource, Course>();
            CreateMap<ProductForIngredientForCourseResource, Product>();
            CreateMap<OrderedCourseForSaveOrderResource, OrderedCourse>();
            CreateMap<SaveOrderResource, Order>();
        }
    }
}