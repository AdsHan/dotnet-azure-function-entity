using CatalogFunctions.Data.Entities;
using CatalogFunctions.Data.Repositories;
using CatalogFunctions.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CatalogFunctions.Functions
{
    public class PostProducts
    {
        private readonly IProductRepository _productRepository;

        public PostProducts(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /*
            POST / Produto
            {
	            "title": "Sandalia",
	            "description": "Sandália Preta Couro Salto Fino",
	            "price": 249.50,
	            "quantity": 100       
            }
        */
        [FunctionName("PostProducts")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "products")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            ProductDTO productDTO = JsonConvert.DeserializeObject<ProductDTO>(requestBody);

            if (!productDTO.Validate()) return new BadRequestObjectResult("Produto Inválido");

            var product = new ProductModel()
            {
                Title = productDTO.Title,
                Description = productDTO.Description,
                Price = productDTO.Price,
                Quantity = productDTO.Quantity
            };

            try
            {
                await _productRepository.AddAsync(product);

                await _productRepository.SaveAsync();

                var result = new ObjectResult(JsonConvert.SerializeObject(product));
                result.StatusCode = StatusCodes.Status201Created;
                return result;

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
