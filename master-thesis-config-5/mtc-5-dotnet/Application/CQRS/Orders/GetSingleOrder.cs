using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Resources.Orders.Get;
using Application.Responses;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Orders
{
    public class GetSingleOrder
    {
        public class Query : IRequest<Response<OrderResource>>
        {
            public Guid Id { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, Response<OrderResource>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            
            public async Task<Response<OrderResource>> Handle(Query request, CancellationToken cancellationToken)
            {
                var order = await _context.Orders
                    .Include(p => p.Table)
                    .Include(p => p.User)
                    .Include(p => p.StatusEntries)
                    .ThenInclude(p => p.Status)
                    .Include(p => p.OrderedCourses)
                    .ThenInclude(p => p.Course)
                    .Include(p => p.OrderedCourses)
                    .ThenInclude(p => p.StatusEntries)
                    .ThenInclude(p => p.Status)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);

                var orderResource = _mapper.Map<Order, OrderResource>(order);

                return new Response<OrderResource>(HttpStatusCode.OK, orderResource);
            }
        }
    }
}