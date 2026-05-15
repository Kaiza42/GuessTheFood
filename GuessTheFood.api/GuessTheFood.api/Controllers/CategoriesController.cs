using GuessTheFood.api.Application.DTOs.Category;
using GuessTheFood.api.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuessTheFood.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryReadDto>>> GetAll()
    {
        List<CategoryReadDto> categories = await _categoryService.GetAllAsync();

        return Ok(categories);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryReadDto>> GetById(Guid id)
    {
        CategoryReadDto? category = await _categoryService.GetByIdAsync(id);

        if (category is null)
            return NotFound();

        return Ok(category);
    }

    [HttpGet("by-name/{name}")]
    public async Task<ActionResult<CategoryReadDto>> GetByName(string name)
    {
        CategoryReadDto? category = await _categoryService.GetByNameAsync(name);

        if (category is null)
            return NotFound();

        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryReadDto>> Create(CategoryCreateDto dto)
    {
        try
        {
            CategoryReadDto category = await _categoryService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = category.Id },
                category);
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new { message = exception.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, CategoryUpdateDto dto)
    {
        try
        {
            bool updated = await _categoryService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new { message = exception.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool deleted = await _categoryService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}