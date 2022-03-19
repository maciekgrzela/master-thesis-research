using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext context;

        public CustomerRepository(DataContext context)
        {
            this.context = context;
        }

        public void Delete(Customer customer)
        {
            context.Customers.Remove(customer);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await context.Customers.ToListAsync();
        }

        public async Task<Customer> GetAsync(Guid id)
        {
            return await context.Customers.FirstOrDefaultAsync(c => c.Id == id);  
        }

        public async Task SaveAsync(Customer customer)
        {
            await context.Customers.AddAsync(customer);
        }

        public void Update(Customer customer)
        {
            context.Customers.Update(customer);
        }
    }
}