using DishesAPI.DbContexts;
using DishesAPI.Entities;
using DishesMinimalApi.Features.Dishes.GetById;
using DishesMinimalApi.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static DishesMinimalApi.Features.Dishes.GetAll.GetAllDishes;
using static DishesMinimalApi.Features.Dishes.GetById.GetByIdWithIngredients;

namespace DishesMinimalApi.Infrastructure.Repositories.Abstractions;

public class DishRepository(DishesDbContext context) : IDishRepository
{
    public async Task<IEnumerable<DishDto>> GetAllDishes()
    {
        var dishesDtos = await context
            .Dishes
            .Select(dish => new DishDto(dish.Id, dish.Name))
            .ToListAsync();

        return dishesDtos;
    }

    public async Task<DishWithIngredientsDto> GetDishById(Guid id)
    {
        var dishWithIngredients = await context
           .Dishes
           .Where(dish => dish.Id == id)
           .Include(dish => dish.DishIngredients)
           .ThenInclude(dishIngredient => dishIngredient.Ingredient)
           .FirstOrDefaultAsync();

        if (dishWithIngredients == null)
            return null;

        var DishIngredientsDtos = dishWithIngredients
            .DishIngredients
            .Select(dishIngredient => dishIngredient.Ingredient)
            .Select(ingredient => new DishIngredientDto(ingredient.Id, ingredient.Name))
            .ToList();

        var dishWithIngredientsDto = new DishWithIngredientsDto(dishWithIngredients.Id, dishWithIngredients.Name, DishIngredientsDtos);

        return dishWithIngredientsDto;
    }
}
