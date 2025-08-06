using DishesAPI.Entities;

namespace DishesMinimalApi.Infrastructure.Entities;

public class DishIngredient
{
    public DishIngredient(Guid dishId, Guid ingredientId)
    {
        DishId = dishId;
        IngredientId = ingredientId;
    }

    public Guid DishId { get; set; }
    public Dish Dish { get; set; } = null!;

    public Guid IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = null!;
}