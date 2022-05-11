using Application.Resources.Course.Save;
using Application.Responses;
using Application.Services.Interfaces;
using Domain.Models;
using Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CoursesService : ICoursesService
    {
        private readonly ICoursesRepository coursesRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICourseCategoryRepository courseCategoryRepository;
        private readonly IIngredientRepository ingredientRepository;
        private readonly IProductRepository productRepository;

        public CoursesService(ICoursesRepository coursesRepository, ICourseCategoryRepository courseCategoryRepository, IIngredientRepository ingredientRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.ingredientRepository = ingredientRepository;
            this.courseCategoryRepository = courseCategoryRepository;
            this.coursesRepository = coursesRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<Course>> GetCourseAsync(Guid id)
        {
            var existingCourse = await coursesRepository.GetCourseAsync(id);

            if (existingCourse == null)
            {
                return new Response<Course>(HttpStatusCode.NotFound, $"Course with id:{id} not found");
            }

            return new Response<Course>(HttpStatusCode.OK, existingCourse);
        }

        public async Task<Response<IEnumerable<Course>>> ListAsync()
        {
            var courses = await coursesRepository.ListAsync();
            return new Response<IEnumerable<Course>>(HttpStatusCode.OK, courses);
        }

        public async Task<Response<Course>> SaveAsync(SaveCourseResource course)
        {
            var existingCourseCategory = await courseCategoryRepository.GetCoursesCategoryAsync(course.CoursesCategoryId);

            if (existingCourseCategory == null)
            {
                return new Response<Course>(HttpStatusCode.NotFound, $"Category with id:{course.CoursesCategoryId} not found");
            }

            var ingredientsList = new List<Ingredient>();

            foreach (var ingredient in course.Ingredients)
            {
                var existingProduct = await productRepository.GetProductAsync(ingredient.ProductId);

                if(existingProduct == null)
                {
                    return new Response<Course>(HttpStatusCode.NotFound, $"Product with id:{ingredient.ProductId} not found");
                }

                var ingredientToAdd = new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Amount = ingredient.Amount,
                    Product = existingProduct
                };

                ingredientsList.Add(ingredientToAdd);
            }

            var savedCourse = new Course
            {
                Id = Guid.NewGuid(),
                Name = course.Name,
                GrossPrice = course.GrossPrice,
                NetPrice = course.NetPrice,
                Tax = course.Tax,
                PreparationTimeInMinutes = course.PreparationTimeInMinutes,
                CoursesCategory = existingCourseCategory,
                Ingredients = ingredientsList
            };

            await coursesRepository.SaveAsync(savedCourse);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Course>(HttpStatusCode.NoContent, savedCourse);
        }

        public async Task<Response<Course>> UpdateAsync(Guid id, SaveCourseResource course)
        {
            var existingCourse = await coursesRepository.GetCourseAsync(id);

            if (existingCourse == null)
            {
                return new Response<Course>(HttpStatusCode.NotFound, $"Course with id:{id} not found");
            }

            var existingCourseCategory = await courseCategoryRepository.GetCoursesCategoryAsync(course.CoursesCategoryId);

            if (existingCourseCategory == null)
            {
                return new Response<Course>(HttpStatusCode.NotFound, $"Category with id:{course.CoursesCategoryId} not found");
            }

            var ingredientsList = new List<Ingredient>();

            foreach (var ingredient in course.Ingredients)
            {
                var existingProduct = await productRepository.GetProductAsync(ingredient.ProductId);

                if(existingProduct == null)
                {
                    return new Response<Course>(HttpStatusCode.NotFound, $"Product with id:{ingredient.ProductId} not found");
                }

                var ingredientToAdd = new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Amount = ingredient.Amount,
                    Product = existingProduct
                };

                ingredientsList.Add(ingredientToAdd);
            }

            await ingredientRepository.SaveRangeAsync(ingredientsList);

            existingCourse.Name = course.Name;
            existingCourse.GrossPrice = course.GrossPrice;
            existingCourse.NetPrice = course.NetPrice;
            existingCourse.Tax = course.Tax;
            existingCourse.PreparationTimeInMinutes = course.PreparationTimeInMinutes;
            existingCourse.CoursesCategoryId = course.CoursesCategoryId;
            existingCourse.Ingredients = ingredientsList;

            coursesRepository.Update(existingCourse);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Course>(HttpStatusCode.NoContent, existingCourse);
        }

        public async Task<Response<Course>> DeleteAsync(Guid id)
        {
            var existingCourse = await coursesRepository.GetCourseAsync(id);

            if (existingCourse == null)
            {
                return new Response<Course>(HttpStatusCode.NotFound, $"Course with id:{id} not found");
            }

            coursesRepository.Delete(existingCourse);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Course>(HttpStatusCode.NoContent, existingCourse);
        }

        public async Task<Response<IEnumerable<Ingredient>>> GetIngredientsForCourseAsync(Guid id)
        {
            var existingCourse = await coursesRepository.GetCourseAsync(id);

            if (existingCourse == null)
            {
                return new Response<IEnumerable<Ingredient>>(HttpStatusCode.NotFound, $"Course with id:{id} not found");
            }

            var ingredients = existingCourse.Ingredients;

            return new Response<IEnumerable<Ingredient>>(HttpStatusCode.OK, ingredients);
        }
    }
}
