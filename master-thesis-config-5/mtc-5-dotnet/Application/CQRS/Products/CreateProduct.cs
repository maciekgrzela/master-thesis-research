﻿using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Responses;
using Domain.Models;
using MediatR;
using Persistence.Context;
using Persistence.Repositories.Interfaces;

namespace Application.CQRS.Products
{
    public class CreateProduct
    {
        public class Command : IRequest<Response<Unit>>
        {
            public string Name { get; set; }
            public double Amount { get; set; }
            public string Unit { get; set; }
            public Guid ProductsCategoryId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Response<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(DataContext context, IUnitOfWork unitOfWork)
            {
                _context = context;
                _unitOfWork = unitOfWork;
            }
            
            
            public async Task<Response<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var existingCategory = await _context.ProductsCategories.FindAsync(new object[] { request.ProductsCategoryId }, cancellationToken: cancellationToken);

                if(existingCategory == null)
                {
                    return new Response<Unit>(HttpStatusCode.NotFound, $"Category with id: {request.ProductsCategoryId} not found");
                }

                var newProduct = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Amount = request.Amount,
                    Unit = request.Unit,
                    ProductsCategoryId = request.ProductsCategoryId
                };

                await _context.Products.AddAsync(newProduct, cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return new Response<Unit>(HttpStatusCode.NoContent, Unit.Value);
            }
        }
    }
}