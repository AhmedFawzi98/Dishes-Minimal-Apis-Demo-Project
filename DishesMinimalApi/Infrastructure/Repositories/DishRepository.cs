using DishesAPI.DbContexts;
using DishesAPI.Entities;
using Microsoft.EntityFrameworkCore;
using static DishesMinimalApi.Features.Dishes.GetAll.Dishes;
using static DishesMinimalApi.Features.Dishes.GetById.Dishes;

namespace DishesMinimalApi.Infrastructure.Repositories.Abstractions;

public class DishRepository(DishesDbContext context) : IDishRepository
{
    public async Task<IEnumerable<DishDto>> GetAllDishDtosAsync()
    {
        var dishesDtos = await context
            .Dishes
            .Select(dish => new DishDto(dish.Id, dish.Name))
            .ToListAsync();

        return dishesDtos;
    }

    public async Task<DishWithIngredientsDto> GetDishWithIngredientsDtoByIdAsync(Guid id)
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

    public void Add(Dish dish)
    {
        context.Dishes.Add(dish);
    }

    public async Task<Dish> GetDishByIdAsync(Guid id)
    {
        return await context.Dishes.FindAsync(id);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public void Delete(Dish dishToDelete)
    {
       context.Dishes.Remove(dishToDelete);
    }
}
