using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Resources.Course.Save;
using Application.Responses;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories.Interfaces;

namespace Application.CQRS.Courses
{
    public class UpdateCourse
    {
        public class Command : IRequest<Response<Unit>>
        {
            public string Name { get; set; }
            public double GrossPrice { get; set; }
            public double NetPrice { get; set; }
            public int Tax { get; set; }
            public int PreparationTimeInMinutes { get; set; }
            public Guid CoursesCategoryId { get; set; }
            public List<SaveIngredientForCourseResource> Ingredients { get; set; }
            private Guid _id;

            public void SetId(Guid id)
            {
                _id = id;
            }

            public Guid GetId()
            {
                return _id;
            }
        }

        public class Handler : IRequestHandler<Command, Response<Unit>>
        {
            private readonly DataReadContext _context;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(DataReadContext context, IUnitOfWork unitOfWork)
            {
                _context = context;
                _unitOfWork = unitOfWork;
            }
            
            
            public async Task<Response<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var courseId = request.GetId();
                
                var existingCourse = await _context.Courses
                    .Include(p => p.Ingredients)
                    .FirstOrDefaultAsync(p => p.Id == courseId, cancellationToken: cancellationToken);

                if (existingCourse == null)
                {
                    return new Response<Unit>(HttpStatusCode.NotFound, $"Course with id:{courseId} not found");
                }

                var existingCourseCategory = await _context.CourseCategories
                    .AsNoTracking()
                    .AnyAsync(p => p.Id == courseId, cancellationToken: cancellationToken);

                if (existingCourseCategory)
                {
                    return new Response<Unit>(HttpStatusCode.NotFound, $"Category with id:{request.CoursesCategoryId} not found");
                }

                var ingredientsList = new List<Ingredient>();

                foreach (var ingredient in request.Ingredients)
                {
                    var existingProduct = await _context.Products
                        .FirstOrDefaultAsync(p => p.Id == ingredient.ProductId, cancellationToken: cancellationToken);

                    if(existingProduct == null)
                    {
                        return new Response<Unit>(HttpStatusCode.NotFound, $"Product with id:{ingredient.ProductId} not found");
                    }

                    var ingredientToAdd = new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Amount = ingredient.Amount,
                        Product = existingProduct
                    };

                    ingredientsList.Add(ingredientToAdd);
                }

                await _context.Ingredients.AddRangeAsync(ingredientsList, cancellationToken);

                existingCourse.Name = request.Name;
                existingCourse.GrossPrice = request.GrossPrice;
                existingCourse.NetPrice = request.NetPrice;
                existingCourse.Tax = request.Tax;
                existingCourse.PreparationTimeInMinutes = request.PreparationTimeInMinutes;
                existingCourse.CoursesCategoryId = request.CoursesCategoryId;
                existingCourse.Ingredients = ingredientsList;

                _context.Courses.Update(existingCourse);
                await _unitOfWork.CommitTransactionAsync();

                return new Response<Unit>(HttpStatusCode.NoContent, Unit.Value);
            }
        }
    }
}