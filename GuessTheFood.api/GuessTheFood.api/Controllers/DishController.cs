using GuessTheFood.api.Application.DTOs.Dish;
using GuessTheFood.api.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuessTheFood.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DishController : ControllerBase
{
    private readonly IDishService _dishService;

    public DishController(IDishService dishService)
    {
        _dishService = dishService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DishReadDto>>> GetAll()
    {
        var dishes = await _dishService.GetAllAsync();

        return Ok(dishes);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DishReadDto>> GetById(Guid id)
    {
        var dish = await _dishService.GetByIdAsync(id);

        if (dish is null)
            return NotFound();

        return Ok(dish);
    }

    [HttpPost]
    public async Task<ActionResult<DishReadDto>> Create(DishCreateDto dto)
    {
        var createdDish = await _dishService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdDish.Id },
            createdDish);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, DishUpdateDto dto)
    {
        bool updated = await _dishService.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool deleted = await _dishService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}