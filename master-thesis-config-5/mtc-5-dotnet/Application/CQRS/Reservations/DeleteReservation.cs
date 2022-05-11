using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Courses;
using Application.Resources.Course.Save;
using Application.Responses;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories.Interfaces;

namespace Application.CQRS.Reservations
{
    public class DeleteReservation
    {
        public class Command : IRequest<Response<Unit>>
        {
            public Guid Id { get; set; }
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
                var existingReservation = await _context.Reservations
                    .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken); 

                if (existingReservation == null)
                {
                    return new Response<Unit>(HttpStatusCode.NotFound, $"Reservation with id:{request.Id} not found");
                }

                _context.Reservations.Remove(existingReservation);
                await _unitOfWork.CommitTransactionAsync();

                return new Response<Unit>(HttpStatusCode.NoContent, Unit.Value);
            }
        }
    }
}