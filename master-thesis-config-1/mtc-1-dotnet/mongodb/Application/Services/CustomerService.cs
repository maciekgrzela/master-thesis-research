using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Resources.Customers.Save;
using Application.Responses;
using Application.Services.Interfaces;
using Domain.Models;
using Persistence.Repositories;
using Persistence.Repositories.Interfaces;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICustomerRepository customerRepository;
        
        public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            this.customerRepository = customerRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<Domain.Models.MongoDb.Customer>> Delete(string id)
        {
            var existingCustomer = await customerRepository.GetAsync(id);

            if (existingCustomer == null)
            {
                return new Response<Domain.Models.MongoDb.Customer>(HttpStatusCode.NotFound, $"Customer with id:{id} not found");
            }

            await customerRepository.Delete(existingCustomer);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Domain.Models.MongoDb.Customer>(HttpStatusCode.NoContent, existingCustomer);
        }

        public async Task<Response<List<Domain.Models.MongoDb.Customer>>> GetAllAsync()
        {
            var customers = await customerRepository.GetAllAsync();
            return new Response<List<Domain.Models.MongoDb.Customer>>(HttpStatusCode.OK, customers);
        }

        public async Task<Response<Domain.Models.MongoDb.Customer>> GetAsync(string id)
        {
            var customer = await customerRepository.GetAsync(id);

            return customer == null ? new Response<Domain.Models.MongoDb.Customer>(HttpStatusCode.NotFound, $"Customer with id:{id} not found") : new Response<Domain.Models.MongoDb.Customer>(HttpStatusCode.OK, customer);
        }

        public async Task<Response<Domain.Models.MongoDb.Customer>> SaveAsync(SaveCustomerResource customer)
        {
            var newCustomer = new Domain.Models.MongoDb.Customer
            {
                Name = customer.Name,
                Address1 = customer.Address1,
                Address2 = customer.Address2,
                NIP = customer.NIP
            };

            await customerRepository.SaveAsync(newCustomer);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Domain.Models.MongoDb.Customer>(HttpStatusCode.NoContent, newCustomer);
        }

        public async Task<Response<Domain.Models.MongoDb.Customer>> Update(string id, SaveCustomerResource customer)
        {
            var existingCustomer = await customerRepository.GetAsync(id);

            if(existingCustomer == null)
            {
                return new Response<Domain.Models.MongoDb.Customer>(HttpStatusCode.NotFound, $"Customer with id:{id} not found");
            }

            existingCustomer.Name = customer.Name;
            existingCustomer.Address1 = customer.Address1;
            existingCustomer.Address2 = customer.Address2;

            await customerRepository.Update(existingCustomer);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Domain.Models.MongoDb.Customer>(HttpStatusCode.NoContent, existingCustomer);
        }
    }
}