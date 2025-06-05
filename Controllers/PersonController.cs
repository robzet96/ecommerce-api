using ecommerceAPI.DTO;
using ecommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ecommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var people = await _personService.GetAllAsync();
            return Ok(people);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var person = await _personService.GetByIdAsync(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpGet("search/email")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SearchByEmail([FromQuery] string email)
        {
            var results = await _personService.SearchByEmailAsync(email);
            return Ok(results);
        }

        [HttpGet("search/name")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            var results = await _personService.SearchByNameAsync(name);
            return Ok(results);
        }

        [HttpGet("search/lastname")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SearchByLastname([FromQuery] string lastname)
        {
            var results = await _personService.SearchByLastnameAsync(lastname);
            return Ok(results);
        }

        [HttpGet("search/fullname")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SearchByNameAndLastname([FromQuery] string name, [FromQuery] string lastname)
        {
            var results = await _personService.SearchByNameAndLastnameAsync(name, lastname);
            return Ok(results);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] CreatePersonDto dto)
        {
            try
            {
                var created = await _personService.RegisterAsync(dto);
                return Ok(created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var token = await _personService.LoginAsync(dto);
            if (token == null)
                return Unauthorized(new { message = "Invalid credentials." });

            return Ok(new { token });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreatePersonDto dto)
        {
            var created = await _personService.AddAsync(dto);
            return Ok(created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePersonDto dto)
        {
            var updated = await _personService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpPut("self/{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> UpdateOwnData(int id, [FromBody] UpdatePersonDto dto)
        {
            var updated = await _personService.UpdateOwnDataAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _personService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
