using System;
using System.Collections.Generic;
using Bogus;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CorsInCore.Controllers
{
    [ApiController, Route("api/public/[controller]")]
    [EnableCors("PublicApi")]
    public class PublicProductsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ProductModel> Get()
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
