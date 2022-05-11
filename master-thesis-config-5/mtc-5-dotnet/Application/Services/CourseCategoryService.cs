using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Resources.CourseCategories.Save;
using Application.Responses;
using Application.Services.Interfaces;
using Domain.Models;
using Persistence.Repositories.Interfaces;

namespace Application.Services
{
    public class CourseCategoryService : ICourseCategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICourseCategoryRepository courseCategoryRepository;
        public CourseCategoryService(ICourseCategoryRepository courseCategoryRepository, IUnitOfWork unitOfWork)
        {
            this.courseCategoryRepository = courseCategoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<CoursesCategory>> DeleteAsync(Guid id)
        {
            var existingCourseCategory = await courseCategoryRepository.GetCoursesCategoryAsync(id);

            if (existingCourseCategory == null)
            {
                return new Response<CoursesCategory>(HttpStatusCode.NotFound, $"Courses Category with id:{id} not found");
            }

            courseCategoryRepository.Delete(existingCourseCategory);
            await unitOfWork.CommitTransactionAsync();

            return new Response<CoursesCategory>(HttpStatusCode.NoContent, existingCourseCategory);
        }

        public async Task<Response<CoursesCategory>> GetCoursesCategoryAsync(Guid id)
        {
            var existingCourseCategory = await courseCategoryRepository.GetCoursesCategoryAsync(id);

            if (existingCourseCategory == null)
            {
                return new Response<CoursesCategory>(HttpStatusCode.NotFound, $"Bill with id:{id} not found");
            }

            return new Response<CoursesCategory>(HttpStatusCode.OK, existingCourseCategory);
        }

        public async Task<Response<IEnumerable<CoursesCategory>>> ListAsync()
        {
            var courseCategories = await courseCategoryRepository.ListAsync();
            return new Response<IEnumerable<CoursesCategory>>(HttpStatusCode.OK, courseCategories);
        }

        public async Task<Response<CoursesCategory>> SaveAsync(CoursesCategory category)
        {
            var coursesCategory = new CoursesCategory
            {
                Id = Guid.NewGuid(),
                Name = category.Name
            };

            await courseCategoryRepository.SaveAsync(coursesCategory);
            await unitOfWork.CommitTransactionAsync();

            return new Response<CoursesCategory>(HttpStatusCode.NoContent, coursesCategory);
        }

        public async Task<Response<CoursesCategory>> UpdateAsync(Guid id, CoursesCategory category)
        {
            var existingCoursesCategory = await courseCategoryRepository.GetCoursesCategoryAsync(id);

            if(existingCoursesCategory == null)
            {
                return new Response<CoursesCategory>(HttpStatusCode.NotFound, $"Courses Category with id:{id} not found");
            }

            existingCoursesCategory.Name = category.Name;

            courseCategoryRepository.Update(existingCoursesCategory);
            await unitOfWork.CommitTransactionAsync();

            return new Response<CoursesCategory>(HttpStatusCode.NoContent, existingCoursesCategory);
        }
    }
}