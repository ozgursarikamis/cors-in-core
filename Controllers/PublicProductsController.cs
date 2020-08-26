using System;
using System.Collections.Generic;
using Bogus;
using CorsInCore.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CorsInCore.Controllers
{
    [ApiController, Route("api/public/[controller]")]
    [EnableCors("PublicApi")]
    public class PublicProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            ResponseSettings.SetPaginationHeader(HttpContext, 10, 1, 100, 10000);
            var data = GetProducts();
            return Ok(data);
        }

        [HttpGet]
        public IEnumerable<ProductModel> GetProducts()
        {
            var products = new Faker<ProductModel>()
                    .RuleFor(model => model.Id, f => Guid.NewGuid())
                    .RuleFor(model => model.Name, f => f.Commerce.ProductName())
                    .RuleFor(model => model.Image, f => f.Image.PicsumUrl(200, 200))
                    .RuleFor(model => model.Price, f => f.Commerce.Price())
                ;

            return products.Generate(20);
        }
    }
}
