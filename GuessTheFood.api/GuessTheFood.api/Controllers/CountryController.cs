using GuessTheFood.api.Application.DTOs.Country;
using GuessTheFood.api.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuessTheFood.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpPost]
    public async Task<ActionResult<CountryDto>> Create(
        [FromBody] CreateCountryDto dto)
    {
        try
        {
            CountryDto country = await _countryService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = country.Id },
                country);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CountryDto>> Update(
        Guid id,
        [FromBody] UpdateCountryDto dto)
    {
        try
        {
            CountryDto country = await _countryService.UpdateAsync(id, dto);

            return Ok(country);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CountryDto>> GetById(Guid id)
    {
        CountryDto? country = await _countryService.GetByIdAsync(id);

        if (country is null)
            return NotFound();

        return Ok(country);
    }

    [HttpGet("by-name/{name}")]
    public async Task<ActionResult<CountryDto>> GetByName(string name)
    {
        CountryDto? country = await _countryService.GetByNameAsync(name);

        if (country is null)
            return NotFound();

        return Ok(country);
    }

    [HttpGet]
    public async Task<ActionResult<List<CountryDto>>> GetAll()
    {
        List<CountryDto> countries = await _countryService.GetAllAsync();

        return Ok(countries);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool deleted = await _countryService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}