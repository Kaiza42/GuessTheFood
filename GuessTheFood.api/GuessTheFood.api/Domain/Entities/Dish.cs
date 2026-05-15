namespace GuessTheFood.api.Domain.Entities;

public class Dish
{
    private readonly List<DishIngredient> _ingredients = new();

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public Guid CountryId { get; private set; }

    public Country? Country { get; private set; }

    public Guid CategoryId { get; private set; }

    public Category Category { get; private set; }

    public IReadOnlyCollection<DishIngredient> Ingredients => _ingredients.AsReadOnly();

    public int IngredientCount => _ingredients.Count;

    private Dish()
    {
        Name = string.Empty;
        Description = string.Empty;
        Country = null!;
        Category = null!;
    }

    public Dish(string name, string description, Country country, Category category)
    {
        Id = Guid.NewGuid();
        Name = ValidateName(name);
        Description = ValidateDescription(description);
        SetCountry(country);
        SetCategory(category);
    }

    public void AddIngredient(Ingredient ingredient, decimal quantity, string unit)
    {
        if (ingredient is null)
            throw new ArgumentNullException(nameof(ingredient));

        bool alreadyExists = _ingredients.Any(dishIngredient =>
            dishIngredient.IngredientId == ingredient.Id);

        if (alreadyExists)
            throw new InvalidOperationException("Ingredient already exists for this dish.");

        _ingredients.Add(new DishIngredient(this, ingredient, quantity, unit));
    }

    public void SetCountry(Country country)
    {
        if (country is null)
            throw new ArgumentNullException(nameof(country));

        Country = country;
        CountryId = country.Id;
    }

    public void SetCategory(Category category)
    {
        if (category is null)
            throw new ArgumentNullException(nameof(category));

        Category = category;
        CategoryId = category.Id;
    }

    public void Update(string name, string description, Country country, Category category)
    {
        Name = ValidateName(name);
        Description = ValidateDescription(description);
        SetCountry(country);
        SetCategory(category);
    }

    private static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Dish name cannot be empty.", nameof(name));

        name = name.Trim();

        if (name.Length < 2 || name.Length > 100)
            throw new ArgumentException("Dish name must be between 2 and 100 characters.", nameof(name));

        return name;
    }

    private static string ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Dish description cannot be empty.", nameof(description));

        description = description.Trim();

        if (description.Length < 10 || description.Length > 500)
            throw new ArgumentException("Dish description must be between 10 and 500 characters.", nameof(description));

        return description;
    }
}