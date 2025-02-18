using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Application.DTOs;
using OnlineMarket.Application.Interfaces.Services;

namespace OnlineMarket.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        var products = await productService.GetProductsAsync();

        return Ok(products);
    }

    [HttpGet("id/{id:int}")]
    public async Task<ActionResult<ProductDto>> GetProductById([FromRoute] int id)
    {
        var product = await productService.GetProductByIdAsync(id);

        return Ok(product);
    }

    [HttpGet("code/{code:int}")]
    public async Task<IActionResult> GetProductByCode([FromRoute] int code)
    {
        var product = await productService.GetProductByCodeAsync(code);

        return Ok(product);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await productService.CreateProductAsync(product);

        return result.IsSuccess ? Created() : BadRequest(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto product, [FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await productService.UpdateProductAsync(product, id);

        return Ok(result);
    }
}