using GuessTheFood.api.Application.DTOs.Ingredients;
using GuessTheFood.api.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuessTheFood.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class IngredientsController : ControllerBase
{
    private readonly IIngredientService _ingredientService;

    public IngredientsController(IIngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<IngredientDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<IngredientDto>>> GetAll()
    {
        var ingredients = await _ingredientService.GetAllAsync();

        return Ok(ingredients);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(IngredientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IngredientDto>> GetById(Guid id)
    {
        var ingredient = await _ingredientService.GetByIdAsync(id);

        if (ingredient is null)
            return NotFound();

        return Ok(ingredient);
    }

    [HttpPost]
    [ProducesResponseType(typeof(IngredientDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IngredientDto>> Create(CreateIngredientDto dto)
    {
        var ingredient = await _ingredientService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = ingredient.Id },
            ingredient);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateIngredientDto dto)
    {
        var updated = await _ingredientService.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _ingredientService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}