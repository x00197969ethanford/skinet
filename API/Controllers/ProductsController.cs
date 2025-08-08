using System;
using API.RequestHelpers;
using Core.Entities;
using Core.interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;

//using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Build.Framework;
//using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class ProductsController(IUnitOfWork unit) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams specParams)
    {
        var spec = new ProductSpecification(specParams);

        return await CreatePageResult(unit.Repository<Product>(), spec, specParams.PageIndex ,specParams.PageSize);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var Product = await unit.Repository<Product>().GetByIdAsync(id);
        if (Product == null) return NotFound();
        
        return Product;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        unit.Repository<Product>().Add(product);

        if (await unit.Complete())
        {
            return CreatedAtAction("GetProduct", new {Id = product.Id}, product);
        }
    

        return BadRequest("Problem creating product");
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !ProductExists(id)) return BadRequest("Cannot update this product");
        
        unit.Repository<Product>().Update(product);
        
        if (await unit.Complete())
        {
            return NoContent();
        }

        return BadRequest("Problem updating the product");
    
    }

    [Authorize(Roles = "Admin")] 
    [HttpDelete("{id:int}")]

    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await unit.Repository<Product>().GetByIdAsync(id);

        if (product == null) return NotFound();

        unit.Repository<Product>().Remove(product);
        if (await unit.Complete())
        {
            return NoContent();
        }

        return BadRequest("Problem updating the product");
    
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var spec = new BrandListSpecification();
        return Ok(await unit.Repository<Product>().ListAsync(spec));
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var spec = new TypeListSpecification();
        return Ok(await unit.Repository<Product>().ListAsync(spec));
    }
    private bool ProductExists(int id)
    {
        return unit.Repository<Product>().Exists(id);
    }
}
