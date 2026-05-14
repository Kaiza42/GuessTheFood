using GuessTheFood.api.Application.DTOs.Country;
using GuessTheFood.api.Domain.Entities;

namespace GuessTheFood.api.Application.Mappers;

public static class CountryMapper
{
    public static Country ToEntity(CreateCountryDto dto)
    {
        return new Country(dto.Name);
    }

    public static CountryDto ToDto(Country country)
    {
        return new CountryDto
        {
            Id = country.Id,
            Name = country.Name
        };
    }

    public static List<CountryDto> ToDtoList(IEnumerable<Country> countries)
    {
        return countries.Select(ToDto).ToList();
    }
}