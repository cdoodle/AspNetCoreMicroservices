using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Queries.GetProducts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/products")]
    public class CatalogController : ApiControllerBase
    {
        //private readonly IAsyncProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ILogger<CatalogController> logger)
        {
            //_repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> GetProducts()
        {
            //var products = await _repository.GetAllAsync();

            var products = await Mediator.Send(new GetProductsQuery()); 
            return Ok(products);
        }

        [Route("{category}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> GetProductByCategory(string category)
        {
            //var products = await _repository.GetProductByCategory(category);

            var vm = await Mediator.Send(new GetProductByCategoryQuery(category));
            return Ok(vm.Products);
        }

        [Route("{id:Guid}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> GetProductByCategory(Guid id)
        {
            //var products = await _repository.GetProductByCategory(category);

            var vm = await Mediator.Send(new GetProductByIdQuery(id));
            return Ok(vm.Products[0]);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Guid>> Create([FromBody] ProductDto product)
        {
            var productId = await Mediator.Send(new CreateProductCommand(product));
            return Ok(productId);
            //return CreatedAtRoute("GetProductById", new { id = productId }, product);
        }

        [Route("{id:Guid}")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdateProductDto product)
        {
            await Mediator.Send(new UpdateProductCommand(id, product));
            return NoContent();
        }

        [Route("{id:Guid}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> Delete(Guid id)
        {
           await Mediator.Send(new DeleteProductCommand(id));
           return NoContent();
        }
    }
}
