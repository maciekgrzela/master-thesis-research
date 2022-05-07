using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Persistence.Context;
using Persistence.Repositories.Interfaces;
using Customer = Domain.Models.MongoDb.Customer;
using MongoDatabaseSettings = Persistence.Configuration.MongoDatabaseSettings;

namespace Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext context;
        private readonly IMongoCollection<Domain.Models.MongoDb.Customer> customers;

        public CustomerRepository(DataContext context, IOptions<MongoDatabaseSettings> mongoDatabaseSettings)
        {
            var mongoClient = new MongoClient(mongoDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDatabaseSettings.Value.DatabaseName);
            customers = mongoDatabase.GetCollection<Domain.Models.MongoDb.Customer>("Customers");
            this.context = context;
        }

        public async Task Delete(Domain.Models.MongoDb.Customer customer)
        {
            await customers.DeleteOneAsync(x => x.Id == customer.Id);
        }

        public async Task<List<Domain.Models.MongoDb.Customer>> GetAllAsync()
        {
            return await customers.Find(_ => true).ToListAsync();
        }

        public async Task<Domain.Models.MongoDb.Customer> GetAsync(string id)
        {
            return await customers.Find(x => x.Id == id).FirstOrDefaultAsync();  
        }

        public async Task SaveAsync(Domain.Models.MongoDb.Customer customer)
        {
            await customers.InsertOneAsync(customer);
        }

        public async Task Update(Domain.Models.MongoDb.Customer customer)
        {
            await customers.ReplaceOneAsync(x => x.Id == customer.Id, customer);
        }
    }
}