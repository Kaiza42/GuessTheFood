using GuessTheFood.api.Application.DTOs.Ingredients;
using GuessTheFood.api.Application.Interfaces.Services;
using GuessTheFood.api.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace GuessTheFood.api.Controllers;

/// <summary>
/// Manages ingredients used in dishes.
/// Provides CRUD operations and filtering by ingredient type.
/// </summary>
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

    /// <summary>
    /// Retrieves all ingredients.
    /// </summary>
    /// <returns>List of ingredients.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<IngredientDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<IngredientDto>>> GetAll()
    {
        var ingredients = await _ingredientService.GetAllAsync();

        return Ok(ingredients);
    }

    /// <summary>
    /// Retrieves an ingredient by its identifier.
    /// </summary>
    /// <param name="id">Ingredient identifier.</param>
    /// <returns>The matching ingredient.</returns>
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

    /// <summary>
    /// Retrieves ingredients filtered by type.
    /// </summary>
    /// <param name="type">Ingredient type.</param>
    /// <returns>List of matching ingredients.</returns>
    [HttpGet("type/{type}")]
    [ProducesResponseType(typeof(List<IngredientDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<IngredientDto>>> GetByType(
        IngredientType type)
    {
        var ingredients = await _ingredientService.GetByTypeAsync(type);

        return Ok(ingredients);
    }

    /// <summary>
    /// Creates a new ingredient.
    /// </summary>
    /// <param name="dto">Ingredient creation data.</param>
    /// <returns>The created ingredient.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(IngredientDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IngredientDto>> Create([FromBody] CreateIngredientDto dto)
    {
        var ingredient = await _ingredientService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = ingredient.Id },
            ingredient);
    }

    /// <summary>
    /// Updates an existing ingredient.
    /// </summary>
    /// <param name="id">Ingredient identifier.</param>
    /// <param name="dto">Updated ingredient data.</param>
    /// <returns>No content if update succeeded.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateIngredientDto dto)
    {
        var updated = await _ingredientService.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Deletes an ingredient.
    /// </summary>
    /// <param name="id">Ingredient identifier.</param>
    /// <returns>No content if deletion succeeded.</returns>
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
    
    /// <summary>
    /// Retrieves an ingredient by name.
    /// </summary>
    /// <param name="name">Ingredient name.</param>
    /// <returns>The matching ingredient.</returns>
    [HttpGet("name/{name}")]
    [ProducesResponseType(typeof(IngredientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IngredientDto>> GetByName(string name)
    {
        var ingredient = await _ingredientService.GetByNameAsync(name);

        if (ingredient is null)
            return NotFound();

        return Ok(ingredient);
    }
}