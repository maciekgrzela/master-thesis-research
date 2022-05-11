using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Orders
{
    public class GetSingleOrderCompiledQuery
    {
        public static readonly Func<DataContext, Guid, Task<Order>> SingleOrderCompiledQuery
            = EF.CompileAsyncQuery(
                (DataContext context, Guid orderId) => context.Orders
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
                    .AsNoTracking()
                    .FirstOrDefault(p => p.Id == orderId));
    }
}