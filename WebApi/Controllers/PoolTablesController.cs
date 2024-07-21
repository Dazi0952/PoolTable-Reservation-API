using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoolTablesController : ControllerBase
    {
        private readonly IPoolTableService _pooltableService;

        public PoolTablesController(IPoolTableService pooltableService)
        {
            _pooltableService = pooltableService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPoolTableById(int id)
        {
            var pooltable = await _pooltableService.GetPoolTableByIdAsync(id);
            if (pooltable == null)
            {
                return NotFound();
            }
            return Ok(pooltable);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPoolTables()
        {
            var PoolTables = await _pooltableService.GetAllPoolTablesAsync();
            return Ok(PoolTables);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePoolTable([FromBody] CreatePoolTableDto pooltableDto)
        {
            if (pooltableDto == null)
            {
                return BadRequest();
            }
            await _pooltableService.AddPoolTableAsync(pooltableDto);
            return CreatedAtAction(nameof(GetPoolTableById), new { id = pooltableDto.Id }, pooltableDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePoolTable(int id, [FromBody] UpdatePoolTableDto pooltableDto)
        {
            if (pooltableDto == null || id != pooltableDto.Id)
            {
                return BadRequest();
            }
            await _pooltableService.UpdatePoolTableAsync(pooltableDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoolTable(int id)
        {
            await _pooltableService.DeletePoolTableAsync(id);
            return NoContent();
        }
    }
}
