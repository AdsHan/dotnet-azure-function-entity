using CatalogFunctions.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CatalogFunctions.Functions
{
    public class GetProductsById
    {
        private readonly IProductRepository _productRepository;

        public GetProductsById(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [FunctionName("GetProductsById")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "products/{id}")] HttpRequest req,
            Guid id,
            ILogger log)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    var result = new ObjectResult("Product Not Found");
                    result.StatusCode = StatusCodes.Status404NotFound;
                    return result;
                }
                return new OkObjectResult(product);

                log.LogInformation($"Function executada em: {DateTime.Now}");
            }
            catch (Exception e)
            {
                log.LogCritical(e.Message);
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
