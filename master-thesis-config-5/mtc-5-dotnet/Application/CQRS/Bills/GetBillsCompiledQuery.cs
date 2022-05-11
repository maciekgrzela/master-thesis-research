using System;
using System.Linq;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Bills
{
    public static class GetBillsCompiledQuery
    {
        public static readonly Func<DataReadContext, IQueryable<Bill>> BillsCompiledQuery
            = EF.CompileQuery<DataReadContext, IQueryable<Bill>>(
                context => context.Bills
                    .Include(p => p.Customer)
                    .Include(p => p.Order)
                    .Include(p => p.OrderedCourses)
                    .AsSplitQuery()
                    .AsNoTracking().AsQueryable()
                );
    }
}