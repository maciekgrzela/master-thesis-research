using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Resources.Products.Save;
using Application.Responses;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IProductsService
    {
        Task<Response<IEnumerable<Product>>> ListAsync();

        Task<Response<Product>> GetProductAsync(Guid id);

        Task<Response<Product>> SaveAsync(SaveProductResource order);

        Task<Response<Product>> UpdateAsync(Guid id, SaveProductResource order);
    }
}
