using GuessTheFood.api.Application.DTOs.Country;

namespace GuessTheFood.api.Application.Interfaces.Services;

public interface ICountryService
{
    Task<CountryDto> CreateAsync(CreateCountryDto dto);

    Task<CountryDto?> UpdateAsync(Guid id, UpdateCountryDto dto);

    Task<CountryDto?> GetByIdAsync(Guid id);

    Task<CountryDto?> GetByNameAsync(string name);

    Task<List<CountryDto>> GetAllAsync();

    Task<bool> DeleteAsync(Guid id);
}