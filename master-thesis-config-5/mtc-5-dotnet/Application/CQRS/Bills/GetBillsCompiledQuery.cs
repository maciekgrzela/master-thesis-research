using System;
using System.Collections.Generic;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Bills
{
    public class GetBillsCompiledQuery
    {
        public static readonly Func<DataContext, IEnumerable<Bill>> BillsCompiledQuery
                    = EF.CompileQuery(
                        (DataContext context) => context.Bills
                        .Include(p => p.Customer)
                        .Include(p => p.Order)
                        .Include(p => p.OrderedCourses)
                        .AsSplitQuery()
                        .AsNoTracking()
                        );
    }
}