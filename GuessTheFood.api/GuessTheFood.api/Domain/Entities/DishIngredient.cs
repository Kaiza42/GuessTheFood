namespace GuessTheFood.api.Domain.Entities;

public class DishIngredient
{
    public Guid DishId { get; private set; }
    public Dish Dish { get; private set; }

    public Guid IngredientId { get; private set; }
    public Ingredient Ingredient { get; private set; }

    private DishIngredient()
    {
        Dish = null!;
        Ingredient = null!;
    }

    public DishIngredient(Dish dish, Ingredient ingredient)
    {
        Dish = dish ?? throw new ArgumentNullException(nameof(dish));
        Ingredient = ingredient ?? throw new ArgumentNullException(nameof(ingredient));

        DishId = dish.Id;
        IngredientId = ingredient.Id;
    }
}