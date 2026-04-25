namespace GuessTheFood.api.Domain.Entities;

public class Dish
{
    private readonly List<DishIngredient> _ingredients = new();

    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public IReadOnlyCollection<DishIngredient> Ingredients => _ingredients.AsReadOnly();

    public int IngredientCount => _ingredients.Count;

    private Dish()
    {
        Name = string.Empty;
    }

    public Dish(string name)
    {
        Id = Guid.NewGuid();
        Name = ValidateName(name);
    }

    public void AddIngredient(Ingredient ingredient)
    {
        if (ingredient is null)
            throw new ArgumentNullException(nameof(ingredient));

        bool alreadyExists = _ingredients.Any(dishIngredient =>
            dishIngredient.IngredientId == ingredient.Id);

        if (alreadyExists)
            throw new InvalidOperationException("Ingredient already exists for this dish.");

        _ingredients.Add(new DishIngredient(this, ingredient));
    }

    private static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Dish name cannot be empty.", nameof(name));

        return name.Trim();
    }
}