﻿using System.Net.NetworkInformation;
using Asp.Versioning;

//using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalkAPICore8.Model.Domain;
using NZWalkAPICore8.Model.DTO;
//using ApiVersion = Microsoft.AspNetCore.Mvc.ApiVersion;
//using ApiVersionAttribute = Asp.Versioning.ApiVersionAttribute;

namespace NZWalkAPICore8.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //[Route("api/products")]

    [ApiVersion(1)]
    [ApiVersion(2)]
    [ApiController]
    [Route("api/v{v:apiVersion}/Product")]
    public class ProductController : ControllerBase
    {
        private static readonly List<Product> _products = new List<Product>()
        {
            new Product { Id = 1, Name="Nokia 12.09" },
            new Product { Id = 2, Name= "motorala 4.0" }
        };

        // GET Version 1: Return Id and Name only
        [HttpGet]
        [MapToApiVersion(1)]
        public IActionResult GetV1()
        {
            var productsV1 = _products.Select(p => new ProductResponseV1
            {
                Id = p.Id,
                Name = p.Name
            });

            return Ok(productsV1);
        }

        // GET Version 2: Return Id, Name, and Price
        [HttpGet]
        [MapToApiVersion(2)]
        public IActionResult GetV2()
        {
            var productsV2 = _products.Select(p => new ProductResponseV2
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price ?? 0
            });

            return Ok(productsV2);
        }

        // POST Version 1: Accept Name only, create product without price
        [HttpPost]
        //[ApiVersion("1.0")]
        public IActionResult PostV1([FromBody] ProductCreateRequestV1 request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Name is required.");

            int newId = _products.Max(p => p.Id) + 1;
            var newProduct = new Product
            {
                Id = newId,
                Name = request.Name,
                Price = 0  // Price not supported in v1, default 0
            };
            _products.Add(newProduct);

            // Return version 1 response DTO (without price)
            var response = new ProductResponseV1
            {
                Id = newProduct.Id,
                Name = newProduct.Name
            };

            return Ok(response);
        }

        // POST Version 2: Accept Name and Price, create full product
        //[HttpPost]
        ////[ApiVersion("2.0")]
        //public IActionResult PostV2([FromBody] ProductCreateRequestV2 request)
        //{
        //    if (string.IsNullOrWhiteSpace(request.Name))
        //        return BadRequest("Name is required.");
        //    if (request.Price <= 0)
        //        return BadRequest("Price must be greater than zero.");

        //    int newId = _products.Max(p => p.Id) + 1;
        //    var newProduct = new Product
        //    {
        //        Id = newId,
        //        Name = request.Name,
        //        Price = request.Price
        //    };
        //    _products.Add(newProduct);

        //    return Ok(newProduct);
        //}
    }
}

    

