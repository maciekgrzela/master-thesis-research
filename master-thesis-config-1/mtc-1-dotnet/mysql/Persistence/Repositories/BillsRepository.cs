using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class BillRepository : BaseRepository, IBillRepository
    {
        public BillRepository(DataContext context) : base(context) {}

        public async Task<List<Bill>> ListAsync()
        {
            return await context.Bills
            .Include(p => p.Customer)
            .Include(p => p.Order)
            .Include(p => p.OrderedCourses)
            .ToListAsync();
        }

        public IQueryable<Bill> QueryableAsync()
        {
            return context.Bills
            .Include(p => p.Customer)
            .Include(p => p.Order)
            .Include(p => p.OrderedCourses)
            .AsQueryable();
        }

        public async Task<Bill> GetBillAsync(Guid id)
        {
            return await context.Bills
            .Include(p => p.Customer)
            .Include(p => p.Order)
            .Include(p => p.OrderedCourses)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SaveAsync(Bill bill)
        {
            await context.Bills.AddAsync(bill);
        }

        public void Update(Bill bill)
        {
            context.Bills.Update(bill);
        }

        public void Delete(Bill bill)
        {
            context.Bills.Remove(bill);
        }
    }
}