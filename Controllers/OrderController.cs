using ecommerceAPI.DTO;
using ecommerceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ecommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("admin-add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddByAdmin([FromBody] CreateOrderDto dto)
        {
            var created = await _orderService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpGet("by-date")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByDate([FromQuery] DateTime date)
        {
            var orders = await _orderService.GetOrdersByDateAsync(date);
            return Ok(orders);
        }

        [HttpGet("by-range")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var orders = await _orderService.GetOrdersByDateRangeAsync(startDate, endDate);
            return Ok(orders);
        }

        [HttpGet("by-product-id/{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var orders = await _orderService.GetOrdersContainingProductAsync(productId);
            return Ok(orders);
        }

        [HttpGet("by-product-name")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByProductName([FromQuery] string name)
        {
            var orders = await _orderService.GetOrdersContainingProductByNameAsync(name);
            return Ok(orders);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            try
            {
                var created = await _orderService.CreateOrderAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderDto dto)
        {
            try
            {
                var updated = await _orderService.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _orderService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
        [HttpGet("by-user")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetByLoggedInUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            if (!int.TryParse(userIdClaim.Value, out int userId)) return Unauthorized();

            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }
    }
}
