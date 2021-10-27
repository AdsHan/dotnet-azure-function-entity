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
    public class GetProducts
    {
        private readonly IProductRepository _productRepository;

        public GetProducts(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [FunctionName("GetProducts")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "products")] HttpRequest req,
            ILogger log)
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                return new OkObjectResult(products);

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
