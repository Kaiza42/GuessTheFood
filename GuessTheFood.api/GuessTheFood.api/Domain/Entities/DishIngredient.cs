namespace GuessTheFood.api.Domain.Entities;

public class DishIngredient
{
    public Guid DishId { get; private set; }
    public Dish Dish { get; private set; }

    public Guid IngredientId { get; private set; }
    public Ingredient Ingredient { get; private set; }

    public decimal Quantity { get; private set; }

    public string Unit { get; private set; }

    private DishIngredient()
    {
        Dish = null!;
        Ingredient = null!;
        Unit = string.Empty;
    }

    public DishIngredient(Dish dish, Ingredient ingredient, decimal quantity, string unit)
    {
        Dish = dish ?? throw new ArgumentNullException(nameof(dish));
        Ingredient = ingredient ?? throw new ArgumentNullException(nameof(ingredient));

        DishId = dish.Id;
        IngredientId = ingredient.Id;

        Quantity = ValidateQuantity(quantity);
        Unit = ValidateUnit(unit);
    }

    public void UpdateQuantity(decimal quantity, string unit)
    {
        Quantity = ValidateQuantity(quantity);
        Unit = ValidateUnit(unit);
    }

    private static decimal ValidateQuantity(decimal quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than 0.", nameof(quantity));

        return quantity;
    }

    private static string ValidateUnit(string unit)
    {
        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentException("Unit cannot be empty.", nameof(unit));

        unit = unit.Trim();

        if (unit.Length < 1 || unit.Length > 20)
            throw new ArgumentException("Unit must be between 1 and 20 characters.", nameof(unit));

        return unit;
    }
}