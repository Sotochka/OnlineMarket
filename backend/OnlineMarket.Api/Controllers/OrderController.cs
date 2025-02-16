using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Application.DTOs.OrderDtos;
using OnlineMarket.Application.Interfaces.Services;
using OnlineMarket.Domain;
using OnlineMarket.Domain.Entities;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await orderService.GetOrdersAsync();

            return orders.IsSuccess ? Ok(orders.Value) : NotFound(orders.ErrorMessage);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await orderService.CreateOrderAsync(orderDto);

            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id)
        {
            var order = await orderService.GetOrderByIdAsync(id);

            return order.IsSuccess ? Ok(order.Value) : NotFound(order.ErrorMessage);
        }
    }
}

