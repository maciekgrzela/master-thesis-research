using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Params;
using Application.Resources.Bills.Get;
using Application.Responses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Bills
{
    public class GetBills
    {
        public class Query : IRequest<Response<PagedList<BillResource>>>
        {
            public PagingParams QueryParams { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, Response<PagedList<BillResource>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            
            public async Task<Response<PagedList<BillResource>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var bills = _context.Bills
                    .Include(p => p.Customer)
                    .Include(p => p.Order)
                    .Include(p => p.OrderedCourses)
                    .ProjectTo<BillResource>(_mapper.ConfigurationProvider)
                    .AsQueryable();

                var pagedList = await PagedList<BillResource>.ToPagedListAsync(bills, request.QueryParams.PageNumber, request.QueryParams.PageSize);

                return new Response<PagedList<BillResource>>(HttpStatusCode.OK, pagedList);
            }
        }
    }
}