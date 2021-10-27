using CatalogFunctions.Data.DomainObjects;
using CatalogFunctions.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogFunctions.Data.Repositories
{
    public interface IProductRepository : IRepository<ProductModel>
    {
        Task<List<ProductModel>> GetAllAsync();
        Task<ProductModel> GetByIdAsync(Guid id);
        Task AddAsync(ProductModel product);
        Task UpdateAsync(ProductModel product);
    }
}