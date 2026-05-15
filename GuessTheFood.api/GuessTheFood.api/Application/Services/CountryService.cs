using GuessTheFood.Api.Infrastructure.Persistence;
using GuessTheFood.api.Application.DTOs.Country;
using GuessTheFood.api.Application.Interfaces.Services;
using GuessTheFood.api.Application.Mappers;
using GuessTheFood.api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuessTheFood.api.Application.Services;

public class CountryService : ICountryService
{
    private readonly AppDbContext _context;

    public CountryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CountryDto> CreateAsync(CreateCountryDto dto)
    {
        bool alreadyExists = await _context.Countries
            .AnyAsync(c => c.Name.ToLower() == dto.Name.Trim().ToLower());

        if (alreadyExists)
            throw new InvalidOperationException("Country already exists.");

        Country country = CountryMapper.ToEntity(dto);

        _context.Countries.Add(country);
        await _context.SaveChangesAsync();

        return CountryMapper.ToDto(country);
    }

    public async Task<CountryDto?> UpdateAsync(Guid id, UpdateCountryDto dto)
    {
        Country? country = await _context.Countries
            .FirstOrDefaultAsync(c => c.Id == id);

        if (country is null)
            return null;

        bool nameAlreadyExists = await _context.Countries
            .AnyAsync(c =>
                c.Id != id &&
                c.Name.ToLower() == dto.Name.Trim().ToLower());

        if (nameAlreadyExists)
            throw new InvalidOperationException("Country name already exists.");

        country.ChangeName(dto.Name);

        await _context.SaveChangesAsync();

        return CountryMapper.ToDto(country);
    }

    public async Task<CountryDto?> GetByIdAsync(Guid id)
    {
        Country? country = await _context.Countries
            .FirstOrDefaultAsync(c => c.Id == id);

        if (country is null)
            return null;

        return CountryMapper.ToDto(country);
    }

    public async Task<CountryDto?> GetByNameAsync(string name)
    {
        string normalizedName = name.Trim().ToLower();

        Country? country = await _context.Countries
            .FirstOrDefaultAsync(c => c.Name.ToLower() == normalizedName);

        if (country is null)
            return null;

        return CountryMapper.ToDto(country);
    }

    public async Task<List<CountryDto>> GetAllAsync()
    {
        List<Country> countries = await _context.Countries
            .OrderBy(c => c.Name)
            .ToListAsync();

        return CountryMapper.ToDtoList(countries);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Country? country = await _context.Countries
            .FirstOrDefaultAsync(c => c.Id == id);

        if (country is null)
            return false;

        _context.Countries.Remove(country);
        await _context.SaveChangesAsync();

        return true;
    }
}